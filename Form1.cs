using System.Globalization;

namespace VAlignStratFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (TryParseDouble(StartY1.Text, out double startY1) &&
                TryParseDouble(StartY2.Text, out double startY2) &&
                TryParseDouble(GoalY1.Text, out double goalY1) &&
                TryParseDouble(GoalY2.Text, out double goalY2) &&
                TryParseDouble(FloorY.Text, out double floorY))
            {
                Results.Items.Clear();

                Player.SetGoalRange(goalY1, goalY2, floorY);

                GroupedResults = new Dictionary<int, List<Player>>();

                Player p = new Player(startY1, startY2);
                BruteforceList(p.Advance(true));
                Bruteforce(p);

                p = new Player(startY1, startY2);
                BruteforceList(p.Advance(false));
                Bruteforce(p);

                List<(string Strat, double SuccessRate)> ProcessedResults = new List<(string, double)>();
                foreach (KeyValuePair<int, List<Player>> value in GroupedResults)
                {
                    (int ReleaseFrame, List<Player> players) = value;
                    string Strat = players[0].ToString();
                    double TotalRange = 0;
                    double TotalOverlap = 0;
                    foreach (Player player in players)
                    {
                        TotalRange += player.GetRangeLength();
                        TotalOverlap += player.GetGoalOverlapLength();
                    }
                    double SuccessRate = TotalOverlap / TotalRange;
                    ProcessedResults.Add((Strat, SuccessRate));
                }

                foreach ((string Strat, double SuccessRate) in ProcessedResults.OrderByDescending(t => t.SuccessRate))
                {
                    Results.Items.Add($"{Strat}: {Math.Round(SuccessRate * 100, 5)}%");
                }
            }
            else
            {
                MessageBox.Show("Failed to parse a value");
            }
        }

        Dictionary<int, List<Player>> GroupedResults;

        private void Bruteforce(Player p)
        {
            if (p.IsFullyStable())
            {
                if (GroupedResults.ContainsKey(p.GetReleaseFrame()))
                {
                    GroupedResults[p.GetReleaseFrame()].Add(p);
                }
                else
                {
                    GroupedResults.Add(p.GetReleaseFrame(), [p]);
                }
                return;
            }
            else if (p.IsLowerStable())
            {
                Player unstable = p.SplitStable();
                Bruteforce(unstable);
                Bruteforce(p);
                return;
            }

            if (p.CanRelease())
            {
                Player temp = new Player(p);
                BruteforceList(temp.Advance(true));
                Bruteforce(temp);
            }
            BruteforceList(p.Advance(false));
            Bruteforce(p);
        }

        private void BruteforceList(List<Player> PlayerList)
        {
            foreach (Player p in PlayerList)
            {
                Bruteforce(p);
            }
        }

        private static bool TryParseDouble(string Text, out double Result)
        {
            return double.TryParse(Text, CultureInfo.InvariantCulture, out Result);
        }

        private void UpdateGoalText()
        {
            if (TryParseDouble(GoalY1.Text, out double goalY1) &&
                TryParseDouble(GoalY2.Text, out double goalY2) &&
                Math.Round(goalY1) == Math.Round(goalY2))
            {
                FloorY.Text = $"{Math.Round(goalY1) + 9}";
            }
            else
            {
                FloorY.Text = "";
            }
        }

        private void GoalY1_TextChanged(object sender, EventArgs e)
        {
            UpdateGoalText();
        }

        private void GoalY2_TextChanged(object sender, EventArgs e)
        {
            UpdateGoalText();
        }

        private static bool HandleBitAdjustment(TextBox textBox, char keyChar)
        {
            switch (keyChar)
            {
                case 'w':
                    if (TryParseDouble(textBox.Text, out double y))
                    {
                        textBox.Text = Math.BitDecrement(y).ToString(CultureInfo.InvariantCulture);
                        return true;
                    }
                    break;
                case 's':
                    if (TryParseDouble(textBox.Text, out y))
                    {
                        textBox.Text = Math.BitIncrement(y).ToString(CultureInfo.InvariantCulture);
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        private void StartY1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HandleBitAdjustment((TextBox)sender, e.KeyChar);
        }

        private void StartY2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HandleBitAdjustment((TextBox)sender, e.KeyChar);
        }

        private void GoalY1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HandleBitAdjustment((TextBox)sender, e.KeyChar);
        }

        private void GoalY2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HandleBitAdjustment((TextBox)sender, e.KeyChar);
        }
    }
}

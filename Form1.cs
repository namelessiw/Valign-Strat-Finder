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

            }
            else
            {
                MessageBox.Show("Failed to parse a value");
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

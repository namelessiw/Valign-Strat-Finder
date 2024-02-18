namespace VAlignStratFinder
{
    internal class Player
    {
        const double GRAVITY = 0.4, SINGLEJUMP_VSPEED = -8.5, RELEASE_MULTIPLIER = 0.45, MAX_VSPEED = 9;

        double YUpper, YLower, VSpeed;
        int Frame, ReleaseFrame;

        static double GoalYUpper, GoalYLower, FloorY;

        public static void SetGoalRange(double GoalY1, double GoalY2, double GroundY)
        {
            GoalYUpper = Math.Min(GoalY1, GoalY2);
            GoalYLower = Math.Max(GoalY1, GoalY2);
            FloorY = GroundY - 8;
        }

        public Player(double StartY1, double StartY2)
        {
            YUpper = Math.Min(StartY1, StartY2);
            YLower = Math.Max(StartY1, StartY2);
            VSpeed = SINGLEJUMP_VSPEED;
            Frame = 0;
            ReleaseFrame = -2;
        }

        public Player(Player p)
        {
            YUpper = p.YUpper;
            YLower = p.YLower;
            VSpeed = p.VSpeed;
            Frame = p.Frame;
            ReleaseFrame = p.ReleaseFrame;
        }

        public double GetRangeLength() => YLower - YUpper;

        public double GetGoalOverlapLength()
        {
            // [407.1, 407.3], [407.25, 407.4] => [407.25, 407.3]
            // [407.2, 407.3], [407.1, 407.4] => [407.2, 407.3]
            // [407.2, 407.3], [407.1, 407.25] => [407.2, 407.25]
            // [407.2, 407.3], [407.1, 407.15] => -

            if (YUpper > GoalYLower || YLower < GoalYUpper)
            {
                return 0;
            }

            double UpperValue = Math.Max(GoalYUpper, YUpper),
                LowerValue = Math.Min(GoalYLower, YLower);
            return LowerValue - UpperValue;
        }

        public int GetReleaseFrame() => ReleaseFrame;

        public bool CanRelease() => ReleaseFrame == -2 && VSpeed < 0;

        public bool IsFullyStable() => Math.Round(YUpper + GRAVITY) >= FloorY;
        public bool IsLowerStable() => Math.Round(YLower + GRAVITY) >= FloorY;

        protected void MoveUp()
        {
            YUpper -= 1;
            YLower -= 1;
        }

        protected void MoveDown()
        {
            YUpper += 1;
            YLower += 1;
        }

        // TODO: simplify collision code and create split function

        public Player SplitStable()
        {
            YUpper += GRAVITY;
            YLower += GRAVITY;

            // split at .5, move back and continue from there
            double Split = Math.Round(YUpper) + 0.5;
            double newYUpper, newYLower;

            if (Math.Abs(Math.Round(YUpper)) % 2 == 0)
            {
                // ..., 406.5], (406.5, ...
                newYLower = Split;
                newYUpper = Math.BitIncrement(Split);
            }
            else
            {
                // ..., 407.5), [407.5, ...
                newYLower = Math.BitDecrement(Split);
                newYUpper = Split;
            }

            Player LowerPlayer = new Player(this);
            LowerPlayer.YUpper = newYUpper;
            LowerPlayer.YUpper -= GRAVITY;
            LowerPlayer.YLower -= GRAVITY;
            LowerPlayer.VSpeed = 0;
            LowerPlayer.Frame += 1;

            // now to finish this one
            YLower = newYLower;
            YUpper -= GRAVITY;
            YLower -= GRAVITY;

            return LowerPlayer;
        }

        public List<Player> Advance(bool Release)
        {
            if (Release)
            {
                VSpeed *= RELEASE_MULTIPLIER;
                ReleaseFrame = Frame;
            }
            else if (VSpeed >= 0 && ReleaseFrame == -2)
            {
                ReleaseFrame = -1;
            }

            if (VSpeed > MAX_VSPEED)
            {
                VSpeed = MAX_VSPEED;
            }

            VSpeed += GRAVITY;

            List<Player> SplitOffPlayers = new List<Player>();

            double PreviousVSpeed = VSpeed;
            while (VSpeed != 0)
            {
                if (Math.Round(YLower + VSpeed) < FloorY)
                {
                    YLower += VSpeed;
                    YUpper += VSpeed;
                    VSpeed = PreviousVSpeed;
                    break;
                }

                // assumes floor collision
                // TODO: implement ceiling collision
                while (VSpeed >= 1 && Math.Round(YLower) < FloorY)
                {
                    MoveDown();
                    VSpeed -= 1;
                }

                if (Math.Round(YLower) < FloorY)
                {
                    if (Math.Round(YUpper + VSpeed) >= FloorY)
                    {
                        VSpeed = 0;
                        break;
                    }

                    YUpper += VSpeed;
                    YLower += VSpeed;

                    // split at .5, move back and continue from there
                    double Split = Math.Round(YUpper) + 0.5;
                    double newYUpper, newYLower;

                    if (Math.Abs(Math.Round(YUpper)) % 2 == 0)
                    {
                        // ..., 406.5], (406.5, ...
                        newYLower = Split;
                        newYUpper = Math.BitIncrement(Split);
                    }
                    else
                    {
                        // ..., 407.5), [407.5, ...
                        newYLower = Math.BitDecrement(Split);
                        newYUpper = Split;
                    }

                    Player LowerPlayer = new Player(this);
                    LowerPlayer.YUpper = newYUpper;
                    LowerPlayer.YUpper -= VSpeed;
                    LowerPlayer.YLower -= VSpeed;
                    LowerPlayer.VSpeed = 0;
                    LowerPlayer.Frame += 1;
                    SplitOffPlayers.Add(LowerPlayer);

                    // now to finish this one
                    YLower = newYLower;

                    VSpeed = PreviousVSpeed;
                    break;
                }
                else
                {
                    if (Math.Round(YUpper) < FloorY)
                    {
                        double Split = Math.Round(YUpper) + 0.5;
                        double newYUpper, newYLower;

                        if (Math.Abs(Math.Round(YUpper)) % 2 == 0)
                        {
                            // ..., 406.5], (406.5, ...
                            newYLower = Split;
                            newYUpper = Math.BitIncrement(Split);
                        }
                        else
                        {
                            // ..., 407.5), [407.5, ...
                            newYLower = Math.BitDecrement(Split);
                            newYUpper = Split;
                        }

                        Player LowerPlayer = new Player(this);
                        LowerPlayer.YUpper = newYUpper;
                        LowerPlayer.MoveUp();
                        LowerPlayer.VSpeed = 0;
                        LowerPlayer.Frame += 1;
                        SplitOffPlayers.Add(LowerPlayer);

                        // now to finish this one
                        YLower = newYLower;
                    }
                    else
                    {
                        MoveUp();
                        VSpeed = 0;
                        break;
                    }
                }
            }





            /*if (Math.Round(YLower + VSpeed) >= FloorY)
            {
                while (Math.Round(YLower) < FloorY)
                {
                    MoveDown();
                    VSpeed -= 1;
                }

                if (Math.Round(YLower) == Math.Round(YUpper))
                {
                    MoveUp();
                    VSpeed = 0;
                }
                else
                {
                    // split at .5, move back and continue from there
                    double Split = Math.Round(YUpper) + 0.5;
                    double newYUpper, newYLower;

                    if (Math.Abs(Math.Round(YUpper)) % 2 == 0)
                    {
                        // ..., 406.5], (406.5, ...
                        newYLower = Split;
                        newYUpper = Math.BitIncrement(Split);
                    }
                    else
                    {
                        // ..., 407.5), [407.5, ...
                        newYLower = Math.BitDecrement(Split);
                        newYUpper = Split;
                    }

                    Player LowerPlayer = new Player(this);
                    LowerPlayer.YUpper = newYUpper;
                    LowerPlayer.MoveUp();
                    LowerPlayer.VSpeed = 0;
                    LowerPlayer.Frame += 1;
                    SplitOffPlayers.Add(LowerPlayer);

                    // now to finish this one
                    YLower = newYLower;
                    if (Math.Round(YLower + VSpeed) < FloorY)
                    {
                        YUpper += VSpeed;
                        YLower += VSpeed;
                    }
                    else
                    {
                        while (Math.Round(YLower) < FloorY)
                        {
                            MoveDown();
                            VSpeed -= 1;
                        }

                        if (Math.Round(YLower) == Math.Round(YUpper))
                        {
                            MoveUp();
                            VSpeed = 0;
                        }
                        else
                        {
                            Split = Math.Round(YUpper) + 0.5;

                            if (Math.Abs(Math.Round(YUpper)) % 2 == 0)
                            {
                                // ..., 406.5], (406.5, ...
                                newYUpper = Split;
                                newYLower = Math.BitIncrement(Split);
                            }
                            else
                            {
                                // ..., 407.5), [407.5, ...
                                newYUpper = Math.BitDecrement(Split);
                                newYLower = Split;
                            }

                            LowerPlayer = new Player(this);
                            LowerPlayer.YUpper = newYUpper;
                            LowerPlayer.MoveUp();
                            LowerPlayer.VSpeed = 0;
                            LowerPlayer.Frame += 1;
                            SplitOffPlayers.Add(LowerPlayer);

                            YLower = newYLower;

                            if (Math.Round(YLower + VSpeed) < FloorY)
                            {
                                YUpper += VSpeed;
                                YLower += VSpeed;
                            }
                            else
                            {
                                MoveDown();
                            }
                        }
                    }

                    VSpeed = 0;
                }
            }
            else
            {
                YUpper += VSpeed;
                YLower += VSpeed;
            }*/


            Frame++;
            return SplitOffPlayers;
        }

        // TODO: add toggle between 0f/1f convention
        public override string ToString() => (ReleaseFrame >= 0 ? $"{ReleaseFrame + 1}f" : "FJ") /*+ $" ({Frame})"*/;
    }
}

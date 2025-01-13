using Core.Extentions;

namespace Core.Utilities.Balance
{
    public enum GameBalanceRound
    {
        None = 0,
        Nearest1 = 1,
        Nearest5 = 10,
        Nearest5_10 = 20,
        Nearest5_10_50 = 30,
        Nearest5_10_50_100 = 40,
        Nearest5_50_100 = 50,
        Nearest5_50_500 = 60,
        Nearest5_50_500_5000 = 70,

        Nearest10 = 100,

        Nearest50 = 200,

        Nearest100 = 300,

        Nearest500 = 400,

        Nearest1000 = 500,
        Nearest500_2500_5000 = 550
    }

    public static class GameBalanceRoundCalculator
    {
        public static float RoundValue(this GameBalanceRound round, float value)
        {
            switch (round)
            {
                case GameBalanceRound.Nearest1:
                    value = value.RoundToNearest(1);
                    break;

                case GameBalanceRound.Nearest5:
                    value = value.RoundToNearest(5);
                    break;

                case GameBalanceRound.Nearest5_10:
                    if (value <= 100) value = value.RoundToNearest(5);
                    else value = value.RoundToNearest(10);
                    break;

                case GameBalanceRound.Nearest5_10_50:
                    if (value <= 100) value = value.RoundToNearest(5);
                    else if (value <= 1000) value = value.RoundToNearest(10);
                    else value = value.RoundToNearest(50);
                    break;

                case GameBalanceRound.Nearest5_10_50_100:
                    if (value <= 100) value = value.RoundToNearest(5);
                    else if (value <= 1000) value = value.RoundToNearest(10);
                    else if (value <= 10000) value = value.RoundToNearest(50);
                    else value = value.RoundToNearest(100);
                    break;

                case GameBalanceRound.Nearest5_50_100:
                    if (value <= 100) value = value.RoundToNearest(5);
                    else if (value <= 1000) value = value.RoundToNearest(50);
                    else value = value.RoundToNearest(100);
                    break;

                case GameBalanceRound.Nearest5_50_500:
                    if (value <= 100) value = value.RoundToNearest(5);
                    else if (value <= 1000) value = value.RoundToNearest(50);
                    else value = value.RoundToNearest(500);
                    break;

                case GameBalanceRound.Nearest5_50_500_5000:
                    if (value <= 100) value = value.RoundToNearest(5);
                    else if (value <= 1000) value = value.RoundToNearest(50);
                    else if (value <= 10000) value = value.RoundToNearest(500);
                    else value = value.RoundToNearest(5000);
                    break;

                case GameBalanceRound.Nearest10:
                    value = value.RoundToNearest(10);
                    break;

                case GameBalanceRound.Nearest50:
                    value = value.RoundToNearest(50);
                    break;

                case GameBalanceRound.Nearest100:
                    value = value.RoundToNearest(100);
                    break;

                case GameBalanceRound.Nearest500:
                    value = value.RoundToNearest(500);
                    break;

                case GameBalanceRound.Nearest1000:
                    value = value.RoundToNearest(1000);
                    break;

                case GameBalanceRound.Nearest500_2500_5000:
                    if (value <= 10000) value = value.RoundToNearest(500);
                    else if (value <= 100000) value = value.RoundToNearest(2500);
                    else value = value.RoundToNearest(5000);
                    break;
            }

            return value;
        }
    }
}
using System;

namespace Core.Utilities.Balance
{
    public static class GameBalanceCalculator
    {
        public static float Calculate(this GameBalanceFormula formula, float baseValue, float step,
            float stepGrowthRate, float stepGrowthAcceleration, int level)
        {
            if (level <= 0)
                return baseValue;
            switch (formula)
            {
                case GameBalanceFormula.Exponent: return Exponent(level, baseValue, step, stepGrowthRate, stepGrowthAcceleration);
                case GameBalanceFormula.Plus: return Plus(level, baseValue, step, stepGrowthRate, stepGrowthAcceleration);
                case GameBalanceFormula.Minus: return Minus(level, baseValue, step, stepGrowthRate, stepGrowthAcceleration);
                default: throw new ArgumentOutOfRangeException(nameof(formula), formula, null);
            }
        }
        
        private static float Exponent(int targetLevel, float baseValue, float stepValue, float stepGrowthRate, float stepGrowthAcceleration)
        {
            return (float)(baseValue * Math.Pow(stepValue, Math.Pow(targetLevel, stepGrowthAcceleration) * stepGrowthRate));
        }
        
        private static float Plus(int targetLevel, float baseValue, float stepValue, float stepGrowthRate, float stepGrowthAcceleration)
        {
            return (float)(baseValue + stepValue * Math.Pow(targetLevel, stepGrowthAcceleration) * stepGrowthRate);
        }
        
        private static float Minus(int targetLevel, float baseValue, float stepValue, float stepGrowthRate, float stepGrowthAcceleration)
        {
            return (float)(baseValue - stepValue * Math.Pow(targetLevel, stepGrowthAcceleration) * stepGrowthRate);
        }
    }
}
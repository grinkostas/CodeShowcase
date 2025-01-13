using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Utilities.Balance
{
    [System.Serializable]
    public class GameBalanceValueConfig
    {
        public GameBalanceFormula formula;

        [SerializeField, HideIf(nameof(isPreset)), AllowNesting]
        private float start;

        [SerializeField, HideIf(nameof(isPreset)), AllowNesting]
        private float step;

        [SerializeField, HideIf(nameof(isPreset)), AllowNesting]
        private float stepGrowthRate = 1;

        [SerializeField, HideIf(nameof(isPreset)), AllowNesting]
        private float stepGrowthAcceleration = 1;

        [SerializeField, HideIf(nameof(isPreset)), AllowNesting]
        private GameBalanceRound round;

        [SerializeField, ShowIf(nameof(isPreset)), AllowNesting]
        private float[] presetValues;

        [SerializeField, ShowIf(nameof(isPreset)), AllowNesting]
        private bool useLastWhenOutOfRangeOfPreset = true;

        public bool isPreset => formula == GameBalanceFormula.Preset;

        public float Calculate(int level = 1)
        {
            if (formula == GameBalanceFormula.Preset)
            {
                if (presetValues.Length == 0)
                    return 0;

                var index = Mathf.Max(0, level);
                if (useLastWhenOutOfRangeOfPreset && index > presetValues.Length)
                    index = presetValues.Length - 1;

                return presetValues[index];
            }

            if (level == 0)
                return start;
            var value = formula.Calculate(start, step, stepGrowthRate, stepGrowthAcceleration, level);
            value = round.RoundValue(value);
            return value;
            
        }

        public int CalculateToInt(int level = 1) => (int)Calculate(level);
    }
}
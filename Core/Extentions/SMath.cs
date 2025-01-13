using Core.Utilities;
using UnityEngine;

namespace Core.Extentions
{
    public static class SMath
    {
        public static bool IsEqual(this float value1, float value2)
        {
            return Mathf.Abs(value1 - value2) <= Constants.tolerance;
        }

        public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);

        public static bool IsZero(this float value)
        {
            return Mathf.Abs(value) <= Constants.tolerance;
        }
        
        public static float MinAbs(float value1, float value2)
        {
            if (Mathf.Abs(value1) < Mathf.Abs(value2))
                return value1;
            return value2;
        }

        public static float MaxAbs(float value1, float value2)
        {
            if (Mathf.Abs(value1) < Mathf.Abs(value2))
                return value2;
            return value1;
        }

        public static float Sqr(this float value)
        {
            return value * value;
        }
    }
}

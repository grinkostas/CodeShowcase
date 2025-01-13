using System;
using UnityEngine;

namespace Core.Extentions
{
    public static class NumberExtensions
    {
        public static int ToInt(this bool value) => value ? 1 : 0;
        public static int ToInt(this float value) => (int)value;
        public static int ToIntRound(this float value) => Mathf.RoundToInt(value);
        
        public static int RoundToNearest(this int num, int nearest)
        {
            return ((float)num).RoundToNearestInt(nearest);
        }
        public static int RoundToNearestInt(this float num, int nearest)
        {
            return Mathf.RoundToInt(num / nearest) * nearest;
        }

        public static float RoundToNearest(this float num, float nearest)
        {
            return Mathf.Round(num / nearest) * nearest;
        }

        public static double RoundToNearest(this double num, float nearest)
        {
            return Math.Round(num / nearest) * nearest;
        }

        public static float Sign(this float num)
        {
            return num >= 0 ? 1 : -1;
        }
        
        public static float Format(this float value, int format)
        {
            return (float)Math.Round(value - value % (double)(Math.Pow(0.1, (double)format)), format);
        }

    }
}
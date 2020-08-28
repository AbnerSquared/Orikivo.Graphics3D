using System;

namespace Orikivo.Graphics3D
{
    internal static class Utils
    {
        internal static float Radians(float angle)
            => angle / 180.0f * 3.14159274f;

        internal static float RangeConvert(float fromMin, float fromMax, float toMin, float toMax, float value)
        {
            float from = fromMax - fromMin;
            float to = toMax - toMin;

            if (Math.Abs(from) < 0.001f)
                return toMin;

            return (value - fromMin) * to / from + toMin;
        }

        internal static bool RangeContains(float min, float max, float value, bool inclusiveMin = true, bool inclusiveMax = true)
            => (inclusiveMin ? value >= min : value > min)
               && (inclusiveMax ? value <= max : value < max);
    }
}

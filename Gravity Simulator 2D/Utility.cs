using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.Xna.Framework;

namespace GravitySimulator2D
{
    public static class MathUtil
    {
        public static float Lerp(float from, float to, float time)
        {
            return from + (to - from) * time;
        }
        public static float InvLerp(float from, float to, float value)
        {
            return (value - from) / (to - from);
        }

        public static Vector3 Lerp(Vector3 from, Vector3 to, float time)
        {
            return new Vector3(Lerp(from.X, to.X, time), Lerp(from.Y, to.Y, time), Lerp(from.Z, to.Z, time));
        }

        public static int GCD(int a, int b)
        {
            if (b == 0)
                return a;
            else
                return GCD(b, a % b);
        }
        public static int LCM(int a, int b)
        {
            return (Math.Abs(a) / GCD(a, b)) * Math.Abs(b);
        }

        public static bool InRange(float min, float max, float val)
        {
            return val >= min && val <= max;
        }

        public static bool AlmostZero(float val)
        {
            return InRange(-.001f, .001f, val);
        }
    }
}

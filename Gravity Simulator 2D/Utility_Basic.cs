using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.Xna.Framework;

namespace GravitySimulator2D
{
    static class Utility_Basic
    {
        //TODO_L: Make Utility_Basic useable
        public static float map(int x, int in_min, int in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static float lerp(float from, float to, float time)
        {
            return from + (to - from) * time;
        }
        public static float invLerp(float from, float to, float value)
        {
            return (value - from) / (to - from);
        }
    }
}

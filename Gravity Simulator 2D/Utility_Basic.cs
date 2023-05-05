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

    public struct Circle
    {
        public Vector2 centre;

        public float radius;

        /// <summary>
        /// Calculate degree of a point relative to this circle.
        /// </summary>
        public float DegreeOfPoint(Vector2 point)
        {
            float distSqr = Vector2.DistanceSquared(point, centre);
            return distSqr - radius * radius;
        }

        public static float DegreeOfPoint(Circle circle, Vector2 point)
        {
            return circle.DegreeOfPoint(point);
        }

        public float DegreeOfCircle(Circle circle)
        {
            float distCentreSqr = Vector2.DistanceSquared(centre, circle.centre);
            float distSqr = distCentreSqr - circle.radius * circle.radius;
            return distSqr - radius * radius;
        }

        public float CalculateCircumference()
        {
            return 2 * MathF.PI * radius;
        }

        public float CalculateDegreesCircumference()
        {
            return 2 * 360 * radius;
        }
    }

    public struct Mat22
    {
        float m00, m01;
        float m10, m11;

        public Mat22(float radians)
        {
            float cos = MathF.Cos(radians);
            float sin = MathF.Sin(radians);

            m00 = cos; m01 = -sin;
            m10 = sin; m11 = cos;
        }

        public Mat22(float a, float b, float c, float d)
        {
            m00 = a; m01 = b; m10 = c; m11 = d;
        }

        public void Set(float radians)
        {
            float cos = MathF.Cos(radians);
            float sin = MathF.Sin(radians);

            m00 = cos; m01 = -sin;
            m10 = sin; m11 = cos;
        }

        public Mat22 Abs()
        {
            return new Mat22(MathF.Abs(m00), MathF.Abs(m01), MathF.Abs(m10), MathF.Abs(m11));
        }

        public Vector2 AxisX()
        {
            return new Vector2(m00, m10);
        }

        public Vector2 AxisY()
        {
            return new Vector2(m01, m11);
        }

        public Mat22 Transpose()
        {
            return new Mat22(m00, m10, m01, m11);
        }

        public static Vector2 operator *(Mat22 left, Vector2 right)
        {
            return new Vector2(left.m00 * right.X + left.m01 * right.Y, left.m10 * right.X + left.m11 * right.Y);
        }

        public static Mat22 operator *(Mat22 left, Mat22 right)
        {
            return new Mat22
                (
                left.m00 * right.m00 + left.m01 * right.m10,
                left.m00 * right.m01 + left.m01 * right.m11,
                left.m10 * right.m00 + left.m11 * right.m10,
                left.m10 * right.m01 + left.m11 * right.m11
                );
        }
    }

    public static class MathUtil
    {
        /// <summary>
        /// Uses an Euclidean algorithm to compute two numbers' Greatest Common Divisor.
        /// </summary>
        public static int GCD(int a, int b)
        {
            if (b == 0)
                return a;
            else
                return GCD(b, a % b);
        }

        /// <summary>
        /// Calculates the given numbers' GCD, then, based on it, calculates the given numbers' Least Common Multiplier.
        /// </summary>
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
        public static Vector2 Edge_GetClosestPoint(Vector2 p, Vector2 a, Vector2 b)
        {
            Vector2 ab = b - a;
            Vector2 ap = p - a;
            float ab_ab = Vector2.Dot(ab, ab);
            float ab_ap = Vector2.Dot(ap, ab);
            float t = ab_ap / ab_ab;
            t = (t < 0.0f) ? 0.0f : (t > 1.0f) ? 1.0f : t;
            return a + t * ab;
        }
    }
}

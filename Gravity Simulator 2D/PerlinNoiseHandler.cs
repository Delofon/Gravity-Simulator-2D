using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GravitySimulator2D
{
    public static class PerlinNoiseHandler
    {
        public static float Sample(Perlin2D perlin, int x, int y, int width, int height, float scale, float offsetX, float offsetY)
        {
            float xCoord = (float)x / width * scale + offsetX;
            float yCoord = (float)y / height * scale + offsetY;
            return perlin.Noise(xCoord, yCoord);
        }

        public static float Sample(Perlin2D perlin, int x, int y, int width, int height, float scale, float offsetX, float offsetY, int octaves, float perm = 0.5f)
        {
            float xCoord = (float)x / width * scale + offsetX;
            float yCoord = (float)y / height * scale + offsetY;
            return perlin.Noise(xCoord, yCoord, octaves, perm);
        }
    }
}

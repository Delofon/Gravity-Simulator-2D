using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravitySimulator2D
{
    public class Gradient
    {
        Dictionary<float, Color> keyColours;

        public Gradient()
        {
            keyColours = new Dictionary<float, Color>();
        }

        public Color Evaluate(float time, float alpha = 1)
        {
            // Safe time
            time = Math.Clamp(time, 0f, 1f);

            // If time lands on a key colour, retrieve it
            if (keyColours.ContainsKey(time))
            {
                return new Color(GetValueFromDictionary(time), MathUtil.Lerp(0, 255, alpha));
            }

            // Find previous and next keys
            float leftKey = float.NegativeInfinity;
            float rightKey = float.PositiveInfinity;

            foreach (float key in keyColours.Keys)
            {
                if (key < time)
                {
                    if (key > leftKey)
                    {
                        leftKey = key;
                    }
                }
                else
                {
                    if (key < rightKey)
                    {
                        rightKey = key;
                    }
                }
            }

            // Get the according colours in normalized values
            Vector3 leftColour = GetValueFromDictionary(leftKey).ToVector3();
            Vector3 rightColour = GetValueFromDictionary(rightKey).ToVector3();

            // Lerp

            Vector3 output = MathUtil.Lerp(leftColour, rightColour, MathUtil.InvLerp(leftKey, rightKey, time));

            // Denormalize
            Color outputColour = new Color(output);

            return new Color(outputColour, MathUtil.Lerp(0, 255, alpha));
        }

        public Texture2D GetTexture(int resolution)
        {
            Texture2D gradient = new Texture2D(GravitySimulator2D.graphics.GraphicsDevice, resolution, 1);
            Color[] data = new Color[resolution];

            for (int i = 0; i < resolution; i++)
            {
                data[i] = Evaluate((float)i / resolution);
            }

            gradient.SetData(data);
            return gradient;
        }

        public void AddKeyColour(float time, Color colour)
        {
            KeyValuePair<float, Color> pair = new KeyValuePair<float, Color>(time, colour);
            if (!keyColours.Contains(pair))
            {
                keyColours.Add(time, colour);
            }
        }

        public bool RemoveKeyColour(float time)
        {
            return keyColours.Remove(time);
        }

        private Color GetValueFromDictionary(float key)
        {
            Color output;
            keyColours.TryGetValue(key, out output);
            return output;
        }
    }
}

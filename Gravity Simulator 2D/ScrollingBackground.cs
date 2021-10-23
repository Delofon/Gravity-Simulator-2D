using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravitySimulator2D
{
    class Stars
    {
        Texture2D texture;
        Rectangle rectangle;

        public Stars(int size)
        {
            texture = constructStars(size, 9342876);
            rectangle = new Rectangle(0, 0, size * 1000, size * 1000);
        }

        public void Update(Camera2d camera)
        {

        }

        public void Draw(Camera2d camera, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, -new Vector2(rectangle.Width, rectangle.Height) / 2, rectangle, Color.White);
        }

        private Texture2D constructStars(int size, int seed)
        {
            Texture2D stars = new Texture2D(ThingiesProvider.graphics.GraphicsDevice, size, size);

            Color[] colours = new Color[size * size];

            Random rand = new Random(seed);

            for(int x = 0; x < size; x++)
            {
                for(int y = 0; y < size; y++)
                {
                    if (rand.NextDouble() < 0.001)
                    {
                        colours[x + y * size] = Color.White;
                        colours[(x) + (y + 1) * size] = Color.White;
                        colours[(x + 1) + (y) * size] = Color.White;
                        colours[(x + 1) + (y + 1) * size] = Color.White;
                    }
                }
            }

            stars.SetData(colours);

            return stars;
        }
    }
}

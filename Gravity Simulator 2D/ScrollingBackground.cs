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

        float parallax;

        public Stars(int size, int starSize, double proportion, float parallax)
        {
            texture = constructStars(size, starSize, 9342876, (proportion / (starSize * starSize)));
            rectangle = new Rectangle(0, 0, size * 2000, size * 2000);
            this.parallax = parallax;
        }

        public void Draw(Camera2d camera, SpriteBatch spriteBatch)
        {
            // TODO: Make scale also depend on parallax
            spriteBatch.Draw(texture, camera.Pos * parallax, rectangle, Color.White, 0f, Vector2.One * rectangle.Size.ToVector2() / 2, 1f, SpriteEffects.None, 0f);
        }

        private Texture2D constructStars(int size, int starSize, int seed, double randomchance)
        {
            Texture2D stars = new Texture2D(GravitySimulator2D.graphics.GraphicsDevice, size, size);

            Color[] colours = new Color[size * size];

            Random rand = new Random(seed);

            for(int x = 0; x < size; x++)
            {
                for(int y = 0; y < size; y++)
                {
                    bool draw = true;

                    if(rand.NextDouble() >= randomchance) continue;

                    if(draw)
                    {
                        for (int dx = 0; dx < starSize; dx++)
                        {
                            for (int dy = 0; dy < starSize; dy++)
                            {
                                int xpdx = x + dx;
                                while (x >= size) x -= size;
                                while (x < 0)     x += size;

                                int ypdy = y + dy;
                                while (y >= size) y -= size;
                                while (y < 0)     y += size;
                                colours[xpdx + ypdy * size] = Color.White;
                            }
                        }
                    }
                }
            }

            stars.SetData(colours);

            return stars;
        }
    }
}

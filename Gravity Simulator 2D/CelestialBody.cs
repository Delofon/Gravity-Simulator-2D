using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravitySimulator2D
{
    class BodySettings
    {
        public float mass;

        public int size;

        public Vector2 initPos;
        public Vector2 initVel;

        public BodySettings(float mass, int size, Vector2 initPos, Vector2 initVel)
        {
            this.mass = mass;
            this.size = size;
            this.initPos = initPos;
            this.initVel = initVel;
        }

        public class BodyTextureSettings
        {
            public Gradient gradient;

            public int seed;
            public float noiseScale;

            public BodyTextureSettings(Color baseColour)
            {
                this.gradient = new Gradient();
                this.gradient.AddKeyColour(0f, baseColour);
                this.gradient.AddKeyColour(1f, baseColour);
            }

            public BodyTextureSettings(float oceanThreshold, Color landColour, Color oceanColour, int seed = 0, float noiseScale = 8f)
            {
                gradient = new Gradient();
                gradient.AddKeyColour(0.0f, oceanColour);
                gradient.AddKeyColour(oceanThreshold, landColour);
                gradient.AddKeyColour(1.0f, landColour);

                this.seed = seed;
                this.noiseScale = noiseScale;
            }

            public BodyTextureSettings(Gradient gradient, int seed = 0, float noiseScale = 8f)
            {
                this.gradient = gradient;
                this.seed = seed;
                this.noiseScale = noiseScale;
            }
        }
    }

    class CelestialBody
    {
        float mass;

        public int size;

        Vector2 position;
        Vector2 velocity;

        //OrbitVisualizer visualizer;

        Texture2D texture;

        public CelestialBody(Vector2 position, int size, float mass, Vector2 initVel, BodySettings.BodyTextureSettings textureSettings)
        {
            this.mass = mass;

            this.size = size;

            this.position = position;
            velocity = initVel;

            texture = new Texture2D(GravitySimulator2D.graphics.GraphicsDevice, size, size);

            Color[] colours = new Color[size * size];

            colours = Texturize(size, textureSettings);

            texture.SetData(colours);
        }

        public CelestialBody(BodySettings settings, BodySettings.BodyTextureSettings textureSettings) : this(settings.initPos, settings.size, settings.mass, settings.initVel, textureSettings)
        {

        }

        public CelestialBody(Vector2 position, int size, Vector2 initVel, float surfaceGravity, BodySettings.BodyTextureSettings textureSettings) : this(position, size, surfaceGravity * size * size / 4 / Universe.gravitationalConstant, initVel, textureSettings)
        {

        }

        private static Color[] Texturize(int size, BodySettings.BodyTextureSettings textureSettings)
        {
            Color[] colours = new Color[size * size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    float dist = Vector2.Distance(new Vector2(size / 2, size / 2), new Vector2(x, y));
                    if (dist <= size / 2 || dist <= size / 2) colours[x + y * size] = Color.White;
                }
            }

            Perlin2D perlin = new Perlin2D(textureSettings.seed);

            for(int x = 0; x < size; x++)
            {
                for(int y = 0; y < size; y++)
                {
                    if (colours[x + y * size] != Color.White)
                        continue;

                    float sample = PerlinNoiseHandler.Sample(perlin, x, y, size, size, textureSettings.noiseScale, 0, 0, 8, .3f) + .5f;
                    colours[x + y * size] = textureSettings.gradient.Evaluate(sample);
                }
            }

            return colours;
        }

        public void SetVisualizerSteps(int steps)
        {
            //visualizer.steps = steps;
        }

        public Vector2 getPos()
        {
            return position;
        }

        public void UpdateVelocity(List<CelestialBody> bodies, float timeStep)
        {
            //visualizer.UpdateVelocity(bodies, timeStep);
            foreach(CelestialBody body in bodies)
            {
                if(body != this)
                {
                    Vector2 forceDir = -Vector2.Normalize(position - body.position);
                    Vector2 force;
                    if (Vector2.Distance(position, body.position) >= Universe.distThreshold)
                        force = forceDir * Universe.gravitationalConstant * mass * body.mass / Vector2.DistanceSquared(position, body.position);
                    else force = Vector2.Zero;
                    Vector2 acceleration = force / mass;
                    velocity += acceleration * timeStep;
                }
            }
        }

        public void UpdatePosition(float timeStep)
        {
            //visualizer.UpdatePosition();
            position += velocity * timeStep;
        }

        public void CheckCollisions(List<CelestialBody> bodies)
        {
            //foreach(CelestialBody body in bodies)
            for(int i = 0; i < bodies.Count; i++)
            {
                if(bodies[i] != this)
                {
                    CelestialBody survivingBody;
                    CelestialBody dyingBody;

                    if(Vector2.Distance(position, bodies[i].position) <= size / 2 + bodies[i].size / 2)
                    {
                        if(bodies[i].size < size / 2)
                        {
                            bodies.Remove(bodies[i]);
                        }
                        else if(size < bodies[i].size)
                        {
                            bodies.Remove(this);
                        }
                        else
                        {
                            bodies.Remove(bodies[i]);
                            bodies.Remove(this);
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position - new Vector2(texture.Width / 2, texture.Height / 2), Color.White);
        }
    }
}

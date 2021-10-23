using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravitySimulator2D
{
    class Universe
    {
        public const float gravitationalConstant = .9f;
        public const float distThreshold = 10f;
        public float timeElapsed = 0f;

        float timeStep { get; set; }

        List<CelestialBody> bodies;

        public Universe(float timeStep)
        {
            this.timeStep = timeStep;
            bodies = new List<CelestialBody>();
        }

        public void addBody(CelestialBody body)
        {
            bodies.Add(body);
        }

        //public void addBody(Vector2 position, Color colour, int size, float mass, Vector2 initVel)
        //{
        //    bodies.Add(new CelestialBody(position, colour, size, mass, initVel));
        //}

        public void addBody(Vector2 position, int size, float mass, Vector2 initVel, BodySettings.BodyTextureSettings textureSettings)
        {
            bodies.Add(new CelestialBody(position, size, mass, initVel, textureSettings));
        }

        public List<CelestialBody> getBodies()
        {
            return bodies;
        }

        public void setBodies(List<CelestialBody> bodies)
        {
            this.bodies = bodies;
        }

        public float getStep()
        {
            return timeStep;
        }

        public void setStep(float timeStep)
        {
            this.timeStep = timeStep;
        }

        public void Update()
        {
            //foreach(CelestialBody body in bodies)
            for(int i = 0; i < bodies.Count; i++)
            {
                bodies[i].CheckCollisions(ref bodies);
            }

            for(int i = 0; i < bodies.Count; i++)
            {
                bodies[i].UpdateVelocity(bodies, timeStep);
                //Console.WriteLine("Updated velocity of body {0}", i);
            }

            for (int i = 0; i < bodies.Count; i++)
            {
                bodies[i].UpdatePosition(timeStep);
                //Console.WriteLine("Updated position of body {0}", i);
            }
            timeElapsed += timeStep;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(CelestialBody body in bodies)
            {
                body.Draw(spriteBatch);
            }
        }
    }
}

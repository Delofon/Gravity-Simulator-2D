using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravitySimulator2D
{
    class OrbitVisualizer : CelestialBody
    {
        public int steps;
        float timeStep;

        public OrbitVisualizer(Vector2 position, Color colour, float mass, Vector2 initVel) : base(position, colour, 1, 1, mass, initVel)
        {
            
        }

        public void UpdatePosition()
        {

        }
    }
}

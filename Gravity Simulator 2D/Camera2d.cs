using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GravitySimulator2D
{
    //Thanks https://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
    public class Camera2d
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        public Vector2 offset;
        public int bodyFocus = 0;

        public Camera2d()
        {
            _zoom = 1.0f;
            _pos = Vector2.Zero;
        }

        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < .001f) _zoom = .001f; } // Negative zoom will flip image
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Matrix get_transformation(GraphicsDeviceManager graphics)
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphics.GraphicsDevice.Viewport.Width * 0.5f, graphics.GraphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }
    }
}

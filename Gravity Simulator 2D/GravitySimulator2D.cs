using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GravitySimulator2D.InputHandlers;

namespace GravitySimulator2D
{
    public class GravitySimulator2D : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch hud;
        Universe universe;
        SpriteFont Calibri;
        Camera2d camera;
        Stars stars;

        InputHandler ih;

        bool isEnabled = false;

        int lastMouseWheel;
        int changeMouseWheel;

        public GravitySimulator2D()
        {
            Window.Title = "Gravity Simulator 2D";
            IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        private List<CelestialBody> System1()
        {
            List<CelestialBody> bodies = new List<CelestialBody>();

            bodies.Add(new CelestialBody(Vector2.Zero, 128, 3000f, Vector2.Zero, new BodySettings.BodyTextureSettings(0.26f, Color.Yellow, Color.Brown, 43983, 32)));
            bodies.Add(new CelestialBody(new Vector2(128, 0), 16, 4, -Vector2.UnitY * 4.2f, new BodySettings.BodyTextureSettings(0.47f, Color.Blue, Color.Red, 83473)));
            bodies.Add(new CelestialBody(new Vector2(412, 0), 22, 6, -Vector2.UnitY * 2.4f, new BodySettings.BodyTextureSettings(0.47f, Color.Gray, Color.Aqua, 65323, 3)));
            bodies.Add(new CelestialBody(new Vector2(800, 0), 8, 3, -Vector2.UnitY * 1.24f, new BodySettings.BodyTextureSettings(Color.Aquamarine)));

            return bodies;
        }

        private List<CelestialBody> ThreeBodies()
        {
            List<CelestialBody> bodies = new List<CelestialBody>();

            bodies.Add(new CelestialBody(Vector2.Zero, 1500, Vector2.Zero, 50, new BodySettings.BodyTextureSettings(0.26f, Color.Yellow, Color.Brown, 43983, 32)));
            bodies.Add(new CelestialBody(Vector2.UnitX * 1500 * 3, 1200, Vector2.UnitY * 100, 42, new BodySettings.BodyTextureSettings(0.26f, Color.Yellow, Color.Brown, 355467, 32)));
            bodies.Add(new CelestialBody(Vector2.UnitX * 1500 * 7, 300, -Vector2.UnitY * 50, 10, new BodySettings.BodyTextureSettings(0.26f, Color.Brown, Color.Red, 329876)));

            return bodies;
        }

        private List<CelestialBody> System2()
        {
            List<CelestialBody> bodies = new List<CelestialBody>();

            int sunSize = 500;

            Vector2 ggPos = Vector2.UnitX * sunSize * 23;

            bodies.Add(new CelestialBody(Vector2.Zero, sunSize, Vector2.Zero, 50, new BodySettings.BodyTextureSettings(0.26f, Color.Yellow, Color.Brown, 43983, 32)));
            bodies.Add(new CelestialBody(Vector2.UnitX * sunSize * 3, sunSize - 10, Vector2.UnitY * 47, 42, new BodySettings.BodyTextureSettings(0.26f, Color.Yellow, Color.Brown, 355467, 32)));
            bodies.Add(new CelestialBody(Vector2.UnitX * sunSize * 7, 32, -Vector2.UnitY * 23, 10, new BodySettings.BodyTextureSettings(0.26f, Color.Brown, Color.Red, 329876)));
            bodies.Add(new CelestialBody(Vector2.UnitX * sunSize * 11, 52, -Vector2.UnitY * 10, 15, new BodySettings.BodyTextureSettings(Color.Gray)));

            Gradient terra = new Gradient();
            terra.AddKeyColour(0f, Color.Blue);
            terra.AddKeyColour(.25f, Color.Blue);
            terra.AddKeyColour(.5f, Color.Green);
            terra.AddKeyColour(.7f, Color.Green);
            terra.AddKeyColour(.8f, Color.Brown);
            terra.AddKeyColour(.9f, Color.Gray);
            terra.AddKeyColour(1f, Color.White);
            //System.IO.FileStream file = new System.IO.FileStream("./terra.png", System.IO.FileMode.Create);
            //terra.GetTexture(128).SaveAsPng(file, 128, 1);
            //file.Close();
            //file.Dispose();
            bodies.Add(new CelestialBody(Vector2.UnitX * sunSize * 15, 16, -Vector2.UnitY * 5, 5, new BodySettings.BodyTextureSettings(terra, 958346, 3)));
            //bodies.Add(new CelestialBody(Vector2.UnitX * sunSize * 15, 16, -Vector2.UnitY * 5, 5, new BodySettings.BodyTextureSettings(0.5f, Color.Green, Color.Blue, 958346, 3)));

            bodies.Add(new CelestialBody(ggPos, 128, -Vector2.UnitY * 1, 21, new BodySettings.BodyTextureSettings(0.26f, Color.GreenYellow, Color.Yellow, 987347)));
            bodies.Add(new CelestialBody(ggPos + Vector2.UnitX * 150, 10, -Vector2.UnitY * 27, 3, new BodySettings.BodyTextureSettings(Color.DarkGray)));
            bodies.Add(new CelestialBody(ggPos + Vector2.UnitX * 210, 8, -Vector2.UnitY * 22, 2, new BodySettings.BodyTextureSettings(Color.DarkGray)));
            bodies.Add(new CelestialBody(ggPos + Vector2.UnitX * 310, 15, -Vector2.UnitY * 19, 4, new BodySettings.BodyTextureSettings(Color.DarkGray)));
            bodies.Add(new CelestialBody(ggPos + Vector2.UnitX * 450, 6, -Vector2.UnitY * 13.6f, 1.75f, new BodySettings.BodyTextureSettings(Color.DarkGray)));

            return bodies;
        }

        private List<CelestialBody> PopulateUniverse()
        {
            List<CelestialBody> bodies = new List<CelestialBody>();
            //Celestial bodies go in here

            //bodies = System1();
            //bodies = ThreeBodies();
            bodies = System2();

            return bodies;
        }

        protected override void Initialize()
        {
            camera = new Camera2d();

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;

            graphics.ApplyChanges();

            ih = new InputHandler(this);
            ih.AddInputHandler(new PCInputHandler());

            stars = new Stars(1024, 1, .0005, .999f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hud = new SpriteBatch(GraphicsDevice);
            Calibri = Content.Load<SpriteFont>("Calibri");
        }

        protected override void Update(GameTime gameTime)
        {
            ih.Update();
            PCInputHandler pcih = ih.GetInputHandler<PCInputHandler>();

            if (pcih.GetKeyUp(Keys.Escape)) Exit();

            if (pcih.GetKeyUp(Keys.D0) &&-1 < universe.getBodies().Count) camera.bodyFocus =-1;
            if (pcih.GetKeyUp(Keys.D1) && 0 < universe.getBodies().Count) camera.bodyFocus = 0;
            if (pcih.GetKeyUp(Keys.D2) && 1 < universe.getBodies().Count) camera.bodyFocus = 1;
            if (pcih.GetKeyUp(Keys.D3) && 2 < universe.getBodies().Count) camera.bodyFocus = 2;
            if (pcih.GetKeyUp(Keys.D4) && 3 < universe.getBodies().Count) camera.bodyFocus = 3;
            if (pcih.GetKeyUp(Keys.D5) && 4 < universe.getBodies().Count) camera.bodyFocus = 4;
            if (pcih.GetKeyUp(Keys.D6) && 5 < universe.getBodies().Count) camera.bodyFocus = 5;

            if (pcih.GetKeyUp(Keys.Space)) isEnabled = !isEnabled;

            // TODO: Implement panning
            if (pcih.GetMButtonUp(MouseButtons.Left)) camera.offset += (ih.GetMousePos() - new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2)) / camera.Zoom;

            changeMouseWheel = pcih.GetMWheel() - lastMouseWheel;

            camera.Zoom += (float)changeMouseWheel / 120 / 10 * camera.Zoom;

            lastMouseWheel = pcih.GetMWheel();

            if (universe == null)
            {
                universe = new Universe(0);
                universe.setBodies(PopulateUniverse());
            }

            else if(isEnabled)
            {
                universe.Update();
            }

            //if (isEnabled)
            //{
            //    universe.setStep(1);
            //}
            //else
            //{
            //    universe.setStep(0);
            //}

            if (pcih.GetKeyUp(Keys.OemPlus)) universe.setStep(universe.getStep() + .25f);
            if (pcih.GetKeyUp(Keys.OemMinus) && universe.getStep() > 0) universe.setStep(universe.getStep() - .25f);

            if (camera.bodyFocus >= 0)
                camera.Pos = universe.getBodies()[camera.bodyFocus].getPos() + camera.offset;
            else camera.Pos = camera.offset;
            //camera.Zoom = .5f;

            // Endless
            const float max_pos = 51200f;
            if(camera.Pos.X > max_pos)
            {
                camera._pos.X -= max_pos;
                foreach(CelestialBody body in universe.getBodies())
                {
                    body.position.X -= max_pos;
                }
            }
            else if(camera.Pos.X < -max_pos)
            {
                camera._pos.X += max_pos;
                foreach(CelestialBody body in universe.getBodies())
                {
                    body.position.X += max_pos;
                }
            }

            if(camera.Pos.Y > max_pos)
            {
                camera._pos.Y -= max_pos;
                foreach(CelestialBody body in universe.getBodies())
                {
                    body.position.Y -= max_pos;
                }
            }
            else if(camera.Pos.Y < -max_pos)
            {
                camera._pos.Y += max_pos;
                foreach(CelestialBody body in universe.getBodies())
                {
                    body.position.Y += max_pos;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, null, null, null, camera.get_transformation(graphics));
            stars.Draw(camera, spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.get_transformation(graphics));
            universe.Draw(spriteBatch);
            spriteBatch.End();

            float curPosY = 0f;
            float step = 18f;
            hud.Begin();
            hud.DrawString(Calibri, "Time Passed: " + universe.timeElapsed, Vector2.Zero, Color.White);
            hud.DrawString(Calibri, "Time Step: " + universe.getStep(), new Vector2(0f, curPosY += step), Color.White);
            hud.DrawString(Calibri, "Camera Position X: " + camera.Pos.X, new Vector2(0f, curPosY += step), Color.White);
            hud.DrawString(Calibri, "Camera Position Y: " + camera.Pos.Y, new Vector2(0f, curPosY += step), Color.White);
            hud.DrawString(Calibri, "Camera Zoom: " + camera.Zoom, new Vector2(0f, curPosY += step), Color.White);
            hud.DrawString(Calibri, "Mouse Wheel: " + ih.GetInputHandler<PCInputHandler>().GetMWheel(), new Vector2(0f, curPosY += step), Color.White);
            //hud.DrawString(Calibri, "Last Key Press: " + InputHandler.lastKeyPressed(), new Vector2(0f, curPosY += step), Color.White);
            hud.End();

            base.Draw(gameTime);
        }
    }
}

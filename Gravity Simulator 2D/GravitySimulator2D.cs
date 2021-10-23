using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravitySimulator2D
{
    public static class ThingiesProvider
    {
        public static GraphicsDeviceManager graphics;
    }

    public class GravitySimulator2D : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch hud;
        Universe universe;
        SpriteFont Calibri;
        Camera2d camera;
        //Stars stars;

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

            bodies.Add(new CelestialBody(Vector2.Zero, 128, 50, Vector2.Zero, new BodySettings.BodyTextureSettings(0.26f, Color.Yellow, Color.Brown, 43983, 32)));
            bodies.Add(new CelestialBody(new Vector2(128, 0), 16, 4, -Vector2.UnitY * 2.2f, new BodySettings.BodyTextureSettings(0.47f, Color.Blue, Color.Red, 83473)));
            bodies.Add(new CelestialBody(new Vector2(412, 0), 22, 6, -Vector2.UnitY * 1.4f, new BodySettings.BodyTextureSettings(0.47f, Color.Gray, Color.Aqua, 65323, 3)));
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
            bodies.Add(new CelestialBody(Vector2.UnitX * sunSize * 15, 16, -Vector2.UnitY * 5, 5, new BodySettings.BodyTextureSettings(0.5f, Color.Green, Color.Blue, 958346, 3)));
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
            ThingiesProvider.graphics = graphics;
            camera = new Camera2d();

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;

            graphics.ApplyChanges();

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
            if (InputHandler.isEscPressed()) Exit();

            if (InputHandler.isZeroPressed() && -1 < universe.getBodies().Count) camera.bodyFocus = -1;
            if (InputHandler.isOnePressed() && 0 < universe.getBodies().Count) camera.bodyFocus = 0;
            if (InputHandler.isTwoPressed() && 1 < universe.getBodies().Count) camera.bodyFocus = 1;
            if (InputHandler.isThreePressed() && 2 < universe.getBodies().Count) camera.bodyFocus = 2;
            if (InputHandler.isFourPressed() && 3 < universe.getBodies().Count) camera.bodyFocus = 3;
            if (InputHandler.isFivePressed() && 4 < universe.getBodies().Count) camera.bodyFocus = 4;
            if (InputHandler.isSixPressed() && 5 < universe.getBodies().Count) camera.bodyFocus = 5;

            //isEnabled = InputHandler.detectSingleKeyPress(Keys.Space, isEnabled);

            if (InputHandler.isSpacePressed()) isEnabled = !isEnabled;

            if (InputHandler.isLeftPressed()) camera.offset = InputHandler.msGetPos() - new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            changeMouseWheel = InputHandler.msGetWheel() - lastMouseWheel;

            camera.Zoom += (float)changeMouseWheel / 120 / 10 * camera.Zoom;

            lastMouseWheel = InputHandler.msGetWheel();

            if (universe == null)
            {
                universe = new Universe(0);
                universe.setBodies(PopulateUniverse());
            }

            else
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

            if (InputHandler.isPlusPressed()) universe.setStep(universe.getStep() + .25f);
            if (InputHandler.isMinusPressed() && universe.getStep() > 0) universe.setStep(universe.getStep() - .25f);

            if (camera.bodyFocus >= 0)
                camera.Pos = universe.getBodies()[camera.bodyFocus].getPos() + camera.offset / camera.Zoom;
            else camera.Pos = camera.offset;
            //camera.Zoom = .5f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, camera.get_transformation(graphics));
            //stars.Draw(camera, spriteBatch);
            //spriteBatch.End();

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
            hud.DrawString(Calibri, "Mouse Wheel: " + InputHandler.msGetWheel(), new Vector2(0f, curPosY += step), Color.White);
            //hud.DrawString(Calibri, "Last Key Press: " + InputHandler.lastKeyPressed(), new Vector2(0f, curPosY += step), Color.White);
            hud.End();

            base.Draw(gameTime);
        }
    }
}

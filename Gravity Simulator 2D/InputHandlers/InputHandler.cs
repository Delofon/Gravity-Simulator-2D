using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GravitySimulator2D.InputHandlers
{
    // Also known as ih
    public sealed class InputHandler
    {
        private List<IInputHandler> inputHandlers;
        private GravitySimulator2D game;

        public InputHandler(GravitySimulator2D game)
        {
            inputHandlers = new List<IInputHandler>();
            this.game = game;
        }

        public void Update()
        {
            foreach(IInputHandler ih in inputHandlers)
            {
                ih.HandleInput(null); // TODO: shit
            }
        }

        public void AddInputHandler(IInputHandler ih)
        {
            if(inputHandlers.Contains(ih))
            {
                //Logger.Error($"ih: the requested IInputHandler \"{ih}\" to add already exists.");
                return;
            }    
            inputHandlers.Add(ih);
        }
        public bool RemoveInputHandler(IInputHandler ih)
        {
            return inputHandlers.Remove(ih);
        }
        public IInputHandler[] GetInputHandlers()
        {
            return inputHandlers.ToArray();
        }
        public T GetInputHandler<T>() where T : IInputHandler
        {
            return (T)inputHandlers.Find(x => x.GetType() == typeof(T));
        }
        
        /// <summary>
        /// Gets raw normalized mouse position from the engine.
        /// </summary>
        /// <returns>Mouse position in the window.</returns>
        public Vector2 GetMousePos()
        {
            PCInputHandler pcih = GetInputHandler<PCInputHandler>();

            if(pcih == null)
            {
                //Logger.Warning("ih: Cannot fetch mouse position: no PCInputHandler defined.");
                return Vector2.Zero;
            }

            Vector2 mousePos = pcih.GetMousePos();
            //mousePos.X /= GravitySimulator2D.graphics.PreferredBackBufferWidth;
            //mousePos.Y /= GravitySimulator2D.graphics.PreferredBackBufferHeight;

            return pcih.GetMousePos();
        }
        /// <summary>
        /// Sets raw normalized mouse position from the engine.
        /// </summary>
        /// <param name="newPosition">New mouse position in the window.</param>
        public void SetMousePos(Vector2 newPosition)
        {
            PCInputHandler pcih = GetInputHandler<PCInputHandler>();

            if (pcih == null)
            {
                //Logger.Error("ih: Cannot set mouse position: no PCInputHandler defined.");
                return;
            }
            if(!MathUtil.InRange(0, 1, newPosition.Y))
            {
                //Logger.Error($"ih: Cannot set mouse position: new mouse position Y is not normalized ({newPosition.Y})");
                return;
            }

            //newPosition.X *= GravitySimulator2D.graphics.PreferredBackBufferWidth;
            //newPosition.Y *= GravitySimulator2D.graphics.PreferredBackBufferHeight;

            pcih.SetMousePos(newPosition);
        }
    }
}

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

        public Vector2 GetMousePos()
        {
            PCInputHandler pcih = GetInputHandler<PCInputHandler>();

            if(pcih == null)
            {
                return Vector2.Zero;
            }

            Vector2 mousePos = pcih.GetMousePos();

            return pcih.GetMousePos();
        }
        /// <summary>
        /// Sets raw normalized mouse position from the engine.
        /// </summary>
        /// <param name="newPosition">New mouse position in the window.</param>
        public void SetMousePos(Vector2 newPosition)
        {
            PCInputHandler pcih = GetInputHandler<PCInputHandler>();
            pcih.SetMousePos(newPosition);
        }
    }
}

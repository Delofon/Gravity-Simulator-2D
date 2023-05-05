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
        private List<KeyBind> keyBinds;

        private List<string> input_evenzxts;

        private GravitySimulator2D game;

        public InputHandler(GravitySimulator2D game)
        {
            inputHandlers = new List<IInputHandler>();
            keyBinds = new List<KeyBind>();

            input_events = new List<string>();

            this.game = game;
        }

        public void Update()
        {
            input_events.Clear();
            KeyBind[] keyBindsArr = GetKeyBinds();
            foreach(IInputHandler ih in inputHandlers)
            {
                input_events.AddRange(ih.HandleInput(keyBindsArr)); // TODO: shit
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

        public void AddKeyBind(KeyBind keyBind)
        {
            if(keyBinds.Exists(x => x.name == keyBind.name))
            {
                //Logger.Error($"ih: the KeyBind with requested \"{keyBind.name}\" already exists.");
                return;
            }
            keyBinds.Add(keyBind);
        }
        public bool RemoveKeyBind(KeyBind keybind)
        {
            return keyBinds.Remove(keybind);
        }
        public bool RemoveKeyBind(string name)
        {
            return keyBinds.Remove(GetKeyBind(name));
        }
        public KeyBind GetKeyBind(string name)
        {
            return keyBinds.Find(x => x.name == name);
        }
        public KeyBind[] GetKeyBinds()
        {
            return keyBinds.ToArray();
        }

        public bool GetInputEvent(string input_event)
        {
            return input_events.Contains(input_event);
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
            mousePos.X /= game.graphics.PreferredBackBufferWidth;
            mousePos.Y /= game.graphics.PreferredBackBufferHeight;

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
                Logger.Error("ih: Cannot set mouse position: no PCInputHandler defined.");
                return;
            }
            if(!MathUtil.InRange(0, 1, newPosition.X))
            {
                Logger.Error($"ih: Cannot set mouse position: new mouse position X is not normalized ({newPosition.X})");
                return;
            }
            if(!MathUtil.InRange(0, 1, newPosition.Y))
            {
                Logger.Error($"ih: Cannot set mouse position: new mouse position Y is not normalized ({newPosition.Y})");
                return;
            }

            newPosition.X *= game.graphics.PreferredBackBufferWidth;
            newPosition.Y *= game.graphics.PreferredBackBufferHeight;

            pcih.SetMousePos(newPosition);
        }
    }
}

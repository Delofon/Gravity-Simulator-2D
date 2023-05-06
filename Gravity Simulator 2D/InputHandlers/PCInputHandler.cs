using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GravitySimulator2D.InputHandlers
{
    //TODO: Issue #40. Maybe separate mouse input handling from PCInputHandler
    public sealed class PCInputHandler : IInputHandler
    {
        KeyboardState curState;
        KeyboardState prevState;

        MouseState curMState;
        MouseState prevMState;

        public List<string> HandleInput(KeyBind[] keyBinds)
        {
            prevState = curState;
            curState = Keyboard.GetState();

            prevMState = curMState;
            curMState = Mouse.GetState();

            return null;
        }

        public bool GetKeyDown(Keys key)
        {
            if(curState.IsKeyDown(key) && prevState.IsKeyUp(key))
                return true;
            else
                return false;
        }

        public bool GetKeyHeld(Keys key)
        {
            return curState.IsKeyDown(key);
        }

        public bool GetKeyUp(Keys key)
        {
            if (curState.IsKeyUp(key) && prevState.IsKeyDown(key))
                return true;
            else
                return false;
        }

        public Vector2 GetMousePos()
        {
            return curMState.Position.ToVector2();
        }

        public void SetMousePos(Vector2 newPosition)
        {
            Mouse.SetPosition((int)newPosition.X, (int)newPosition.Y);
        }

        public bool GetMButtonHeld(MouseButtons button, MouseState? mouseState = null)
        {
            MouseState toCheckFrom = mouseState.HasValue ? mouseState.Value : curMState;

            switch(button)
            {
                case MouseButtons.None:
                    return false;

                case MouseButtons.Left:
                    return toCheckFrom.LeftButton == ButtonState.Pressed;

                case MouseButtons.Middle:
                    return toCheckFrom.MiddleButton == ButtonState.Pressed;

                case MouseButtons.Right:
                    return toCheckFrom.RightButton == ButtonState.Pressed;

                case MouseButtons.Forward:
                    return toCheckFrom.XButton1 == ButtonState.Pressed;

                case MouseButtons.Back:
                    return toCheckFrom.XButton2 == ButtonState.Pressed;

                default:
                    throw new ArgumentException("The provided mouse button code is invalid.");
                    //throw new Exception("What the?! Something happened that caused the input check to check for a non-existing mouse button.");
                    // this is an impossible situation, only some SEU shit or wrong method calls may cause this to happen
            }
        }

        public bool GetMButtonDown(MouseButtons button)
        {
            if (GetMButtonHeld(button) && !GetMButtonHeld(button, prevMState))
                return true;
            else
                return false;
        }

        public bool GetMButtonUp(MouseButtons button)
        {
            if (!GetMButtonHeld(button) && GetMButtonHeld(button, prevMState))
                return true;
            else
                return false;
        }

        public int GetMWheel()
        {
            return curMState.ScrollWheelValue;
        }
    }

    public enum MouseButtons
    {
        None,
        Left,
        Middle,
        Right,
        Forward,
        Back
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GravitySimulator2D.InputHandlers
{
    //LONG-TERM: Issue #6. Add GamepadInputHandler.
    /*public sealed class GPInputHandler : IInputHandler
    {
        GamePadState curState;
        GamePadState prevState;

        public void Update()
        {
            prevState = curState;
            curState = GamePad.GetState(PlayerIndex.One);
        }

        public bool GetButtonHeld(Buttons button)
        {
            return curState.IsButtonDown(button);
        }

        public bool GetButtonDown(Buttons button)
        {
            if (curState.IsButtonDown(button) && prevState.IsButtonUp(button))
                return true;
            else
                return false;
        }

        public bool GetButtonUp(Buttons button)
        {
            if (curState.IsButtonUp(button) && prevState.IsButtonDown(button))
                return true;
            else
                return false;
        }
    }*/
}

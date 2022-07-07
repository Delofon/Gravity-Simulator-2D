using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GravitySimulator2D
{
    static class InputHandler
    {
        //Keyboard
        static KeyboardState kb_state;
        static KeyboardState prev_kb_state;
        static MouseState    ms_state;

        ///<summary>
        ///Returns true if any key is down, false if otherwise.
        ///</summary>
        public static bool isAnyKeyPressed() //Previously: isStateChanged. isStateChanged is a fancy way of saying isAnyKeyPressed.
        {
            Keys[] keysarray = kb_state.GetPressedKeys();
            return keysarray.Length > 0;
            //KeyboardState new_kb_state = Keyboard.GetState();
            //return new_kb_state == kb_state; //This way is analogus to commented way.
            ///if (new_kb_state == kb_state) return false;
            ///else return true;
        }

        public static bool isZeroPressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.D0));
        }

        public static bool isOnePressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.D1));
        }

        public static bool isTwoPressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.D2));
        }

        public static bool isThreePressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.D3));
        }

        public static bool isFourPressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.D4));
        }

        /// <summary>
        /// Checks for a W key pressed.
        /// </summary>
        /// <returns>True if W is pressed, false otherwise.</returns>
        public static bool isWPressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.W)); //This way is analogus to commented way. The same applies to all below.
            //if (kb_state.IsKeyDown(Keys.W)) return true;
            //else return false;
        }

        /// <summary>
        /// Checks for a S key pressed.
        /// </summary>
        /// <returns>True if S is pressed, false otherwise.</returns>
        public static bool isSPressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.S));
            //if (kb_state.IsKeyDown(Keys.S)) return true;
            //else return false;
        }

        /// <summary>
        /// Checks for a A key pressed. I know, this summary looks a bit weird.
        /// </summary>
        /// <returns>True if S is pressed, false otherwise.</returns>
        public static bool isAPressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.A));
            //if (kb_state.IsKeyDown(Keys.A)) return true;
            //else return false;
        }

        /// <summary>
        /// Checks for a D key pressed.
        /// </summary>
        /// <returns>True if D is pressed, false otherwise.</returns>
        public static bool isDPressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.D));
            //if (kb_state.IsKeyDown(Keys.D)) return true;
            //else return false;
        }

        /// <summary>
        /// Checks for a Escape key pressed.
        /// </summary>
        /// <returns>True if Escape is pressed, false otherwise.</returns>
        public static bool isEscPressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.Escape));
            //if (kb_state.IsKeyDown(Keys.Escape)) return true;
            //else return false;
        }

        /// <summary>
        /// Checks for a F2 key pressed.
        /// </summary>
        /// <returns>True if F2 is pressed, false otherwise.</returns>
        public static bool isF2Pressed()
        {
            kb_state = Keyboard.GetState();
            return (kb_state.IsKeyDown(Keys.F2));
            //if (kb_state.IsKeyDown(Keys.F2)) return true;
            //else return false;
        }

        public static bool isSpacePressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.Space);
        }

        public static bool isLCtrlPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.LeftControl);
        }

        public static bool isUpAPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.Up);
        }

        public static bool isDownAPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.Down);
        }

        public static bool isLeftAPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.Left);
        }

        public static bool isRightAPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.Right);
        }

        public static bool isLShiftPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.LeftShift);
        }

        public static bool isLAltPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.LeftAlt);
        }

        public static Keys lastKeyPressed()
        {
            kb_state = Keyboard.GetState();
            if (0 < kb_state.GetPressedKeys().Length)
                return kb_state.GetPressedKeys()[0];
            else return Keys.A;
        }

        public static bool detectSingleKeyPress(Keys key, bool desiredElse)
        {
            prev_kb_state = kb_state;
            kb_state = Keyboard.GetState();
            if (kb_state.IsKeyDown(key) && prev_kb_state.IsKeyUp(key)) return true;
            else if (kb_state.IsKeyUp(key) && prev_kb_state.IsKeyDown(key)) return false;
            else return desiredElse;
        }

        public static bool isFivePressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.D5);
        }

        public static bool isPlusPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.OemPlus);
        }

        public static bool isMinusPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.OemMinus);
        }

        public static bool isSixPressed()
        {
            kb_state = Keyboard.GetState();
            return kb_state.IsKeyDown(Keys.D6);
        }

        //Mouse
        public static int msGetX()
        {
            ms_state = Mouse.GetState();
            return ms_state.X;
        }

        public static void msSetX(int x)
        {
            Mouse.SetPosition(x, msGetY());
        }

        public static int msGetY()
        {
            ms_state = Mouse.GetState();
            return ms_state.Y;
        }

        public static void msSetY(int y)
        {
            Mouse.SetPosition(msGetX(), y);
        }

        public static Vector2 msGetPos()
        {
            ms_state = Mouse.GetState();
            return new Vector2(ms_state.X, ms_state.Y);
        }

        public static void msSetPos(int x, int y)
        {
            Mouse.SetPosition(x, y);
        }

        public static void msSetPos(Vector2 pos)
        {
            Mouse.SetPosition((int)pos.X, (int)pos.Y);
        }

        public static bool isLeftPressed()
        {
            ms_state = Mouse.GetState();
            return ButtonState.Pressed == ms_state.LeftButton;
        }

        public static bool isRightPressed()
        {
            ms_state = Mouse.GetState();
            return ButtonState.Pressed == ms_state.RightButton;
        }

        public static bool isMiddlePressed()
        {
            ms_state = Mouse.GetState();
            return ButtonState.Pressed == ms_state.MiddleButton;
        }

        public static bool isMouseFourPressed()
        {
            ms_state = Mouse.GetState();
            return ButtonState.Pressed == ms_state.XButton1;
        }

        public static bool isMouseFivePressed()
        {
            ms_state = Mouse.GetState();
            return ButtonState.Pressed == ms_state.XButton2;
        }
        
        public static int msGetWheel()
        {
            ms_state = Mouse.GetState();
            return ms_state.ScrollWheelValue;
        }
    }
}

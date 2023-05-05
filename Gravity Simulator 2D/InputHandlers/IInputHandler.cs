using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace GravitySimulator2D.InputHandlers
{
    public interface IInputHandler
    {
        List<string> HandleInput(KeyBind[] keyBinds);
    }
}

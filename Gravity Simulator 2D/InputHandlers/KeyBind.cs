using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace GravitySimulator2D.InputHandlers
{
    public class KeyBind : IEquatable<KeyBind>
    {
        public string name;

        public Keys key;
        public Keys aux_key;
        // Neither Buttons or MouseButtons enums have a None element. I'll leave this up for future, for now KeyBinds are only available for keyboards.
        //public Buttons button;
        //public MouseButtons mouseButton;

        public HoldType holdType;

        public KeyBind(string name, Keys key = Keys.None, Keys aux_key = Keys.None, HoldType holdType = HoldType.Held)
        {
            this.name = name;
            this.key = key;
            this.aux_key = aux_key;
            this.holdType = holdType;
        }

        public override bool Equals(object obj)
        {
            KeyBind keyBind = obj as KeyBind;
            if(keyBind != null)
            {
                return Equals(keyBind);
            }
            return false;
        }
        public bool Equals(KeyBind keyBind)
        {
            return keyBind.name == name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public static implicit operator string(KeyBind keyBind)
        {
            return keyBind.name;
        }
        public static explicit operator KeyBind(string name)
        {
            return new KeyBind(name);
        }
    }

    public enum HoldType
    {
        Held,
        Down,
        Up
    }
}

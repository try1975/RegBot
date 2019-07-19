using System;

namespace Common.Service
{
    public class EnumLocalizeAttribite : Attribute
    {
        public readonly string Text;

        public EnumLocalizeAttribite(string text)
        {
            Text = text;
        }

    }
}
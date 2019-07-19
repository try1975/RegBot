using System;

namespace Common.Service
{
    public class EnumLocalizeAttribute : Attribute
    {
        public readonly string Text;

        public EnumLocalizeAttribute(string text)
        {
            Text = text;
        }

    }
}
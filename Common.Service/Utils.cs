using System;
using System.Reflection;

namespace Common.Service
{
    public class Utils
    {
        public static string GetDescription(Enum en)
        {
            if (en == null) return string.Empty;
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumLocalizeAttribite), false);
                if (attrs != null && attrs.Length > 0) 
                    return ((EnumLocalizeAttribite)attrs[0]).Text;
            }
            return en.ToString();
        }
    }
}
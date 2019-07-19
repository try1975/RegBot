using System;

namespace Common.Service
{
    public static class Utils
    {
        public static string GetDescription(Enum en)
        {
            if (en == null) return string.Empty;
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            if (memInfo.Length <= 0) return en.ToString();
            var attrs = memInfo[0].GetCustomAttributes(typeof(EnumLocalizeAttribute), false);
            return attrs.Length > 0 ? ((EnumLocalizeAttribute)attrs[0]).Text : en.ToString();
        }
    }
}
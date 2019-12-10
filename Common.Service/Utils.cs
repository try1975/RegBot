using System;
using System.IO;
using System.Windows.Forms;

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

        public static void SaveLinesToFile(string[] lines)
        {
            var saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveDialog.FileName, lines);
            }
        }
    }
}
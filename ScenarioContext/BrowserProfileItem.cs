using System.Collections.Generic;
using System.Linq;

namespace ScenarioContext
{
    public class BrowserProfileItem
    {
        public string Text { get; set; }
        public string Folder { get; set; }

        public static List<BrowserProfileItem> GetItems(IBrowserProfileService browserProfileService)
        {
            var browserProfileItems = browserProfileService
                    .GetBrowserProfiles()
                    .Select(x => new BrowserProfileItem
                    {
                        Text = x.Name,
                        Folder = x.Folder
                    })
                    .ToList();
            browserProfileItems.Insert(0, new BrowserProfileItem { Text = "Создать новый профиль" });
            browserProfileItems.Insert(0, new BrowserProfileItem { Text = "Не использовать профиль" });
            return browserProfileItems;
        }
}
}

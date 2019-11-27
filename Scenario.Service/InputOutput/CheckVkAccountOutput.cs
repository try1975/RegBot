using System;

namespace ScenarioService
{
    public class CheckVkAccountOutput
    {
        private string vkAccountName;

        public string VkAccountName
        {
            get { return vkAccountName; }
            set
            {
                vkAccountName = value;
                VkAccountUrl = $"https://vk.com/{vkAccountName}";
                CheckDate = DateTime.Now;
            }
        }
        public bool Available { get; set; }

        public string VkAccountUrl { get; private set; }

        public DateTime CheckDate { get; private set; }
    }
}

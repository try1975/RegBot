﻿using System;

namespace ScenarioService
{
    public class CheckVkAccountOutput
    {
        private string _accountName;

        public string AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                AccountUrl = $"https://vk.com/{_accountName}";
                CheckDate = DateTime.Now;
            }
        }
        public bool Available { get; set; }

        public string AccountUrl { get; private set; }

        public DateTime CheckDate { get; private set; }
    }
}

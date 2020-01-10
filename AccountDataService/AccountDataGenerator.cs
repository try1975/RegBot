using Common.Service.Enums;
using Common.Service.Interfaces;
using System;
using System.IO;
using System.Text;

namespace AccountData.Service
{
    public class AccountDataGenerator : IAccountDataGenerator
    {
        private readonly string _path;
        private readonly Random random;

        public AccountDataGenerator(string path)
        {
            if (string.IsNullOrEmpty(path)) path = Environment.CurrentDirectory;
            _path = path;
            random = new Random();
        }
        public IAccountData GetRandom(CountryCode countryCode = CountryCode.EN)
        {
            IAccountData accountData = new EmailAccountData { Sex = SexCode.Male };

            if (random.NextDouble() > 0.5) accountData.Sex = SexCode.Female;
            var sex = Enum.GetName(typeof(SexCode), accountData.Sex);

            accountData.BirthDate = GetBirthDate(random);

            var lang = Enum.GetName(typeof(CountryCode), CountryCode.EN);
            if (countryCode == CountryCode.RU) lang = Enum.GetName(typeof(CountryCode), CountryCode.RU);

            var path = Path.Combine(_path, "Data");

            var filename = Path.Combine(path, $"{nameof(IAccountData.Firstname)}{sex}{lang}.txt");
            var firstnameList = File.ReadAllLines(filename);
            accountData.Firstname = firstnameList[random.Next(firstnameList.Length)];

            filename = Path.Combine(path, $"{nameof(IAccountData.Lastname)}{sex}{lang}.txt");
            var lastnameList = File.ReadAllLines(filename);
            accountData.Lastname = lastnameList[random.Next(lastnameList.Length)];

            accountData.Password = CreatePassword(10);
            return accountData;
        }


        public IAccountData GetRandomMale(CountryCode countryCode)
        {
            IAccountData accountData = new EmailAccountData { Sex = SexCode.Male };

            var sex = Enum.GetName(typeof(SexCode), accountData.Sex);

            accountData.BirthDate = GetBirthDate(random);
            accountData.Password = CreatePassword(10);
            return accountData;
        }

        public IAccountData GetRandomFemale(CountryCode countryCode)
        {
            IAccountData accountData = new EmailAccountData { Sex = SexCode.Female };

            var sex = Enum.GetName(typeof(SexCode), accountData.Sex);

            accountData.BirthDate = GetBirthDate(random);
            accountData.Password = CreatePassword(10);
            return accountData;
        }

        private static DateTime GetBirthDate(Random random)
        {
            var year = DateTime.Today.Year - random.Next(19, 40);
            var month = random.Next(1, 12);
            int dayCount;
            switch (month)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    dayCount = 30;
                    break;
                case 2:
                    dayCount = 28;
                    break;
                default:
                    dayCount = 31;
                    break;
            }
            var day = random.Next(1, dayCount);
            return new DateTime(year, month, day);
        }

        private string CreatePassword(int length)
        {
            // ReSharper disable once StringLiteralTypo
            const string valid = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";
            var res = new StringBuilder();
            while (0 < length--)
            {
                res.Append(valid[random.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
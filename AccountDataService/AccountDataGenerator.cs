using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Text;
using Common.Service;

namespace AccountData.Service
{
    public class AccountDataGenerator : IAccountDataGenerator
    {
        private readonly string _path;

        public AccountDataGenerator(string path)
        {
            if (string.IsNullOrEmpty(path)) path = Environment.CurrentDirectory;
            _path = path;
        }
        public IAccountData GetRandom(CountryCode countryCode = CountryCode.EN)
        {
            IAccountData accountData = new EmailAccountData { Sex = SexEnum.Male };
            var random = new Random();
            if (random.NextDouble() > 0.5) accountData.Sex = SexEnum.Female;
            var sex = Enum.GetName(typeof(SexEnum), accountData.Sex);

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
            IAccountData accountData = new EmailAccountData { Sex = SexEnum.Male };
            var random = new Random();
            var sex = Enum.GetName(typeof(SexEnum), accountData.Sex);

            accountData.BirthDate = GetBirthDate(random);
            accountData.Password = CreatePassword(10);
            return accountData;
        }

        public IAccountData GetRandomFemale(CountryCode countryCode)
        {
            IAccountData accountData = new EmailAccountData { Sex = SexEnum.Female };
            var random = new Random();
            var sex = Enum.GetName(typeof(SexEnum), accountData.Sex);

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

        private static string CreatePassword(int length)
        {
            // ReSharper disable once StringLiteralTypo
            const string valid = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
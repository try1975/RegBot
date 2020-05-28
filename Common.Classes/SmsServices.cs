using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using GetSmsOnline;
using Newtonsoft.Json;
using OnlineSimRu;
using SimSmsOrg;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Classes
{
    public class SmsServices : ISmsServices
    {
        private bool _smsServiceInfoListInitialized;
        private readonly List<SmsServiceInfo> _smsServiceInfoList = new List<SmsServiceInfo>();
        private readonly Dictionary<SmsServiceCode, ISmsService> _smsServiceDictionary = new Dictionary<SmsServiceCode, ISmsService>();
        private readonly string _path;

        public SmsServices(string path)
        {
            _path = path;
            foreach (SmsServiceCode serviceCode in Enum.GetValues(typeof(SmsServiceCode)))
            {
                switch (serviceCode)
                {
                    case SmsServiceCode.GetSmsOnline:
                        _smsServiceDictionary[SmsServiceCode.GetSmsOnline] = new GetSmsOnlineApi();
                        break;
                    case SmsServiceCode.OnlineSimRu:
                        _smsServiceDictionary[SmsServiceCode.OnlineSimRu] = new OnlineSimRuApi();
                        break;
                    case SmsServiceCode.SimSmsOrg:
                        _smsServiceDictionary[SmsServiceCode.SimSmsOrg] = new SimSmsOrgApi();
                        break;
                }
            }
        }
        public ISmsService GetSmsService(SmsServiceCode smsServiceCode)
        {
            return _smsServiceDictionary[smsServiceCode];
        }

        public async Task<IEnumerable<SmsServiceInfo>> GetServiceInfoList(ServiceCode serviceCode)
        {
            if (!_smsServiceInfoListInitialized) await InitializeSmsServiceInfoList();
            return _smsServiceInfoList
                    .Where(z => z.ServiceCode == serviceCode && z.NumberCount > 0 && !z.Skiped)
                    .OrderBy(z => z.FailCount)
                    .ThenBy(z => z.Price)
                    .ThenByDescending(z => z.NumberCount)
                    .ToList();
        }

        public async Task<IEnumerable<SmsServiceInfo>> GetServiceInfoList(SmsServiceInfoCondition smsServiceInfoCondition)
        {
            if (!_smsServiceInfoListInitialized) await InitializeSmsServiceInfoList();
            var smsServiceInfoList = _smsServiceInfoList
                    .Where(z => z.ServiceCode == smsServiceInfoCondition.ServiceCode && z.NumberCount > 0 && !z.Skiped)
                    .OrderBy(z => z.FailCount)
                    .ThenBy(z => z.Price)
                    .ThenByDescending(z => z.NumberCount)
                    .ToList();
            if ((smsServiceInfoCondition.CountryCodes != null) && smsServiceInfoCondition.CountryCodes.Any())
            {
                smsServiceInfoList = smsServiceInfoList
                    .Where(z => smsServiceInfoCondition.CountryCodes.Contains(z.CountryCode))
                    .ToList();
            }
            return smsServiceInfoList;
        }

        private async Task InitializeSmsServiceInfoList()
        {
            _smsServiceInfoList.Clear();
            var path = Path.Combine(_path, "Data", "SmsServiceInfo.json");

            if (File.Exists(path))
            {
                var time = File.GetLastWriteTime(path).AddHours(4);
                if (time > DateTime.Now)
                {
                    _smsServiceInfoList.AddRange(JsonConvert.DeserializeObject<List<SmsServiceInfo>>(File.ReadAllText(path)));
                    _smsServiceInfoListInitialized = true;
                    return;
                }
            }
            foreach (KeyValuePair<SmsServiceCode, ISmsService> entry in _smsServiceDictionary)
            {
                var smsServiceInfoList = await _smsServiceDictionary[entry.Key].GetInfo();
                foreach (var smsServiceInfo in smsServiceInfoList)
                {
                    if (_smsServiceInfoList.Any(z => z.SmsServiceCode == smsServiceInfo.SmsServiceCode
                            && z.ServiceCode == smsServiceInfo.ServiceCode && z.CountryCode == smsServiceInfo.CountryCode)) continue;
                    _smsServiceInfoList.Add(smsServiceInfo);
                }
                //_smsServiceInfoList.AddRange(smsServiceInfoList);
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(_smsServiceInfoList));
            _smsServiceInfoListInitialized = true;
        }

        public void RemoveSmsServiceLowBalance(SmsServiceCode smsServiceCode)
        {
            _smsServiceInfoList.RemoveAll(z => z.SmsServiceCode == smsServiceCode);
        }

        public async Task AddFail(SmsServiceInfo smsServiceInfo)
        {
            //smsServiceInfo.FailCount++;
            //smsServiceInfo.NumberCount--;
        }


    }
}
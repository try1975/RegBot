using System;
using Common.Service.Enums;
using Common.Service.Interfaces;

namespace RegBot.Db.Entities
{
    public class AccountDataEntity : IEntity<int>, ITrackedEntity, IAccountData
    {

        public int Id { get; set; }
        public string ChangeBy { get; set; }
        public DateTime? ChangeAt { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public SexCode Sex { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string PhoneCountryCode { get; set; }
        public string Phone { get; set; }
        public bool Success { get; set; }
        public string ErrMsg { get; set; }
    }
}
using System;
using Common.Service.Enums;

namespace Common.Service.Interfaces
{
    public interface IAccountData
    {
        int Id { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        DateTime BirthDate { get; set; }
        SexCode Sex { get; set; }
        string AccountName { get; set; }
        string Password { get; set; }
        string Domain { get; set; }
        string PhoneCountryCode { get; set; }
        string Phone { get; set; }
        bool Success { get; set; }
        string ErrMsg { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
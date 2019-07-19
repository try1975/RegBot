using System;

namespace AccountData.Service
{
    public interface IAccountData
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        DateTime BirthDate { get; set; }
        SexEnum Sex { get; set; }
        string AccountName { get; set; }
        string Password { get; set; }
        string Domain { get; set; }
        string PhoneCountryCode { get; set; }
        string Phone { get; set; }
        bool Success { get; set; }
        string ErrMsg { get; set; }
    }
}
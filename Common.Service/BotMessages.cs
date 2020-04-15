﻿namespace Common.Service
{
    public static class BotMessages
    {
        public const string NoPhoneNumberMessage = "No get phone from service";
        public const string PhoneNumberNotAcceptMessage = "No phone number accepted by mail service";
        public const string PhoneNumberNotRecieveSms = "No phone number recieve sms";
        public static string[] BadNumber = new[] { NoPhoneNumberMessage, PhoneNumberNotAcceptMessage, PhoneNumberNotRecieveSms };
    }
}
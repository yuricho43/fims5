﻿namespace Fims5.Data
{
    public class ModelConstants
    {
        public class Common
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 20;
        }

        public class Identity
        {
            public const int MinUserNameLength = 3;
            public const int MaxUserNameLength = 20;
            public const int MinEmailLength = 3;
            public const int MaxEmailLength = 50;
            public const int MinPasswordLength = 6;
            public const int MinRoleNameLength = 3;
            public const int MaxRoleNameLength = 20;
        }

        public class Address
        {
            public const int MaxCountryLength = 255;
            public const int MaxCityLength = 255;
            public const int MaxStateLength = 255;
            public const int MaxDescriptionLength = 1000;
            public const int MaxPostalCodeLength = 10;
            public const int MinPhoneNumberLength = 5;
            public const int MaxPhoneNumberLength = 20;
            public const string PhoneNumberRegularExpression = @"\+[0-9]*";
        }

        public class TItem
        {
            public const int MinQuantity = 1;
            public const int MaxQuantity = int.MaxValue;
            public const int MaxUrlLength = 2048;
            public const int MaxDescriptionLength = 1000;
            public const string MinPrice = "1";
            public const string MaxPrice = "100000";
        }
    }
}
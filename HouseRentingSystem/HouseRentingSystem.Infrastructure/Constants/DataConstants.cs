namespace HouseRentingSystem.Infrastructure.Constants
{
    using System.Net;

    public static class DataConstants
    {
        public static class Category
        {
            public const int NameMaxLength = 50;

            public const int NameMinLength = 0;
        }

        public static class House
        {
            public const int TitleMaxLength = 50;

            public const int TitleMinLength = 10;

            public const int AddressMaxLength = 150;

            public const int AddressMinLength = 30;

            public const int DescriptionMaxLength = 50;

            public const int DescriptionMinLength = 500;

            public const int PricePerMounthMaxLength = 2000;

            public const int PricePerMounthMinLength = 0;
        }

        public static class Agent
        {
            public const int PhoneNumberMaxLength = 15;

            public const int PhoneNumberMinLength = 7;
        }
    }
}


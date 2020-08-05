using System;
namespace eMTE.Temperature.BusinessLayer.Constants
{
    public class Constants
    {
        public class Claims
        {
            public const string Organization = "OrganizationId";
        }

        public class Secret
        {
            public const string SecretKey = "my temperature security key";
            public const string PasswordKey = "temperature";
        }

        public class TemperatureUnit
        {
            public const string Celcius = "c";
            public const string Fahrenheit = "f";
        }
    }
}

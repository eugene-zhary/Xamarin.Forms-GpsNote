namespace GpsNote
{
    public static class Constants
    {
        public static class Navigation
        {
            public const string SELECTED_PIN = nameof(SELECTED_PIN);
        }

        public static class GpsRest
        {
            public const string BASE_URL = "http://gpsnoteapicontainer.westeurope.azurecontainer.io/api";
        }

        public static class Database
        {
            public const string DOCUMENT_PATH = "GpsNote.db";

            public const string USERS_TABLE_NAME = "Users";

            public const string PINS_TABLE_NAME = "Pins";
        }

        public static class WeatherRest
        {
            public const string API_KEY = "b38b0666ec3594a828ac3b8ac707684f";

            public const string BASE_URL = "https://api.openweathermap.org/data/2.5/weather";
        }
    }
}

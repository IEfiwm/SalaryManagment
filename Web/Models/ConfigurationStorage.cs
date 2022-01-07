using Microsoft.Extensions.Configuration;

namespace Web.Models
{
    public static class ConfigurationStorage
    {
        public static IConfiguration Configuration { private get; set; }

        public static string GetValue(string key)
        {
            return GetValue<string>(key);
        }

        public static T GetValue<T>(string key)
        {
            return Configuration.GetValue<T>(key);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace afdbox
{
    internal class Config
    {
        public static string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "afdbox");

        public static string? Get(string key)
        {
            try
            {
                return File.ReadAllText(Path.Combine(configPath, $"{key}.txt"));
            }
            catch (Exception)
            {
            }

            return null;
        }

        public static void Set(string key, string value)
        {
            if(!Directory.Exists(configPath)) Directory.CreateDirectory(configPath);

            File.WriteAllText(Path.Combine(configPath, $"{key}.txt"), value);
        }
    }
}

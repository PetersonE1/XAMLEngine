using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XAMLEngine
{
    public static class Persistents
    {
        private static Dictionary<string, string> data;

        public static void Initialize()
        {
            data = new Dictionary<string, string>();
            if (!File.Exists("save_data.jc"))
                return;
            string raw_data = File.ReadAllText("save_data.jc");
            if (raw_data != null && raw_data.Length > 0)
            {
                foreach (string[] entry in raw_data.Split(';').Select(s => s.Split(',')).ToArray())
                {
                    data.Add(entry[0], entry[1]);
                }
            }
        }

        public static void OnQuit(object sender, ExitEventArgs e)
        {
            string raw_data = string.Join(';', data.Select(n => $@"{n.Key},{n.Value}").ToArray());
            File.WriteAllText("save_data.jc", raw_data);
        }

        public static void SetValue(string key, string value)
        {
            if (data.ContainsKey(key))
            {
                data[key] = value;
                return;
            }
            data.Add(key, value);
        }

        public static string GetValue(string key, string defaultValue)
        {
            if (data.ContainsKey(key))
                return data[key];
            return defaultValue;
        }
    }
}

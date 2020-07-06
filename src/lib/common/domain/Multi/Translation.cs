using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Threading;
using Common.Domain.Multi;
using System;

namespace Common.Domain.Multi
{
    public static class Translation
    {        
        public static string Key(string key, bool defaultLanguage = false)
        {
            return ParseTranslation(key, defaultLanguage);
        }

        public static string Key(string key, bool defaultLanguage = false, params object[] values)
        {
            var message = ParseTranslation(key, defaultLanguage);
            return string.Format(message, values);
        }

        private static string ParseTranslation(string key, bool defaultLanguage)
        {   
            var culture = defaultLanguage ? "en-US" : Thread.CurrentThread.CurrentCulture.Name;
            var file = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Resources/{culture}.json";
            var raw = File.ReadAllText(file);
            var json = JObject.Parse(raw);
            var keys = key.Split('.');

            if (keys.Length != 2)
            {
                throw new ArgumentException("Translation keys have a 'CATEGORY.KEY' format.");
            }

            try
            {
                return json[keys[0]][keys[1]].ToString();
            }
            catch
            {
                throw new ArgumentException($"Key {key} does not exists");
            }
        }
    }
}

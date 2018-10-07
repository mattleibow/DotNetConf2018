using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace ClassLibrary
{
    public static class TheLibrary
    {
        public static void DoMagic(string[] args)
        {
            // create a word and punctuation list
            var list = new List<string>();
            list.Add("Hello");
            list.Add(" ");

#if NETFRAMEWORK
            list.Add(".NET Framework");
#elif NETSTANDARD
            list.Add(".NET Standard");
#else
            throw new PlatformNotSupportedException();
#endif

            list.Add(" ");
            list.Add("World");
            list.Add("!");
            list.Add(Environment.NewLine);

            // write some text
            foreach (string item in list)
            {
                // do we want to use upper case?
                var tweaked = UseUpperCase()
                    ? item.ToUpperInvariant()
                    : item;

                // write
                Console.Write(tweaked);
            }
        }

        private static bool UseUpperCase()
        {
            // check the config file
            if (File.Exists("config.json"))
            {
                var file = File.ReadAllText("config.json");
                var json = JObject.Parse(file);
                if (json.TryGetValue("useUpperCase", out var value))
                    return (bool)value;
            }

            // check the registry (we love Notepad)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
#pragma warning disable PC001 // API not supported on all platforms
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Notepad"))
                {
                    return (int)key?.GetValue("StatusBar") == 1;
                }
#pragma warning restore PC001 // API not supported on all platforms
            }

            return false;
        }
    }
}

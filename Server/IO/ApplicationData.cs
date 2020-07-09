using Computer_Wifi_Remote_Library;
using System;
using System.IO;

namespace Server.IO
{
    public static class ApplicationData
    {
        public static string GetPath(string fileName)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(directory, Constants.ORGANIZATION_NAME, Constants.APP_NAME, fileName);
        }
        public static void Write(byte[] bytes, string fileName)
        {
            var path = GetPath(fileName);
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fs = File.Create(path))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public static byte[] Read(string fileName)
        {
            var path = GetPath(fileName);
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return File.ReadAllBytes(path);
        }
    }
}

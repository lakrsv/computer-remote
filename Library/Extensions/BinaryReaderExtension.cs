﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Computer_Wifi_Remote_Library.Extensions
{
    public static class BinaryReaderExtension
    {
        public static byte[] ReadAllBytes(this BinaryReader reader)
        {
            const int bufferSize = 4096;
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, count);
                return ms.ToArray();
            }

        }
    }
}

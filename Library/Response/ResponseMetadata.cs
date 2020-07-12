using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Computer_Wifi_Remote_Library.Response
{
    public class ResponseMetadata
    {
        public Type Source { get; private set; }

        public ResponseMetadata(Type source)
        {
            Source = source;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

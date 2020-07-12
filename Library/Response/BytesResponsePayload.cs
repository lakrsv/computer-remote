using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Extensions;
using Computer_Wifi_Remote_Library.Response;
using System;
using System.IO;

namespace Computer_Wifi_Remote_Library
{
    public class BytesResponsePayload : IResponsePayload<byte[]>
    {
        public bool IsSuccess { get; private set; }
        public byte[] Payload { get; private set; }
        public ResponseMetadata Metadata { get; private set; }

        public BytesResponsePayload(bool isSuccess, byte[] payload, ResponseMetadata metadata)
        {
            IsSuccess = isSuccess;
            Payload = payload;
            Metadata = metadata;
        }

        public byte[] Serialize()
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(IsSuccess);
            writer.Write(Metadata.Source.FullName);
            writer.Write(Payload);
            return ms.ToArray();
        }

        public static BytesResponsePayload Deserialize(byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            using var reader = new BinaryReader(ms);

            var isSuccess = reader.ReadBoolean();
            var metadata = new ResponseMetadata(Type.GetType(reader.ReadString()));
            var payload = reader.ReadAllBytes();

            return new BytesResponsePayload(isSuccess, payload, metadata);
        }

        public static BytesResponsePayload NoPayloadSuccess(ResponseMetadata metadata)
        {
            return new BytesResponsePayload(true, null, metadata);
        }

        public static BytesResponsePayload NoPayloadFailure(ResponseMetadata metadata)
        {
            return new BytesResponsePayload(false, null, metadata);
        }

        public static BytesResponsePayload PayloadSuccess(byte[] payload, ResponseMetadata metadata)
        {
            return new BytesResponsePayload(true, payload, metadata);
        }

        public static BytesResponsePayload PayloadFailure(byte[] payload, ResponseMetadata metadata)
        {
            return new BytesResponsePayload(true, payload, metadata);
        }
    }
}

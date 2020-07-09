#define LOCAL

using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Connection;
using Newtonsoft.Json;
using QRCoder;
using Server.Http;
using System;
using System.Security.Cryptography;
using WebSocketSharp.Server;

namespace Server.Security
{
    public class Authentication
    {
        private readonly string TOKEN_ENVIRONMENT_KEY = $"{Constants.ORGANIZATION_NAME}_{Constants.APP_NAME}";
        private PngByteQRCode connectionQRCode;


        private bool HasToken()
        {
            return Environment.GetEnvironmentVariable(TOKEN_ENVIRONMENT_KEY) != null;
        }

        private string LoadToken()
        {
            if (!HasToken())
            {
                throw new InvalidOperationException("No token exists, please create a token prior to attempting to load it.");
            }
            return Environment.GetEnvironmentVariable(TOKEN_ENVIRONMENT_KEY);
        }

        private string CreateToken()
        {
            byte[] bytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            string hexToken = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            Environment.SetEnvironmentVariable(TOKEN_ENVIRONMENT_KEY, hexToken);

            return hexToken;
        }

        public void SetAuthentication(WebSocketServer webSocketServer)
        {
            var password = HasToken() ? LoadToken() : CreateToken();
            var connectionPayload = new ConnectionPayload("wss", IpUtils.GetLocalIpAddress(), 34198, "user", password);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(JsonConvert.SerializeObject(connectionPayload), QRCodeGenerator.ECCLevel.Q);
            connectionQRCode = new PngByteQRCode(qrCodeData);

            webSocketServer.AuthenticationSchemes = WebSocketSharp.Net.AuthenticationSchemes.Basic;
            webSocketServer.UserCredentialsFinder = (id) =>
            {
#if LOCAL
                return new WebSocketSharp.Net.NetworkCredential("username", "password");
#else
                return id.Name == connectionPayload.AuthenticationUsername ?
                new WebSocketSharp.Net.NetworkCredential(connectionPayload.AuthenticationUsername, connectionPayload.AuthenticationPassword) :
                null;
#endif
            };
        }

        public PngByteQRCode GetConnectionQRCode()
        {
            if (connectionQRCode == null)
            {
                throw new InvalidOperationException("No connection QR code has been generated. Call SetAuthentication first.");
            }
            return connectionQRCode;
        }
    }
}

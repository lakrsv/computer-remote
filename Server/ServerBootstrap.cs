using QRCoder;
using Server.Http;
using Server.Security;
using WebSocketSharp.Server;
namespace Server
{
    public class ServerBootstrap
    {
        public PngByteQRCode ConnectionCode { get; private set; }
        private WebSocketServer webSocketServer;

        public ServerBootstrap()
        {
            webSocketServer = new WebSocketServer(34198, true);
            webSocketServer.AddWebSocketService<CommandService>("/command");
            var certificateStore = CertificateStore.HasCertificate() ?
                CertificateStore.LoadCertificate() :
                CertificateStore.CreateCertificate();
            webSocketServer.SslConfiguration.ServerCertificate = certificateStore.Certificate;
            var authentication = new Authentication();
            authentication.SetAuthentication(webSocketServer);
            ConnectionCode = authentication.GetConnectionQRCode();
        }

        public void Start()
        {
            webSocketServer.Start();
        }

        public void Stop()
        {
            webSocketServer.Stop();
        }

        //[DllImport("kernel32.dll")]
        //static extern IntPtr GetConsoleWindow();

        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //const int SW_HIDE = 0;
        //const int SW_SHOW = 5;
    }
}

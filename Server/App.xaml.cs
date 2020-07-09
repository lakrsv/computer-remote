using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Server
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private ServerBootstrap server;
        private NotifyIcon notifyIcon;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            server.Stop();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            server = new ServerBootstrap();
            server.Start();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("Resources/icons8-check-all-64.ico");
            notifyIcon.Visible = true;
            notifyIcon.Text = "Server running!";

            var menuStrip = new ContextMenuStrip();
            var help = new ToolStripMenuItem("Help");
            help.Name = "Connect";
            var exit = new ToolStripMenuItem("Exit");
            exit.Name = "Exit";
            help.Click += Connect_Click;
            exit.Click += (o, args) => Current.Shutdown(0);

            menuStrip.Items.Add(help);
            menuStrip.Items.Add(exit);

            notifyIcon.ContextMenuStrip = menuStrip;
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                form.Text = "Connect";

                Bitmap qrCodeImage;
                using (var ms = new MemoryStream(server.ConnectionCode.GetGraphic(20)))
                {
                    qrCodeImage = new Bitmap(ms);
                }

                var label = new System.Windows.Forms.Label()
                {
                    Text = "Scan the QR code with your mobile application\nto remote control this device",
                    AutoSize = true,
                };

                var pictureBox = new PictureBox()
                {
                    Image = qrCodeImage,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill,
                };
                var panel = new System.Windows.Forms.TableLayoutPanel()
                {
                    Padding = new Padding(5),
                    Dock = DockStyle.Fill
                };
                panel.Controls.Add(label);
                panel.Controls.Add(pictureBox);
                form.Controls.Add(panel);
                form.ShowDialog();
            }
        }

        private StackPanel CreateUI(string imagePath, string username)
        {
            StackPanel userStack = new StackPanel()
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Margin = new Thickness(36, 24, 0, 0)
            };

            System.Windows.Controls.Image qrCode = new System.Windows.Controls.Image()
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute)),
                Name = "imgQrCode",
                Height = 100,
                Width = 100,
                Margin = new Thickness(0, 0, 6, 0)
            };

            TextBlock userName = new TextBlock()
            {
                Text = username,
                Name = "txblkUserName",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 32,
                Margin = new Thickness(0, 12, 0, 0)
            };


            userStack.Children.Add(qrCode);
            userStack.Children.Add(userName);
            return userStack;
        }
    }
}

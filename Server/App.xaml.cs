using Server.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private ServerBootstrap server;
        private NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox checkBox;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            server.Stop();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            System.Windows.Forms.Application.EnableVisualStyles();

            server = new ServerBootstrap();
            server.Start();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("Resources/icons8-remote-control-32.ico");
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

            if (Settings.Default.AskToPair)
            {
                Connect_Click(null, null);
            }
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                form.Text = "Connect your device";
                form.Size = new System.Drawing.Size(512, 512);

                Bitmap qrCodeImage;
                using (var ms = new MemoryStream(server.ConnectionCode.GetGraphic(20)))
                {
                    qrCodeImage = new Bitmap(ms);
                }

                var label = new System.Windows.Forms.Label()
                {
                    Text = "Scan the QR code with your mobile application to remote control this device.\n" +
                    "You can close this window and re-open it from the tray icon.",
                    AutoSize = true,
                };

                var pictureBox = new PictureBox()
                {
                    Image = qrCodeImage,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill,
                };
                checkBox = new System.Windows.Forms.CheckBox();
                checkBox.Text = "Do not ask me again";
                checkBox.AutoSize = false;
                checkBox.Size = new System.Drawing.Size(512, 50);
                checkBox.Appearance = Appearance.Normal;
                checkBox.CheckState = Settings.Default.AskToPair ? CheckState.Unchecked : CheckState.Checked;
                checkBox.CheckStateChanged += CheckBox_CheckStateChanged;
                checkBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

                var panel = new TableLayoutPanel()
                {
                    Padding = new Padding(10),
                    Dock = DockStyle.Fill
                };

                panel.ColumnCount = 1;
                panel.RowCount = 3;

                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 5));



                panel.Controls.Add(label, 0, 0);
                panel.Controls.Add(pictureBox, 0, 1);
                panel.Controls.Add(checkBox, 0, 2);
                form.Controls.Add(panel);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.Icon = new Icon("Resources/icons8-remote-control-dark-32.ico");

                form.ShowDialog();
            }
        }

        private void CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            Settings.Default.AskToPair = !checkBox.Checked;
            Settings.Default.Save();
        }
    }
}

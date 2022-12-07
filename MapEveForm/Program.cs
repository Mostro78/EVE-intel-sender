namespace MapEveForm
{


    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Controlla controlla= new Controlla();
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.ContextMenuStrip = GetContext(controlla);
            notifyIcon.Icon = Properties.Resources.eve_logo_6ZK_icon;
            notifyIcon.Visible = true;
            Application.Run(controlla);

            //Application.Run(new DebugForm());
            //Application.Run(new MapEveForm());
        }

        private static ContextMenuStrip GetContext(Controlla controlla) { 
            ContextMenuStrip CMS = new ContextMenuStrip();
            //CMS.Items.Add("ChatSelect", null, new EventHandler(ChatSelect_Click));
            CMS.Items.Add("ChatSelect", null, new EventHandler((o, e) => { 
                controlla.debugForm.Show();
            }));
            CMS.Items.Add("Token", null, new EventHandler((o, e) => {
                controlla.mapEveForm.Show();
            }));
            CMS.Items.Add("Exit", null, new EventHandler(Exit_Click));
            return CMS;
        }

        private static void Exit_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
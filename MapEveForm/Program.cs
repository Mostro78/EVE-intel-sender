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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.ContextMenuStrip = GetContext();
            notifyIcon.Icon = Properties.Resources.eve_logo_6ZK_icon;
            notifyIcon.Visible = true;
            Application.Run(new Controlla());

            //Application.Run(new DebugForm());
            //Application.Run(new MapEveForm());
        }

        private static void Application_Idle(object? sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        private static ContextMenuStrip GetContext() { 
            ContextMenuStrip CMS = new ContextMenuStrip();
            CMS.Items.Add("ChatSelect", null, new EventHandler(ChatSelect_Click));
            CMS.Items.Add("Token", null, new EventHandler(Token_Click));
            CMS.Items.Add("Exit", null, new EventHandler(Exit_Click));
            return CMS;
        }
        private static void ChatSelect_Click(object sender, EventArgs e)
        {
            //mi occupa di aprire la finestra e salvare il token in locale

            DebugForm debugForm = new DebugForm();
            debugForm.save_click += new DebugForm.Save_click((o, e) => {
               // save = Function.ReadFromBinaryFile<Save>(Function.getPathFile());
            });
            debugForm.Show();
        }

        private static void Save_click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void Token_Click(object sender, EventArgs e) {
            //mi occupa di aprire la finestra e salvare il token in locale
            
            Form mef =  new MapEveForm();
            mef.Show();
        }
        private static void Exit_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEveForm
{
    public partial class Controlla : Form
    {
        public DebugForm debugForm;
        public MapEveForm mapEveForm;
        public Save save;
        private List<SolarSystem> solarSystem;
        private int numMess = 0;
        public Controlla()
        {
            InitializeComponent();



            this.Icon = Properties.Resources.eve_logo_6ZK_icon;
            //carico tutti i sistemi di EVE

            using (StreamReader r = new StreamReader(new MemoryStream(Properties.Resources.mapSolarSystems)))
            {
                string json = r.ReadToEnd();
                solarSystem = JsonConvert.DeserializeObject<List<SolarSystem>>(json);
            }

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;

            mapEveForm = new MapEveForm();
            mapEveForm.save_click += new MapEveForm.Save_click((o, s) => {
                timer.Stop();
                save = s;// Function.ReadFromBinaryFile<Save>(Function.getPathFile());
                timer.Start();
            });

            debugForm = new DebugForm();
            debugForm.Hide();
            debugForm.save_click += new DebugForm.Save_click((o, s) => {
                timer.Stop();
                save = s;// Function.ReadFromBinaryFile<Save>(Function.getPathFile());
                timer.Start();
            });

            if (File.Exists(Function.getPathFile()))
            {
                save = Function.ReadFromBinaryFile<Save>(Function.getPathFile());
                timer.Start();
            }
            else
            {
                debugForm.Show();
            }
        }





        private void Controlla_Shown(object sender, EventArgs e)
        {
            //this.Hide();
        }
        private static readonly HttpClient client = new HttpClient();
        private void Timer_Tick(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //qui eseguo il controllo delle chat

            List<string> PlayerOpen = Function.getExeOpen();

            List<Player> res =  save.players.FindAll(p => PlayerOpen.Find(p1 => p1 == p.Name) != null);
            foreach (Player player in res) {

                       List<Messagelog> messaggi = Function.parseChat(player.ID, player.ChatList, solarSystem);
                if (messaggi.Count > 0)
                    if (messaggi[0] != null)
                    {
                        numMess = numMess + 1;
                        if (player.lastMessage == null)
                        {
                            string newLine = Environment.NewLine;
                            tbMessage.Text = player.Name + newLine + "ChatN:" + messaggi[0].chatName + newLine + "SolSys:" + messaggi[0].solarSystem + newLine + "Mess:" + messaggi[0].message + newLine + "--------------------------------------" + newLine + tbMessage.Text;
                            tbMessage.ScrollToCaret();
                            //tbMessage.Text = messaggi[0].solarSystem + " : " + messaggi[0].message;
                            lbMessage.Text = numMess.ToString();
                            //invio il messaggio al server

                            player.lastMessage = new List<Messagelog>();
                            foreach (Messagelog ml in messaggi)
                            {
                                player.lastMessage.Add(ml);
                            }
                        }
                        else {
                            List<Messagelog> mlist = new List<Messagelog>();
                            foreach (Messagelog ml in messaggi)
                            {
                                Messagelog mltmp = player.lastMessage.Find(m => ((m.message == ml.message) && (m.chatName == ml.chatName)));
                                if (mltmp == null) { 
                                    mlist.Add(ml);
                                }
                            }
                            if (mlist.Count > 0) {
                                //ho trovato un nuovo messaggio
                                //rimuovo il vecchio messaggio e aggiungo quello nuovo dal player.lastmessage
                                foreach (Messagelog ml in mlist) {
                                    string newLine = Environment.NewLine;
                                    tbMessage.Text =   player.Name + newLine+ "ChatN:" + ml.chatName + newLine + "SolSys:" + ml.solarSystem + newLine + "Mess:" + ml.message + newLine + "--------------------------------------" + newLine + tbMessage.Text ;
                                    tbMessage.ScrollToCaret();
                                    lbMessage.Text = numMess.ToString();
                                    player.lastMessage.FindAll(m => ((m.message == ml.message) && (m.chatName != ml.chatName)));
                                    player.lastMessage.Add(ml);
                                    Function.sendPost(ml, save.Token, client);
                                }
                            }

                        }
                    }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var uri = "https://map.eveonline.it";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }
    }
}

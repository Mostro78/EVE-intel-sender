﻿using Newtonsoft.Json;
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
        public Save save;
        private List<SolarSystem> solarSystem;
        public Controlla()
        {
            InitializeComponent();

            //carico tutti i sistemi di EVE

            using (StreamReader r = new StreamReader(new MemoryStream(Properties.Resources.mapSolarSystems)))
            {
                string json = r.ReadToEnd();
                solarSystem = JsonConvert.DeserializeObject<List<SolarSystem>>(json);
            }

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;

            if (File.Exists(Function.getPathFile()))
            {
                save = Function.ReadFromBinaryFile<Save>(Function.getPathFile());
                timer.Start();
            }
            else
            {
                new DebugForm().ShowDialog();
            }
        }

        private void Controlla_Shown(object sender, EventArgs e)
        {
            this.Hide();
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
                        if (player.lastMessage == null)
                        {
                            label1.Text = messaggi[0].solarSystem + " : " + messaggi[0].message;
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
                                    label1.Text ="ChatN:" + ml.chatName + " SolSys:"+ ml.solarSystem + "  Mess:" + ml.message;
                                    player.lastMessage.FindAll(m => ((m.message == ml.message) && (m.chatName != ml.chatName)));
                                    player.lastMessage.Add(ml);
                                    Function.sendPost(ml, save.Token, client);
                                }
                            }

                        }
                    }
            }

        }
    }
}
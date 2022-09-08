using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Globalization;

namespace MapEveForm
{
    public partial class DebugForm : Form
    {

        
        private List<Player> players = new List<Player>();
        private Save save = new Save();
        public DebugForm()
        {
            InitializeComponent();
            //cosi trovo tutti le chat uniche per ogni player
            List<Player> player = new List<Player>();
            // carico i dati dal file del salvataggio

            string pathFile = Function.getPathFile();
            if (File.Exists(pathFile))
            {
                save = Function.ReadFromBinaryFile<Save>(pathFile);
                players = save.players;
            }
            List<Chat> aaa = Function.getChatList(null, true);
            //lista dei player
            foreach (Chat chat in aaa)
            {
                if (players.Find(p => p.Name == chat.playerName) == null)
                {
                    players.Add(new Player() { Name = chat.playerName, ID = chat.playerID });
                }
            }
            //id id Zaria Aivo 90512217
            CLBPlayer.Items.Clear();
            foreach (Player p in players)
            {
                CLBPlayer.Items.Add(p.Name);
            }

            CLBPlayer.Items.Clear();
            foreach (Player p in players)
            {
                CLBPlayer.Items.Add(p.Name);
            }

        }



 

        private void button1_Click(object sender, EventArgs e)
        {
            //richieste internet
            //invio messaggio di prova
            
            //getChatList();
            //cosi trovo tutti le chat uniche per ogni player
            List<Player> player = new List<Player>();
            // carico i dati dal file del salvataggio
            
            string pathFile = Function.getPathFile(); 
            if (File.Exists(pathFile))
            {
                save = Function.ReadFromBinaryFile<Save>(pathFile);
                players = save.players;
            }
            List<Chat> aaa = Function.getChatList(null, true);
            //lista dei player
            foreach (Chat chat in aaa)
            {
                if (players.Find(p => p.Name == chat.playerName) == null)
                {
                    players.Add(new Player() { Name = chat.playerName });
                }
            }
            //id id Zaria Aivo 90512217
            CLBPlayer.Items.Clear();
            foreach (Player p in players)
            {
                CLBPlayer.Items.Add(p.Name);
            }

        }

        private void CLBPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //devo caricare l'elenco delle chat del player
            ListBox lb = (ListBox)sender;
            var tmp = lb.SelectedItem;
            loadChat(checkedLBEVE, (string)lb.SelectedItem);
        }

        public void loadChat(CheckedListBox CLBEve, string namePlayer)
        {
            //si occupa di caricarica i dati della chat
            string path = Function.getPathDir();
            string pathFile = Function.getPathFile();
            List<Chat> chatlist = new List<Chat>();
            if (Directory.Exists(path))
            {
                //carico la liste delle chat da controllare dal file 
                var listSelect = CLBEve.CheckedItems;
                //ho la lista caricata dal file adesso aggiungo le chat mancanti del player selezionato
                List<Chat> ChatFile = Function.getChatList();
                //prendo le singole chat per fare un elenco 
                IEnumerable<Chat> nomiChat = ChatFile.Where(el => el.playerName == namePlayer).GroupBy(elem => elem.chatName).Select(group => group.First());
                CLBEve.Items.Clear();
                foreach (var elem in nomiChat)
                {
                    if (chatlist.Find(c => c.chatName == elem.chatName) == null)
                    {
                        //aggiungo elem alla lista 
                        chatlist.Add(new Chat() { chatName = elem.chatName, enable = false });
                    }
                }
                //verifico  le chat presalvate 
                Player p = players.Find(p => p.Name == namePlayer);
                if (p != null)
                    foreach (Chat chat in p.ChatList)
                    {
                        foreach (var elem in chatlist) { 
                            if (elem.chatName == chat.chatName){ 
                                elem.enable = true;
                            }
                        }
                    }

                //carico la lista delle chat dentro il chckbox
                foreach (var elem in chatlist)
                {
                    CLBEve.Items.Add(elem.chatName, elem.enable);
                }

            }
        }


        private void btSave_Click(object sender, EventArgs e)
        {
            //salvo le chat selezionate
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Function.fileDir); ;
            string pathFile = Path.Combine(path, Function.FileSaveName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var listSelect = checkedLBEVE.CheckedItems;
            Function.WriteToBinaryFile<Save>(pathFile, save);
            this.Hide();
        }

        private void checkedLBEVE_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string playerName = (string)CLBPlayer.SelectedItem;
            string chatName = (string)((CheckedListBox)sender).SelectedItem;
            Player player = players.Find(p => p.Name == playerName);
            Chat c = player.ChatList.Find(c => c.chatName == chatName);
            bool check = (e.NewValue == CheckState.Checked) ? true : false;
            if (check) {
                if (c == null)
                {
                    player.ChatList.Add(new Chat() { chatName = chatName, enable = check });
                }
                else
                {
                    c.enable = check;
                }
            }
            else { 
                player.ChatList.Remove(c); 
            }
        }


    }
}

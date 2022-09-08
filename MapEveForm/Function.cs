using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MapEveForm
{

    [Serializable]
    public class Save { 
    public List<Player> players = new List<Player>();
    public String Token { get; set; } = "";

    }
    [Serializable]
    public class Player { 
        public string Name { get; set; }
        public string ID { get; set; }
        public List<Chat> ChatList = new List<Chat>();
        public List<Messagelog> lastMessage = new List<Messagelog>();
        
    }
    [Serializable]
    public class Messagelog 
    {
        public string IDplayer;
        public string chatName;
        public string message;
        public DateTime data;
        public string solarSystem;
        public string solarSystemID;
        public string sender;
    }
    [Serializable]
    public class Chat
    {
        public string chatName;
        public string date;
        public string time;
        public string playerID;
        public string playerName;
        public bool enable;
        public string pathFile;
        public DateTime dateTime;
        public string solarSystemID;
        public string sender;
        public string message;

    }

    public class SolarSystem
    {
        public string regionID;
        public string constellationID;
        public string solarSystemID;
        public string solarSystemName;
    }

    internal static class Function
    {
        public const string fileDir = "MapEve";
        public const string FileSaveName = "data.bin";


        public static string getPathDir() { 
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileDir);
        }
        public static string getPathFile() {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Function.fileDir); ;
            return  Path.Combine(path, Function.FileSaveName);
             
        }

        /// <summary>
        /// Fa il string.reverse
        /// </summary>
        /// <param name="s">abcde</param>
        /// <returns>edcba</returns>
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


        public static List<Chat> getChatList(string IDPlayer = null, bool last = false) {
            List<Chat> ChatFile = new List<Chat>();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string eveChatLog = Path.Combine(new string[] { path, "EVE", "logs", "Chatlogs" });
            DirectoryInfo directoryI = new DirectoryInfo(eveChatLog);
            IEnumerable<FileInfo> file = directoryI.GetFiles().Where(
                file =>
                file.LastWriteTimeUtc >= DateTime.UtcNow.AddDays(-10));
            foreach (var info in file)
            {
                string ChatName = Function.Reverse(Function.Reverse(info.Name.Replace(".txt", "")).Split("_")[3]);
                string Date = Function.Reverse(Function.Reverse(info.Name.Replace(".txt", "")).Split("_")[2]);
                string Time = Function.Reverse(Function.Reverse(info.Name.Replace(".txt", "")).Split("_")[1]);
                string IDP = Function.Reverse(Function.Reverse(info.Name.Replace(".txt", "")).Split("_")[0]);


                string PlayerName = "";
                using (var fs = new FileStream(info.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(fs, Encoding.Default))
                {
                    string line = "";
                    while (sr.EndOfStream == false)
                    {
                        line = sr.ReadLine();
                        if (line.Contains("  Listener:  ")) {
                            PlayerName  = line.Substring(27);
                            break;
                        }
                    }
                }
                if (IDPlayer == IDP || IDPlayer == null)
                    ChatFile.Add(new Chat { playerID = IDP, time = Time, date = Date,playerName = PlayerName, chatName = ChatName, pathFile = info.FullName, dateTime = DateTime.ParseExact(Date + Time, "yyyyMMddHHmmss", CultureInfo.InvariantCulture) });

            }
            List<Chat> chatFileUnique = new List<Chat>();
            if (last == true) {
                //estrapolo solo le ultime chat
                foreach (Chat chat in ChatFile) {
                    Chat findChat = chatFileUnique.Find(c => c.chatName == chat.chatName && c.playerID == chat.playerID);
                    if (findChat == null)
                    {
                        //trovata buova chat
                        chatFileUnique.Add(chat);
                    }
                    else {
                        //ho trovato un chat giá esisistente controlla la data di creazione
                        if (findChat.dateTime <= chat.dateTime) {
                            //rimuovo la vecchia chat per aggiungere quella piú aggiornata
                            chatFileUnique.Remove(findChat);
                            chatFileUnique.Add(chat);
                        }
                    }
                }
                return chatFileUnique;
            }
            return ChatFile;
        }
        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }


        public static List<string> getExeOpen()
        {
            List<string> exeOpen = new List<string>();
            foreach (System.Diagnostics.Process p1 in System.Diagnostics.Process.GetProcessesByName("ExeFile"))
            {
                if (p1.MainWindowTitle.Length > 5)
                {
                    exeOpen.Add(p1.MainWindowTitle.Substring(6));
                }
            }
            return exeOpen;
        }

        public static List<Messagelog> parseChat(string IDPlayer, List<Chat> chats, List<SolarSystem> solarSystem)
        {
            List<Messagelog> messaggi = new List<Messagelog>();

            //estraggo tutte le chat che devo andare ad analizzare 
            List<Chat> ChatFile = Function.getChatList(IDPlayer, true).FindAll(c => chats.Find(c1 => c1.chatName == c.chatName) != null);
            foreach (Chat chat in ChatFile)
            {
                string elem = null;
                Messagelog messLog = null;
                using (var fs = new FileStream(chat.pathFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(fs, Encoding.Default))
                {

                    while (sr.EndOfStream == false)
                    {
                        elem = sr.ReadLine();
                    }
                }

                if (elem != null && elem != "")
                {
                    //trasformo il elem nel messaggio
                    //string timeTmp = elem.Substring(3, 19);
                    DateTime dt = DateTime.ParseExact(elem.Substring(3, 19), "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);

                    int tmp = elem.Substring(19).IndexOf(" > ");
                    string tmps = elem.Substring(22 + tmp);
                    string[] tmpParse = tmps.Split(" ");
                    string sender = elem.Split("]")[1].Split(">")[0].Trim();
                    /*var system = tmpParse.Where<string>(s =>
                    {
                        string stmp = s.Replace("*", "");
                        return (stmp.Contains("-") && stmp.Length == 6);

                    });*/
                     List<string> words = new List<string>();
                     words.AddRange(elem.Substring(22 + tmp).Split(" "));
                    if (words.Count >= 2)
                    {
                        //var s = solarSystem.Find(s => s.solarSystemName == words[0]);
                        var s = solarSystem.Find(s => words.Find( w => w.Replace("*", "") ==   s.solarSystemName) != null );
                        if (s != null)
                        {
                            string message = "";
                            bool first = true;
                            foreach (string word in words)
                            {
                                if (s.solarSystemName != word)
                                {
                                    message = message + " " + word;
                                }

                            }
                            messLog = new Messagelog() { sender = sender , chatName = chat.chatName, data = dt, IDplayer = IDPlayer, message = message, solarSystem = s.solarSystemName, solarSystemID = s.solarSystemID };
                        }
                    }
                }
                DateTime now = DateTime.UtcNow;
                Messagelog messageLog = messaggi.Find(m => m.chatName == chat.chatName);
                if (messageLog == null)
                {
                    //aggiungo la nuova riga
                    if (messLog != null)
                        messaggi.Add(messLog);
                }
            }
            return messaggi;
        }
        public static async void sendPost(Messagelog message, string token, HttpClient client)
        {
            if (token != "")
            {
                /*
                 uuid : token  
                 system: id del sistema
                 sender: nome di chi invia il messaggio
                 string: stringa intel 
                */
                var values = new Dictionary<string, string>
              {
                  { "uuid", token },
                  { "system", message.solarSystemID },
                  { "sender", message.sender },
                  { "string", message.message }
              };
                //gli devo tirare fuori il token dal file salvato 
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://api.eveonline.it/v1/map/upstream", content);
                var responseString = await response.Content.ReadAsStringAsync();
            }
        }

        public static async void getChatList(HttpClient client)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://sso.eveonline.it/map/chars?uuid=cfc854da-1798-41d2-b554-bdf4ddbac798");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}

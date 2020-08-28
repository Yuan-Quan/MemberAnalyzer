using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace MemberAnalyzer.Util
{
    [Serializable()]
    public class QMember
    {
        public QMember(string Nick, string CardName, string ID, string DateJoined, string DateLastSpeak, Gender Gender)
        {
            if (Nick!=null)
            {    
                if (Nick[Nick.Length-1]=='\t')
                {
                    Nick = Nick.Remove(Nick.Length-1);
                }
            }
            if (CardName!=null)
            {
                if (CardName[CardName.Length-1]=='\t')
                {
                    CardName = CardName.Remove(CardName.Length-1);
                }
            }
            this.Nick = Nick;
            this.CardName = CardName;
            this.ID = ID;
            this.DateJoined = DateJoined;
            this.DateLastSpeak = DateLastSpeak;
            this.Gender = Gender;
        }
        
        public QMember()
        {

        }

        public string Nick { get; set; }
        public string CardName { get; set; }
        public string ID { get; set; }
        public Gender Gender { get; set; }
        public string DateJoined { get; set; }
        public string DateLastSpeak { get; set; }
    }

    public enum Gender
    {
        Male,Female,Undefined
    }

    public class Setting
    {
        public Setting(string key, string value,string description)
        {
            this.Key = key;
            this.Value = value;
            this.Description = description;
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public static class Util
    {
        private static byte[] ConvertStringToByteArray(string data)
        {
            return(new System.Text.UnicodeEncoding()).GetBytes(data);
        }

        private static System.IO.FileStream GetFileStream(string pathName)
        {
            return(new System.IO.FileStream(pathName, System.IO.FileMode.Open, 
                        System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
        }

        public static string GetSHA1Hash(string pathName)
        {
            string strResult = "";
            string strHashData = "";

            byte[] arrbytHashValue;
            System.IO.FileStream oFileStream = null;

            System.Security.Cryptography.SHA1CryptoServiceProvider oSHA1Hasher=
                        new System.Security.Cryptography.SHA1CryptoServiceProvider();

            try
            {
                oFileStream = GetFileStream(pathName);
                arrbytHashValue = oSHA1Hasher.ComputeHash(oFileStream);
                oFileStream.Close();

                strHashData = System.BitConverter.ToString(arrbytHashValue);
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch(System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return(strResult);
        }

        /// <summary>
        /// get settings!!
        /// </summary>
        /// <returns>Enumrable Settings</returns>
        public static IEnumerable<Setting> GetAllSettings()
        {
            var appSettings = ConfigurationManager.AppSettings;

            foreach (var key in appSettings.AllKeys)
            {
                yield return new Setting(key, appSettings[key].Split('^')[0], appSettings[key].Split('^')[1]);
            }
        }

        /// <summary>
        /// Print current settings
        /// </summary>
        public static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    Console.WriteLine("Current app settings: ");
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        /// <summary>
        /// Read a setting by key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value of appSettings[key]</returns>
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? null;
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return null;
            }
        }

        /// <summary>
        /// Add a setting in appsettings
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = ""+value+"^"+settings[key].Value.Split('^')[1];
                    
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        /// <summary>
        /// Reading and Echoing the File
        /// <br></br>
        /// Each time the calling code requests the next item from the sequence, the code reads the next line of text from the file and returns it.
        /// </summary>
        /// <param name="file">Path of the file</param>
        /// <returns>string</returns>
        public static IEnumerable<string> ReadFrom(string file)
        {
            string line;
            using var reader = File.OpenText(file);
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        public static void WriteAFile(List<string> ls, string path, string fileName)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, fileName)))
            {
                foreach (string line in ls)
                outputFile.WriteLine(line);
            }

            var preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"File [{fileName}] Write Succeed!!");
            Console.ForegroundColor = preForegroundColor;
        }

        public static IEnumerable<List<string>> RawMemberListSlicer(String path)
        {
            int count = 2;
            var ls = new List<string>(Util.ReadFrom(path));
            var result = new List<string>();
            foreach (var line in ls)
            {
                if (line=="1\t"||line=="\t1\t")
                {
                    continue;
                }

                if ((line==count+"\t")||(line=="\t"+count+"\t"))
                {
                    count ++;
                    yield return result;
                    result.Clear();
                }else
                {
                    result.Add(line);
                    continue;
                }
            }
        }

        private static Gender GetGender(string str)
        {
            return str switch
            {
                "男" => Gender.Male,
                "女" => Gender.Female,
                _ => Gender.Undefined,
            };
        }

        public static QMember MemberStrParser(List<string> lines)
        {
            try
            {
                
                if (lines.Count == 3)
                {
                    //Have a CardName
                    string nick, cardName, dtJoin, dtSpk, id, gender;
                    nick = lines[0];
                    cardName = lines[1];
                    id = lines[2].Split('\t')[0];
                    gender = lines[2].Split('\t')[1];
                    dtJoin = lines[2].Split('\t')[3];
                    dtSpk = lines[2].Split('\t')[4];
                    return new QMember(nick, cardName, id, dtJoin, dtSpk, GetGender(gender));

                }else if(lines.Count == 2)
                {
                    //Haven't set CardName
                    string nick, dtJoin, dtSpk, id, gender;
                    nick = lines[0];
                    id = lines[1].Split('\t')[0];
                    gender = lines[1].Split('\t')[1];
                    dtJoin = lines[1].Split('\t')[3];
                    dtSpk = lines[1].Split('\t')[4];
                    return new QMember(nick, null, id, dtJoin, dtSpk, GetGender(gender));
                }else
                {
                    throw new Exception();
                }
            }
            catch (System.Exception)
            {

                throw new Exception("Sliced Member Info incomplete, check the \"ToProcess\" file");
            }
        }

        public static IEnumerable<QMember> GetMembers(string path)
        {
            foreach (var item in RawMemberListSlicer(path))
            {
                yield return MemberStrParser(item);
            }
        }
    }

}
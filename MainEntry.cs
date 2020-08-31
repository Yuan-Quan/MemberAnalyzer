using System;
using System.IO;
using System.Collections.Generic;
using CommandDotNet;
using MemberAnalyzer.Util;
using ConsoleTables;

public class MainEntry
{
    //to store the console color when color changes
    ConsoleColor preForegroundColor;

    //format of date time
    const string dataFmt = "{0,-30}{1}";//maybe read from config in the future

    [Command(Name = "config",
    Usage = "config [opration] [key] [value]\nexample: config set path ~/GoPractice",
    Description = "view change/add settings",
    ExtendedHelpText = "oprations: view set add remove\nformat: [opration] key value,description")]
    public void Config(
        string opration = null,
        string k = null,
        string v = null
        )
    {
        switch (opration)
        {
            case "v":
            case "view":
                ConfigView(k);
                break;
            case "set":
            case "s":
                ConfigSet(k, v);
                break;
            case "add":
            case "a":
                ConfigAdd(k, v);
                break;
            case "remove":
            case "rm":
                ConfigRemove(k, v);
                break;
            default:
                Console.WriteLine("Usage: config [opration] [key] [value]\nexample: config set path ~/GoPractice");
                break;
        }

        static void ConfigRemove(string k, string v)
        {
            Console.WriteLine("Too lazy to implement it. Just goto App.config and delete it.");
            throw new NotImplementedException();
        }

        void ConfigAdd(string k, string v)
        {
            Util.AddUpdateAppSettings(k, v);
            Console.WriteLine();
            preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Configuration added successfully!!");
            Console.ForegroundColor = preForegroundColor;
        }

        void ConfigSet(string k, string v)
        {
            Util.AddUpdateAppSettings(k, v);
            Console.WriteLine();
            preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Configuration modified successfully!!");
            Console.ForegroundColor = preForegroundColor;
        }

        static void ConfigView(string k)
        {
            if (k == null)
            {
                //view all appsettings
                var table = new ConsoleTable("key", "value", "description");
                foreach (var setting in Util.GetAllSettings())
                {
                    table.AddRow(setting.Key, setting.Value, setting.Description);
                }

                Console.WriteLine();
                Console.WriteLine(" Here all your configurations in App.config:");
                table.Write();
                Console.WriteLine();
            }
        }
    }

    [Command(Name = "deserialize",
    Usage = "deserialize [path]\nexample: deserialize ~/ToProcess.txt",
    Description = "Deserialize text copid from QQ web, then serialize them into a xml file",
    ExtendedHelpText = "Deserialize text copid from QQ web, then serialize them into a xml file.\nfile:\tstring@path\tplain text file you want to deserialize\nsave:\tstring@path\twhere do you want to save serialized xml file(current directory by default)")]
    public void DeserializeAndSerialize(
        [Option(LongName = "file", ShortName = "f", 
        Description = "Plaintext file you want to deserialize")] 
        string orgPath,

        [Option(LongName = "save", ShortName = "s", 
        Description = "Whrer to save serialized xml file(current directory by default)")] 
        string dstPath = null
        )
    {
        if (dstPath == null)
        {
            dstPath = Directory.GetCurrentDirectory();
        }

        var members = new List<QMember>(Util.GetMembers(orgPath));
        
        preForegroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("text file deserialized successfully!!");
        Console.ForegroundColor = preForegroundColor;
        
        if (File.Exists(Path.Combine(dstPath, "Members.xml")))
        {
            //file already exists

            Console.Write($"{Path.Combine(dstPath, "Members.xml")} already exitsts, ");

            Console.WriteLine("\n");
            preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[1]SKIP:");
            Console.ForegroundColor = preForegroundColor;
            Console.Write(" break this opreation.");
            Console.WriteLine();

            preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[2]DELETE:");
            Console.ForegroundColor = preForegroundColor;
            Console.Write(" will DELETE it.");
            Console.WriteLine();

            preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[3]OVERWRITE:");
            Console.ForegroundColor = preForegroundColor;
            Console.Write(" will REPLACE it.");
            Console.WriteLine();

            while (true)
            {

                Console.Write("Select a option -> ");

                if (Int32.TryParse(Console.ReadLine(), out int result))
                {
                    switch (result)
                    {
                        case 2:
                            DeleteExist();
                            return;
                        case 1:
                            Console.WriteLine("will stop opreation 'Serilization'.");
                            return;
                        case 3:
                            DeleteExist();
                            Util.GenerateXML(members.ToArray(), dstPath);
                            return;
                        default:
                            Console.WriteLine("No this option!!");
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid input!!");
                    continue;
                }

            }
        }
        else
        {
            //no exist file
            Util.GenerateXML(members.ToArray(), dstPath);
            return;
        }

        void DeleteExist()
        {
            Console.Write($"Will ");
            preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("DELETE ");
            Console.ForegroundColor = preForegroundColor;
            Console.Write($"[{Path.Combine(dstPath, "Members.xml")}]");

            File.Delete(Path.Combine(dstPath, "Members.xml"));

            preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone!");
            Console.ForegroundColor = preForegroundColor;
        }
    }

    [Command(Name = "print",
    Usage = "print [opreation] -f [xmlFilePath]\nexample: print -f ",
    Description = "print the contnet of a xml file.",
    ExtendedHelpText = "oprations: table text raw")]
    public void Print(
        string opration = null,
        [Option(LongName = "file", ShortName = "f", 
        Description = "xml file you want to print")] 
        string file = null,
        [Option(LongName = "save", ShortName = "s", 
        Description = "save print result into an file")]
        bool save = false,
        [Option(LongName = "savePath", ShortName = "p", 
        Description = "Where do you want to save the file")] 
        string savePath = null
    )
    {
        if (savePath==null)
        {
            savePath = Directory.GetCurrentDirectory();
        }

        switch (opration)
        {
            case "t":
            case "table":
                PrintTable(file);
                break;
            case "raw":
            case "r":
                PrintRaw(file);
                break;
            case "text":
            case "tx":
                PrintText(file);
                break;
            default:
                Console.WriteLine("You must specify a way to print out datas, using [operation]\n avalible: table t raw r text tx");
                break;
        }

        void PrintRaw(string _path)
        {
            var ls = new List<string>();
            foreach (var item in Util.ReadFrom(_path))
            {
                System.Console.WriteLine(item);
                if (save)
                {
                    ls.Add(item);
                }
            }
            if (save)
            {
                System.Console.WriteLine($"this will be saved to {Path.Combine(savePath, Util.GetSHA1Hash(file)+".txt")}");
                Util.WriteAFile(ls, savePath, Util.GetSHA1Hash(file)+".txt");
            }
        }

        void PrintTable(string _path)
        {
            var table = new ConsoleTable("昵称", "群名片", "性别", "QQ号", "入群时间", "上次发言时间");
            foreach (var item in Util.QMemberDeserialize(_path))
            {
                table.AddRow(item.Nick, item.Alias, item.Gender.ToString(), item.ID, item.DateJoined, item.DateLastSpeak);
            }

            Console.WriteLine();
            Console.WriteLine("Table.Write invoked");
            table.Write();
            Console.WriteLine();

        }

        void PrintText(string _path)
        {
            var ls = new List<string>();
            foreach (var item in Util.QMemberDeserialize(_path))
            {
                System.Console.WriteLine(item.Nick+"\t"+item.Alias+"\t"+item.Gender.ToString()+"\t"+item.ID+"\t"+item.DateJoined+"\t"+item.DateLastSpeak);
                if (save)
                {
                    ls.Add(item.Nick+"\t"+item.Alias+"\t"+item.Gender.ToString()+"\t"+item.ID+"\t"+item.DateJoined+"\t"+item.DateLastSpeak);
                }
            }
            if (save)
            {
                System.Console.WriteLine($"this will be saved to {Path.Combine(savePath, Util.GetSHA1Hash(file)+".txt")}");
                Util.WriteAFile(ls, savePath, Util.GetSHA1Hash(file)+".txt");
            }
        }
    }

    [Command(Name = "completeAlias",
    Usage = "completeAlias -f [xmlFilePath] -s [matchSource] \nexample: completeAlias -f test.xml -s foo.txt ",
    Description = "complete Alias using a given file",
    ExtendedHelpText = "[matchSource] is a text file contains full alias")]
    public void CompleteAlias(
        [Option(LongName = "file", ShortName = "f", 
        Description = "xml file you want to complete")] 
        string file,
        [Option(LongName = "sourcce", ShortName = "s", 
        Description = "source file of ailas")]
        string source
    )
    {
        var sourceL = new List<string>(Util.ReadFrom(source));
        var members = new List<QMember>(Util.QMemberDeserialize(file));
        var membersComped = new List<QMember>(); 

        foreach (var item in Util.MatchAndComplete(members, sourceL))
        {
            membersComped.Add(item);
        }

        Util.GenerateXML(membersComped.ToArray(), Directory.GetCurrentDirectory());
    }
}
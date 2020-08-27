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
}
using System;
using CommandDotNet;


namespace MemberAnalyzer
{
    class Program
    {
        static void Startup()
        {
        //debug code here
        var m = new MainEntry();
            m.CompleteAlias("C:/Users/metro/Desktop/Members.xml", "C:/Users/metro/Desktop/MatchSource.txt");
        }
        static int Main(string[] args)
        {
            //Startup();
            return new AppRunner<MainEntry>()
                .UseDefaultMiddleware()
                .Run(args);
            
            //return 0;
        }
    }
}

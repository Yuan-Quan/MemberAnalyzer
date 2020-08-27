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
            m.DeserilizeAndSerilize("C:/Users/Reimu/source/repos/MemberAnalyzer/ToProcess/Test01");
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

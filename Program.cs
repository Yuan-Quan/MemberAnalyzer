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
            m.DeserializeAndSerialize("C:/Users/Reimu/Documents/New Text Document.txt");
        }
        static int Main(string[] args)
        {
            Startup();
            return new AppRunner<MainEntry>()
                .UseDefaultMiddleware()
                .Run(args);
            
            //return 0;
        }
    }
}

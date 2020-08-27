using System;
using CommandDotNet;

namespace MemberAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            static void Startup()
        {
           //debug code here
            
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
}

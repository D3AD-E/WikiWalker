using System;
using System.Collections.Generic;

namespace WikiWalker
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Logger.Log("Started search");

            var path = await Walker.GetPathAsync(@"Minecraft", @"C_Sharp_(programming_language)");

            if(path == null || path.Count == 0)
            {
                Logger.LogError("Path was not found (invalid pages?)");
                return;
            }


            foreach (var item in path)
            {
                Logger.Log(item);
            }
        }
    }
}

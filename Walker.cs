using System;
using System.Collections.Generic;
using System.Text;

namespace WikiWalker
{
    class Walker
    {
        public static async System.Threading.Tasks.Task<List<string>> GetPathAsync(string firstPage, string secondPage)
        {
            WikiApi wiki = new WikiApi();
            firstPage = wiki.TryFixLink(firstPage);
            secondPage = wiki.TryFixLink(secondPage);

            Dictionary<string, List<string>> path = new Dictionary<string, List<string>>();
            var toAdd = new List<string>
            {
                firstPage
            };
            path.Add(firstPage, toAdd);

            Queue<string> pages = new Queue<string>();
            pages.Enqueue(firstPage);

            List<string> cashe;

            while (pages.Count>0)
            {
                var currPage = pages.Dequeue();
                cashe = path[currPage];
                var links = await wiki.GetAllLinksAsync(currPage);

                if (links == null)
                    continue;

                foreach (var link in links)
                {
                    if (link == secondPage)
                    {
                        var toret = cashe;
                        toret.Add(link);

                        Logger.LogInfo($"Looked into {path.Count} pages!");

                        return toret;
                    }

                    if(link!=firstPage && !path.ContainsKey(link))
                    {
                        var prevPath = new List<string>(cashe);
                        prevPath.Add(link);

                        path.Add(link, prevPath);

                        pages.Enqueue(link);
                    }
                }
            }

            return null;
        }
    }
}

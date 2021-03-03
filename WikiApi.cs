using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WikiWalker
{
    class WikiApi
    {
        HtmlWeb Web;
        private const string BASE_URL = @"https://en.wikipedia.org";

        public WikiApi()
        {
            Web = new HtmlWeb();
        }
        public Task<List<string>> GetAllLinksAsync(string page)
        {
            return Task.Run(() => {
                return GetAllLinks(page);
            });
        }

        public string TryFixLink(string link)
        {
            if (!link.StartsWith(BASE_URL))
                link = GetLinkFromName(link);
            return link;
        }

        public string GetLinkFromName(string name)
        {
            return BASE_URL + $"/wiki/{name}";
        }
        
        public List<string> GetAllLinks(string page)
        {
            List<string> toret = new List<string>();
            try
            {
                HtmlDocument doc = Web.Load(page);
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    var refUrl = link.Attributes["href"].Value;
                    if (refUrl.StartsWith("/wiki/") && !refUrl.EndsWith("Main_Page") && !Regex.Match(refUrl, @"^/wiki/(\b[a-zA-Z]+:[a-zA-Z]*)").Success)
                    {
                        var wholeUrl = BASE_URL + refUrl;
                        if (!toret.Contains(wholeUrl))
                            toret.Add(wholeUrl);
                    }
                }
            }
            catch(Exception e)
            {
                return null;
            }

            return toret;
        }
    }
}

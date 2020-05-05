using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using AngleSharp.Html.Parser;

namespace ParserSite
{
    class ParserSites
    {
        public static string table { get; set; }
        /// <summary>
        /// Получить исходный код страницы
        /// </summary>
        /// <param name="link"> Url страницы </param>
        /// <returns></returns>
        public static string GetPage(string link)
        {
            HttpRequest request = new HttpRequest();
            string response = request.Get(link).ToString();
            return response;
        }
        public static string[] GetMatches(string page)
        {
            HtmlParser htmlParser = new HtmlParser();
            var parsePage = htmlParser.ParseDocument(page);
            //table = parsePage.QuerySelector("table.matches").OuterHtml;
            List<string> matches = new List<string>(); 
            foreach (var tmp in parsePage.QuerySelectorAll("div.event__match"))
            {
                matches.Add(tmp.OuterHtml);
            }
            return matches.ToArray();
        }
    }
}

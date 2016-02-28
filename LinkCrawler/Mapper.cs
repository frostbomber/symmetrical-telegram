using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HttpSiteMapper
{
    class Mapper
    {
        private int level = 0;
        private MapBranchNodes tempNodeList;
        private String baseUrl { get; set; }
        private List<String> baseUrls { get; set; }
        public SiteMap map;

        public Mapper(String startingUrl)
        {
            baseUrl = startingUrl;
            map = new SiteMap(startingUrl);
            baseUrls = GatherUrls(baseUrl);
        }

        public List<String> GatherUrls(String currentUrl)
        {
            // shoutout to http://www.dotnetperls.com/scraping-html
            // this is /heavily/ based on that article
            List<String> list = new List<String>();

            WebClient w = new WebClient();
            string file = w.DownloadString(currentUrl);

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                RegexOptions.Singleline);
                if (m2.Success)
                {
                    list.Add(m2.Groups[1].Value);
                }
            }
            return list;
        }

       internal void BuildBranch(String startUrl)
       {
           List<String> urls = GatherUrls(startUrl);
           String next = "";
           
           if(urls != null)
           {
               next = urls[0];
               urls.RemoveAt(0);

               level++;
               BuildBranch(next);
               if (!(tempNodeList.Contains(baseUrl)))
               {
                   tempNodeList.Add(baseUrl, level);
               }
           }
           else
           {
               //base case
               //change the last index in the list to have an 'n' relation for New sub-branch starts after this
               if (!(tempNodeList.Contains(baseUrl)))
               {
                   tempNodeList.Add(baseUrl, level);
               }
               level--;
           }
       }

       internal void AddBranchToMap()
       {
           map.branchCollection.Add(tempNodeList);
           tempNodeList.Clear();
       }

    }
}

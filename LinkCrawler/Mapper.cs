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
        private MapBranchNodes tempNodeList = new MapBranchNodes();
        private String baseUrl { get; set; }
        public List<String> baseUrls { get; set; }
        private SiteMap map;
        public String uriBase { get; set; }

        public Mapper(String startingUrl, String uri)
        {
            baseUrl = startingUrl;
            map = new SiteMap(startingUrl);
            baseUrls = GatherUrls(baseUrl, true);
            if (uri != null)
            {
                uriBase = uri;
            }
            else
            {
                uriBase = "http://wiki";
            }
        }

        public List<String> GatherUrls(String currentUrl, bool verbose)
        {
            // shoutout to http://www.dotnetperls.com/scraping-html
            // this is /heavily/ based on that article
            List<String> list = new List<String>();

            WebClient w = new WebClient();
            string file = "";

            try
            {
                if (verbose)
                {
                    System.Console.WriteLine("Now collecting links on page: " + "http://wiki.advsyscon.com" + currentUrl + "\r\n");
                }

                //for some reason this doesn't work unless I hardcode this...
                // to do --
                if (!(currentUrl.Contains("http://wiki.advsyscon.com")))
                {
                    file = w.DownloadString(uriBase + currentUrl);
                }
                else
                {
                    file = w.DownloadString(currentUrl);
                }
            
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
                        if ((!(m2.Groups[1].Value.Contains("#"))) && (!(m2.Groups[1].Value.Contains(":"))) && (m2.Groups[1].Value != currentUrl) && (m2.Groups[1].Value.Contains("/index.php/")) && (m2.Groups[1].Value != "/index.php/Main_Page") && (m2.Groups[1].Value != "/index.php/Technical_Support"))
                        {
                            list.Add(m2.Groups[1].Value);
                        }
                    }
                }
            }

            catch(WebException wex)
            {
                System.Console.WriteLine("There was an unhandled exception. Boooooo. \r\n");
                System.Console.WriteLine(wex.ToString());
            }
            return list;
        }

       public void BuildBranch(String startUrl, bool verbose)
       {
           List<String> urls = GatherUrls(startUrl, verbose);

           if (verbose)
           {
               foreach (String url in urls)
               {
                   System.Console.WriteLine(url);
               }
           }
           String next = "";
           
           if(!(urls.Count == 0))
           {
               int index = 0;

               //skip anything that is not a link to a wiki site
               //this can be further generalized later
               // to do --
               //while ((!(urls[index].Contains("/index.php/"))) || ((urls[index] == startUrl)) || (urls[index].Contains(":")) || (urls[index] == "/index.php/Main_Page"))
               //{
               //    urls.RemoveAt(index);
               //    index++;
               //}
               next = urls[index];
               urls.RemoveAt(index);

               //do the add ahead of time to avoid circular links
               level++;
               if (!(tempNodeList.Contains(startUrl)))
               {
                   tempNodeList.Add(startUrl, level);
               }

               //cut the recursion short if we hit a loop
               if (!(tempNodeList.Contains(next)))
               {
                   BuildBranch(next, verbose);
               }
               
           }
           else
           {
               //base case
               if (!(tempNodeList.Contains(startUrl)))
               {
                   tempNodeList.Add(startUrl, level);
               }
               level--;
           }
       }

       public void AddBranchToMap()
       {
           
           map.branchCollection.Add( new MapBranchNodes() );
           foreach(MapBranchNode node in tempNodeList)
           {
               map.branchCollection[map.branchCollection.Count - 1].Add(node);
           }
           tempNodeList.Clear();
       }

       public String toStringFlat()
       {
           StringBuilder toStringText = new StringBuilder();

           //show the root node
           toStringText.Append("Starting URL:\r\n" + map.rootNode.rootUrl + "\r\n");

           toStringText.Append("Its children:\r\n");
           //for each branch starting with a direct child of the root
           foreach (MapBranchNodes branch in map.branchCollection)
           {
               //for each actual node (URL) in that collection
               foreach (MapBranchNode node in branch)
               {
                   toStringText.Append(node.url + "\r\n");
               }
           }

           return toStringText.ToString();
       }

        public String toString()
        {
            StringBuilder toStringText = new StringBuilder();

            //show the root node
            toStringText.Append(map.rootNode + "\r\n");

            //for each branch starting with a direct child of the root
            foreach (MapBranchNodes branch in map.branchCollection)
            {
                toStringText.Append("Child:\r\n");
                //for each actual node (URL) in that collection
                foreach (MapBranchNode node in branch)
                {
                    toStringText.Append(node.url + " " + node.level + "\r\n");
                }
            }

            return toStringText.ToString(); 
        }

    }
}

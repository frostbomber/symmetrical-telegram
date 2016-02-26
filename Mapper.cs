using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSiteMapper
{
    class Mapper
    {
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
            
            List<String> list = new List<String>();
            // to do
            // want to see if mediawiki contains a functionality which will gather all linked-to articles from the current article
            // if it does not, I will need to write something to just target

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
               if (!(tempNodeList.Contains(baseUrl)))
               {
                   tempNodeList.Add(baseUrl, 'l');
               }
               BuildBranch(next);
           }
           else
           {
               //base case
               //change the last index in the list to have an 'n' relation for New sub-branch starts after this
               int last = tempNodeList.Count - 1;
               tempNodeList[last].relationship = 'n';
           }
       }

       internal void AddBranchToMap()
       {
           map.branchCollection.Add(tempNodeList);
           tempNodeList.Clear();
       }

    }
}

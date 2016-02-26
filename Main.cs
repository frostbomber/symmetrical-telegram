using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSiteMapper
{
    class Main
    {
        public static void Main()
        {
            String startingUrl = "";
            Mapper mapper = new Mapper(startingUrl);

            List<String> baseUrls = new List<String>();
            baseUrls = mapper.GatherUrls(startingUrl);

            foreach(String url in baseUrls)
            {
                mapper.BuildBranch(url);
                mapper.AddBranchToMap();
            }
        }
    }
}

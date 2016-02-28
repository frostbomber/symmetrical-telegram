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
            System.Console.WriteLine("Enter a starting URL");
            String startingUrl = System.Console.ReadLine();
            Mapper mapper = new Mapper(startingUrl);

            List<String> baseUrls = new List<String>();
            baseUrls = mapper.GatherUrls(startingUrl);

            foreach(String url in baseUrls)
            {
                mapper.BuildBranch(url);
                mapper.AddBranchToMap();
            }

            System.Console.WriteLine(mapper.toStringFlat());
        }
    }
}

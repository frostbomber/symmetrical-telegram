using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSiteMapper
{
    class Entry
    {
        public static void Main(String[] args)
        {
            bool v = false;
            String startingUrl = "";
            String uri = "";

            if (args.Length == 0)
            {
                System.Console.WriteLine("Would you like to run in vebose mode? y/n");
                String verbose = System.Console.ReadLine();
                

                if (verbose == "true" || verbose == "y")
                {
                    v = true;
                }

                System.Console.WriteLine("Enter the URI type (ex: http or file) and base string. \r\nAn example might be http://wikipedia.org if you are crawling a wiki which uses relative hyperlinks");
                uri = System.Console.ReadLine();

                System.Console.WriteLine("Enter a starting URL");
                startingUrl = System.Console.ReadLine();
            }
            else
            {
                if (args[0] == "-v")
                {
                    v = true;
                    uri = args[1];
                    startingUrl = args[2];
                }
                else
                {
                    uri = args[0];
                    startingUrl = args[1];
                }
            }

            if (uri == null)
            {
                uri = "http://wiki";
                //temporary catch-all will fix for real later.
            }    
                Mapper mapper = new Mapper(startingUrl, uri);
            
            List<String> baseUrls = new List<String>();
            baseUrls = mapper.baseUrls;

            foreach(String url in baseUrls)
            {
                mapper.BuildBranch(url, v);
                mapper.AddBranchToMap();
            }

            System.Console.WriteLine(mapper.toStringFlat());
        }
    }
}

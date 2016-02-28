using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSiteMapper
{
    class MapBranchNode
    {
        internal String url { get; set; }
        internal int level {get;set;}
        //idea here is to keep the list flat but maintain relationship so it can be parsed and shown visually later

        internal MapBranchNode(String urlString, int recurLevel)
        {
            url = urlString;
            level = recurLevel;
        }
    }
}

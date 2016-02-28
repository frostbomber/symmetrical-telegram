using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSiteMapper
{
    class SiteMap
    {
        public MapRootNode rootNode { get; set; }
        public MapBranchNodesCollection branchCollection { get; set; }
        internal SiteMap(String startingUrl)
        {
            rootNode = new MapRootNode(startingUrl);
            branchCollection = new MapBranchNodesCollection();
        }

        private void AddBranch(MapBranchNodes branch)
        {
            branchCollection.Add(branch);
        }

        public String toString()
        {
            String str = String.Empty;

            //to do
            //will give textual representation of the SiteMap object that has been constructed

            return str;
        }
    }
}

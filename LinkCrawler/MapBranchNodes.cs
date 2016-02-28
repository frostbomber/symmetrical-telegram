using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSiteMapper
{
    class MapBranchNodes : List<MapBranchNode>
    {
        internal MapBranchNodes(){}

        public void Add(String url, int rLevel)
        {
            MapBranchNode newNode = new MapBranchNode(url, rLevel);
            this.Add(newNode);
        }

        public bool Contains(String currentUrl)
        {
            bool isContained = false;

            foreach (MapBranchNode node in this)
            {
                if (node.url == currentUrl)
                {
                    isContained = true;
                }
            }

            return isContained;
        }

    }
}

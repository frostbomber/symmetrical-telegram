﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSiteMapper
{
    class MapRootNode
    {
        internal String rootUrl {get;set;}

        internal MapRootNode(String baseUrl)
        {
            rootUrl = baseUrl;
        }
    }
}

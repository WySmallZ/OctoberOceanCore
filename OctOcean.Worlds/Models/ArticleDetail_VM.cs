using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OctOcean.Worlds.Models
{
    public class ArticleDetail_VM
    {
        public string Title { get; set; }
        public string LastUpdate { get; set; }
        public string ContentText { get; set; }

        public string AidStyle { get; set; }
        public string ArticleDesc { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int BrowseCount { get; set; }
    }
}

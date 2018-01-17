using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OctOcean.WebSite.Models
{
    public class ArticleDetail_M
    {
        public string Title { get; set; }
        public string LastUpdate { get; set; }
        public string ContentText { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int BrowseCount { get; set; }
    }
}

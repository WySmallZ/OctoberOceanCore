using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Entity
{
   public  class Aux_HomeArticlePager_Entity
    {
        public int SNumber { get; set; }
        public string ArticleKey { get; set; }
        public int Id { get; set; }

        public string ArticleTitle { get; set; }
        public string ArticleDesc { get; set; }

        public DateTime UpdateTime { get; set; }

        public int BrowseCount { get; set; } = 0;
    }
}

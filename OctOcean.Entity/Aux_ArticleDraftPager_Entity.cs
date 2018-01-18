using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Entity
{
    public class Aux_ArticleDraftPager_Entity
    {
        public int SNumber { get; set; }
        public string ArticleKey { get; set; }

        public string ArticleTitle { get; set; }
        public string ArticleCategoryName { get; set; }

        public string ArticleTag { get; set; }

        public DateTime UpdateTime { get; set; }

        public string UpdateTimeF { get { return this.UpdateTime.ToString("yyyy-MM-dd HH:mm"); } }
        public string PubArticleKey { get; set; }

        public int DelStatus { get; set; }

        public int PubDelStatus { get; set; }

        public DateTime? PubUpdateTime { get; set; }

        public bool NeedPublish //需要重新发布的条件：已经发布过，并且最新的一次修改时间，大于发布后的修改时间
        {
            get { return (this.PubDelStatus == 0 &&   this.PubUpdateTime != null && this.UpdateTime > this.PubUpdateTime); }
        }



    }
}

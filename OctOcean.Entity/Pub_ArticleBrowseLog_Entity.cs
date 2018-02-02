using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Entity
{
    public  class Pub_ArticleBrowseLog_Entity
    {
        public int Id { get; set; }
        public string ArticleKey { get; set; }
        public string IP { get; set; }
        public string SessionID { get; set; }
        public string AccessUrl { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

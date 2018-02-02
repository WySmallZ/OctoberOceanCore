using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Entity
{
    public  class Pub_AccessRecord_Entity
    {
        public int Id { get; set; }
        /// <summary>
        /// 页面标签
        /// </summary>
        public string PageTag { get; set; }

        /// <summary>
        /// SessionID
        /// </summary>
        public string SessionID { get; set; }

        public string IP { get; set; }
        /// <summary>
        /// 访问url
        /// </summary>
        public string AccessUrl { get; set; }
        /// <summary>
        /// 访问记录创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}

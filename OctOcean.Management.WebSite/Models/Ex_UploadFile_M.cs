using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OctOcean.Management.WebSite.Models
{
    public class Ex_UploadFile_M
    {
        public string Accept_Title { get; set; }
        public string Accept_Extensions { get; set; }
        public string Accept_MimeTypes { get; set; }
        /// <summary>
        /// 是否开启分片，0：false，1：true，之所以使用0/1是为了js方便转换为bool类型
        /// </summary>
        public byte Chunked { get; set; }
        public string ArticleKey { get; set; }
    }
}

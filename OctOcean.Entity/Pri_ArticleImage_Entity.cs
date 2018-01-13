using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Entity
{
    public  class Pri_ArticleImage_Entity
    {
        public string ImgKey { get; set; }

        public string ArticleKey { get; set; }

        public string ImgName { get; set; }
        public string Src { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public DateTime UpdateTime { get; set; }
        
    }
}

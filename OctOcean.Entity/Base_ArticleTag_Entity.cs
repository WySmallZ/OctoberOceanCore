using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Entity
{
   public class Base_ArticleTag_Entity
    {
        public int Id { get; set; }
        //[Required,StringLength(10)]
        /// <summary>
        /// 文章标签名称
        /// </summary>
        public string ArticleTagName { get; set; }
        //[Required, StringLength(10)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")] //限制用户可以输入的字符，吃住只能为字母
        /// <summary>
        /// 文章标签名称对应的Code
        /// </summary>
        public string ArticleTagCode { get; set; }
        /// <summary>
        /// 删除状态，1：表示已经删除，0：表示未删除
        /// </summary>
        public byte DelStatus { get; set; }
    }
}

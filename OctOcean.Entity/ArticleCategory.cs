using System;
using System.ComponentModel.DataAnnotations;

namespace OctOcean.Entity
{
    public class ArticleCategory
    {
        public int Id { get; set; }
        //[Required,StringLength(10)]
        public string ArticleCategoryName { get; set; }
        //[Required, StringLength(10)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")] //限制用户可以输入的字符，吃住只能为字母
        public string ArticleCategoryCode { get; set; }

        public byte DelStatus { get; set; }

    }
}

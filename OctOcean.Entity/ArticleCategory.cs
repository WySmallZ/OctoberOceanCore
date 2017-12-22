using System;
using System.ComponentModel.DataAnnotations;

namespace OctOcean.Entity
{
    public class ArticleCategory
    {
        public int Id { get; set; }
        [Required,StringLength(10)]
        public string ArticleCategoryName { get; set; }
        [Required, StringLength(10)]
        public string ArticleCategoryCode { get; set; }

    }
}

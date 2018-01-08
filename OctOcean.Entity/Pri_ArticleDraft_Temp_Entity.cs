using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Entity
{
    public class Pri_ArticleDraft_Temp_Entity
    {
        public int Id { get; set; }
        /// <summary>
        /// 文章的Guid值，同一篇文章，该值唯一
        /// </summary>
        public string ArticleKey { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArticleTitle { get; set; }
        /// <summary>
        /// 文章类别
        /// </summary>
        public string ArticleCategory { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string ContentText { get; set; }
        /// <summary>
        /// 文章标签
        /// </summary>
        public string ArticleTag { get; set; }
        /// <summary>
        /// 辅助样式脚本
        /// </summary>
        public string AidStyle { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 删除状态，1：表示已经删除，0：表示未删除
        /// </summary>
        public byte DelStatus { get; set; }
    }
}

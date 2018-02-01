using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OctOcean.Entity;
namespace OctOcean.Worlds.Models
{
    public class ArticleList_VM
    {
        public Aux_HomeArticlePager_Entity [] Article_EntityArray { get; set; }
        public int ArticleSumCount { get; set; }
        public int CurrentPageIdex { get; set; }


    }
}

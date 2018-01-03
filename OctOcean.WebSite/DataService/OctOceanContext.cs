using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OctOcean.Entity;

namespace OctOcean.WebSite.DataService
{
    public class OctOceanContext:DbContext
    {
        public OctOceanContext(DbContextOptions<OctOceanContext> options) : base(options)
        {
        }

        //创建每个实体集
        /*
        注意：默认情况下，DbSet属性名称要和表名相同，如果不同需要覆盖默认行为OnModelCreating，重新指定表名称。
            */
        public DbSet<Pub_Article_Entity> Pub_Article_Entity_DbSet { get; set; }



       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //指定数据库中的表名和实体相关联
            modelBuilder.Entity<Pub_Article_Entity>().ToTable("Pri_ArticleDraft");
           
        }
    }
}

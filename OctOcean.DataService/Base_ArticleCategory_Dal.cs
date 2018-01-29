
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using OctOcean.Entity;
using OctOcean.Utils;


namespace OctOcean.DataService
{
    public class Base_ArticleCategory_Dal
    {
        IDbConnection connection = null;
        public Base_ArticleCategory_Dal()
        {
            this.connection = new SqlConnection(OctOceanGlobal.Config.DefaultConnectionString);
        }

        public int InsertArticleCategory(Base_ArticleCategory_Entity entity)
        {   
            string sql = "INSERT INTO Base_ArticleCategory(ArticleCategoryName, ArticleCategoryCode,DelStatus,UpdateTime ) VALUES(@ArticleCategoryName,@ArticleCategoryCode,@DelStatus,GETDATE())";
            return connection.Execute(sql, new { ArticleCategoryName = entity.ArticleCategoryName, ArticleCategoryCode = entity.ArticleCategoryCode, DelStatus = entity.DelStatus });

        }

        /// <summary>
        /// 物理删除数据，真删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteArticleCategoryWithHard(int Id)
        {
            return connection.Execute("DELETE FROM Base_ArticleCategory WHERE Id=@Id", new { Id = Id });
        }

        public int DeleteArticleCategory(int Id)
        {
            return connection.Execute("UPDATE Base_ArticleCategory SET DelStatus=1,UpdateTime=GETDATE()  WHERE Id=@Id", new { Id = Id });
        }


        public int UpdateArticleCategory(Base_ArticleCategory_Entity entity)
        {
            string sql = "UPDATE Base_ArticleCategory SET ArticleCategoryCode=@ArticleCategoryCode, ArticleCategoryName=@ArticleCategoryName, DelStatus=@DelStatus,UpdateTime=GETDATE() WHERE Id=@Id;";
            return connection.Execute(sql, new { ArticleCategoryName = entity.ArticleCategoryName, ArticleCategoryCode = entity.ArticleCategoryCode, DelStatus = entity.DelStatus, Id = entity.Id });
        }

        public IList<Base_ArticleCategory_Entity> GetAllArticleCategory(string where ,object obj)
        {
            string sql = "select  Id , ArticleCategoryName, ArticleCategoryCode, DelStatus,UpdateTime from Base_ArticleCategory where DelStatus=0 ";
            if (where != null && where.Trim().Length > 0)
            {
                sql += where;
            }
            var query = connection.Query<Base_ArticleCategory_Entity>(sql, obj).AsList();
            return  query;
        }

        public IList<Base_ArticleCategory_Entity> GetAllArticleCategory()
        {
            string sql = "select  Id , ArticleCategoryName, ArticleCategoryCode, DelStatus,UpdateTime from Base_ArticleCategory where DelStatus=0 order by UpdateTime desc ";
             
            var query = connection.Query<Base_ArticleCategory_Entity>(sql).AsList();
            return query;
        }


        public Base_ArticleCategory_Entity GetArticleCategory(int Id)
        {
            string sql = "select  Id , ArticleCategoryName, ArticleCategoryCode, DelStatus,UpdateTime from Base_ArticleCategory where DelStatus=0 and Id=@Id";

            var query = connection.Query<Base_ArticleCategory_Entity>(sql, new { Id = Id }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }
    }
}

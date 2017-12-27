using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using OctOcean.Entity;
using OctOcean.Utils;


namespace OctOcean.DataService
{
    public class ArticleCategoryDal
    {
        IDbConnection connection = null;
        public ArticleCategoryDal()
        {
            this.connection = new SqlConnection(ConfigHelper.DefaultConnectionString);
        }

        public int InsertArticleCategory(ArticleCategory entity)
        {   
            string sql = "INSERT INTO ArticleCategory(ArticleCategoryName, ArticleCategoryCode,DelStatus ) VALUES(@ArticleCategoryName,@ArticleCategoryCode,@DelStatus)";
            return connection.Execute(sql, new { ArticleCategoryName = entity.ArticleCategoryName, ArticleCategoryCode = entity.ArticleCategoryCode, DelStatus = entity.DelStatus });

        }

        /// <summary>
        /// 物理删除数据，真删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteArticleCategoryWithHard(int Id)
        {
            return connection.Execute("DELETE FROM ArticleCategory WHERE Id=@Id", new { Id = Id });
        }

        public int DeleteArticleCategory(int Id)
        {
            return connection.Execute("UPDATE ArticleCategory SET DelStatus=1  WHERE Id=@Id", new { Id = Id });
        }


        public int UpdateArticleCategory(ArticleCategory entity)
        {
            string sql = "UPDATE ArticleCategory SET ArticleCategoryCode=@ArticleCategoryCode, ArticleCategoryName=@ArticleCategoryName, DelStatus=@DelStatus WHERE Id=@Id;";
            return connection.Execute(sql, new { ArticleCategoryName = entity.ArticleCategoryName, ArticleCategoryCode = entity.ArticleCategoryCode, DelStatus = entity.DelStatus, Id = entity.Id });
        }

        public IList<ArticleCategory> GetAllArticleCategory(string where ,object obj)
        {
            string sql = "select  Id , ArticleCategoryName, ArticleCategoryCode, DelStatus from ArticleCategory where DelStatus=0 ";
            if (where != null && where.Trim().Length > 0)
            {
                sql += where;
            }
            var query = connection.Query<ArticleCategory>(sql, obj).AsList();
            return  query;
        }

        public IList<ArticleCategory> GetAllArticleCategory()
        {
            string sql = "select  Id , ArticleCategoryName, ArticleCategoryCode, DelStatus from ArticleCategory where DelStatus=0 ";
             
            var query = connection.Query<ArticleCategory>(sql).AsList();
            return query;
        }


        public ArticleCategory GetArticleCategory(int Id)
        {
            string sql = "select  Id , ArticleCategoryName, ArticleCategoryCode, DelStatus from ArticleCategory where DelStatus=0 and Id=@Id";

            var query = connection.Query<ArticleCategory>(sql, new { Id = Id }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }
    }
}

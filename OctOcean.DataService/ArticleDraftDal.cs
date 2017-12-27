using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using OctOcean.Utils;
using OctOcean.Entity;
using Dapper;

namespace OctOcean.DataService
{
    public class ArticleDraftDal
    {

        IDbConnection connection = null;
        public ArticleDraftDal()
        {
            this.connection = new SqlConnection(ConfigHelper.DefaultConnectionString);
        }

        public int InsertArticleDraft(ArticleDraft entity)
        {
            string sql = "INSERT INTO ArticleDraft(ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,DelStatus,UpdateTime ) VALUES(@ArticleKey,@ArticleTitle,@ArticleCategory,@ContentText,@ArticleTag,@AidStyle,@DelStatus,GETDATE())";
            return connection.Execute(sql, new {  entity.ArticleKey,  entity.ArticleTitle, entity.ArticleCategory,entity.ContentText,entity.ArticleTag, entity.AidStyle ,entity.DelStatus });

        }


        /// <summary>
        /// 物理删除数据，真删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteArticleDraftWithHard(int Id)
        {
            return connection.Execute("DELETE FROM ArticleDraft WHERE Id=@Id", new { Id = Id });
        }

        public int DeleteArticleDraft(int Id)
        {
            return connection.Execute("UPDATE ArticleDraft SET DelStatus=1  WHERE Id=@Id", new { Id = Id });
        }


        public int UpdateArticleDraft(ArticleDraft entity)
        {
            string sql = "UPDATE ArticleDraft SET ArticleTitle=@ArticleTitle, ArticleCategory=@ArticleCategory,ContentText=@ContentText,ArticleTag=@ArticleTag,AidStyle=@AidStyle, DelStatus=@DelStatus,UpdateTime=@UpdateTime WHERE ArticleKey=@ArticleKey;";
            return connection.Execute(sql, new { entity.ArticleTitle, entity.ArticleCategory, entity.ContentText, entity.ArticleTag, entity.AidStyle, entity.DelStatus,entity.UpdateTime,entity.ArticleKey });
        }



        public IList<ArticleDraft> GetAllArticleDraft(string where, object obj)
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,UpdateTime,DelStatus from ArticleDraft where DelStatus=0 ";
            if (where != null && where.Trim().Length > 0)
            {
                sql += where;
            }
            var query = connection.Query<ArticleDraft>(sql, obj).AsList();
            return query;
        }

        public IList<ArticleDraft> GetAllArticleDraft()
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,UpdateTime,DelStatus from ArticleDraft where DelStatus=0 ";

            var query = connection.Query<ArticleDraft>(sql).AsList();
            return query;
        }


        public ArticleDraft GetArticleDraft(int Id)
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,UpdateTime,DelStatus from ArticleDraft where DelStatus=0 and Id=@Id";

            var query = connection.Query<ArticleDraft>(sql, new { Id = Id }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }


        public ArticleDraft GetArticleDraft(string ArticleKey)
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,UpdateTime,DelStatus from ArticleDraft where DelStatus=0 and ArticleKey=@ArticleKey";

            var query = connection.Query<ArticleDraft>(sql, new { ArticleKey = ArticleKey }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }

    }
}

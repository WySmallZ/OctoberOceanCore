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
    public class Pri_ArticleDraft_Dal
    {

        IDbConnection connection = null;
        public Pri_ArticleDraft_Dal()
        {
            this.connection = new SqlConnection(ConfigHelper.DefaultConnectionString);
        }

        public int InsertPri_ArticleDraft(Pri_ArticleDraft_Entity entity)
        {
            string sql = "INSERT INTO Pri_ArticleDraft(ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,DelStatus,UpdateTime ) VALUES(@ArticleKey,@ArticleTitle,@ArticleCategory,@ContentText,@ArticleTag,@AidStyle,@DelStatus,GETDATE())";
            return connection.Execute(sql, new {  entity.ArticleKey,  entity.ArticleTitle, entity.ArticleCategory,entity.ContentText,entity.ArticleTag, entity.AidStyle ,entity.DelStatus });

        }


        /// <summary>
        /// 物理删除数据，真删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeletePri_ArticleDraftWithHard(int Id)
        {
            return connection.Execute("DELETE FROM Pri_ArticleDraft WHERE Id=@Id", new { Id = Id });
        }

        public int DeletePri_ArticleDraft(int Id)
        {
            return connection.Execute("UPDATE Pri_ArticleDraft SET DelStatus=1  WHERE Id=@Id", new { Id = Id });
        }


        public int UpdatePri_ArticleDraft(Pri_ArticleDraft_Entity entity)
        {
            string sql = "UPDATE Pri_ArticleDraft SET ArticleTitle=@ArticleTitle, ArticleCategory=@ArticleCategory,ContentText=@ContentText,ArticleTag=@ArticleTag,AidStyle=@AidStyle, DelStatus=@DelStatus,UpdateTime=@UpdateTime WHERE ArticleKey=@ArticleKey;";
            return connection.Execute(sql, new { entity.ArticleTitle, entity.ArticleCategory, entity.ContentText, entity.ArticleTag, entity.AidStyle, entity.DelStatus,entity.UpdateTime,entity.ArticleKey });
        }



        public IList<Pri_ArticleDraft_Entity> GetAllPri_ArticleDraft(string where, object obj)
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,UpdateTime,DelStatus from Pri_ArticleDraft where DelStatus=0 ";
            if (where != null && where.Trim().Length > 0)
            {
                sql += where;
            }
            var query = connection.Query<Pri_ArticleDraft_Entity>(sql, obj).AsList();
            return query;
        }

        public IList<Pri_ArticleDraft_Entity> GetAllPri_ArticleDraft()
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,UpdateTime,DelStatus from Pri_ArticleDraft where DelStatus=0 ";

            var query = connection.Query<Pri_ArticleDraft_Entity>(sql).AsList();
            return query;
        }


        public Pri_ArticleDraft_Entity GetPri_ArticleDraft(int Id)
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,UpdateTime,DelStatus from Pri_ArticleDraft where DelStatus=0 and Id=@Id";

            var query = connection.Query<Pri_ArticleDraft_Entity>(sql, new { Id = Id }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }


        public Pri_ArticleDraft_Entity GetPri_ArticleDraft(string ArticleKey)
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,AidStyle,UpdateTime,DelStatus from Pri_ArticleDraft where DelStatus=0 and ArticleKey=@ArticleKey";

            var query = connection.Query<Pri_ArticleDraft_Entity>(sql, new { ArticleKey = ArticleKey }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }

    }
}

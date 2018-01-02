﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using OctOcean.Entity;
using OctOcean.Utils;

namespace OctOcean.DataService
{
   public  class Base_ArticleTag_Dal
    {
        IDbConnection connection = null;
        public Base_ArticleTag_Dal()
        {
            this.connection = new SqlConnection(ConfigHelper.DefaultConnectionString);
        }

        public int InsertArticleTag(Base_ArticleTag_Entity entity)
        {
            string sql = "INSERT INTO Base_ArticleTag(ArticleTagName, ArticleTagCode,DelStatus ) VALUES(@ArticleTagName,@ArticleTagCode,@DelStatus)";
            return connection.Execute(sql, new { ArticleTagName = entity.ArticleTagName, ArticleTagCode = entity.ArticleTagCode, DelStatus = entity.DelStatus });

        }

        /// <summary>
        /// 物理删除数据，真删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteArticleTagWithHard(int Id)
        {
            return connection.Execute("DELETE FROM Base_ArticleTag WHERE Id=@Id", new { Id = Id });
        }

        public int DeleteArticleTag(int Id)
        {
            return connection.Execute("UPDATE Base_ArticleTag SET DelStatus=1  WHERE Id=@Id", new { Id = Id });
        }


        public int UpdateArticleTag(Base_ArticleTag_Entity entity)
        {
            string sql = "UPDATE Base_ArticleTag SET ArticleTagCode=@ArticleTagCode, ArticleTagName=@ArticleTagName, DelStatus=@DelStatus WHERE Id=@Id;";
            return connection.Execute(sql, new { ArticleTagName = entity.ArticleTagName, ArticleTagCode = entity.ArticleTagCode, DelStatus = entity.DelStatus, Id = entity.Id });
        }

        public IList<Base_ArticleTag_Entity> GetAllArticleTag(string where, object obj)
        {
            string sql = "select  Id , ArticleTagName, ArticleTagCode, DelStatus from Base_ArticleTag where DelStatus=0 ";
            if (where != null && where.Trim().Length > 0)
            {
                sql += where;
            }
            var query = connection.Query<Base_ArticleTag_Entity>(sql, obj).AsList();
            return query;
        }

        public IList<Base_ArticleTag_Entity> GetAllArticleTag()
        {
            string sql = "select  Id , ArticleTagName, ArticleTagCode, DelStatus from Base_ArticleTag where DelStatus=0 ";

            var query = connection.Query<Base_ArticleTag_Entity>(sql).AsList();
            return query;
        }


        public Base_ArticleTag_Entity GetArticleTag(int Id)
        {
            string sql = "select  Id , ArticleTagName, ArticleTagCode, DelStatus from Base_ArticleTag where DelStatus=0 and Id=@Id";

            var query = connection.Query<Base_ArticleTag_Entity>(sql, new { Id = Id }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }
    }
}

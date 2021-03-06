﻿using System;
using System.Collections.Generic;
using System.Text;
using OctOcean.Utils;
using OctOcean.Entity;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace OctOcean.DataService
{
    public class Pub_Article_Dal
    {
        IDbConnection connection = null;
        public Pub_Article_Dal()
        {
            this.connection = new SqlConnection(OctOceanGlobal.Config.DefaultConnectionString);
        }

        /// <summary>
        /// 根据Pri_ArticleDraft的数据，插入到Pub_Article中，注意：ArticleKey必须在Pri_ArticleDraft中存在，注意发布后的文章的修改时间一定要和draft下的相同，这样才能区分哪些是新编辑的内容
        /// </summary>
        /// <param name="ArticleKey"></param>
        /// <returns></returns>
        public int InsertPub_ArticleWithPri_ArticleDraft(string ArticleKey,bool IsPublish)
        {
            //注意发布后的文章的修改时间一定要和draft下的相同，这样才能区分哪些是新编辑的内容
            string sql =IsPublish? @"
--如果已经发布过了
IF EXISTS (SELECT ArticleKey FROM Pub_Article WHERE ArticleKey=@ArticleKey)
BEGIN
    UPDATE Pub_Article SET ArticleTitle=d.ArticleTitle,ArticleCategory=d.ArticleCategory,ContentText=d.ContentText,ArticleTag=d.ArticleTag,ArticleDesc=d.ArticleDesc,AidStyle=d.AidStyle,UpdateTime=d.UpdateTime,DelStatus=0 FROM Pub_Article a INNER JOIN Pri_ArticleDraft d ON a.ArticleKey=d.ArticleKey WHERE a.ArticleKey=@ArticleKey;
END ELSE BEGIN
    INSERT INTO Pub_Article( ArticleKey ,ArticleTitle ,ArticleCategory ,ContentText ,ArticleTag ,ArticleDesc,AidStyle ,UpdateTime ,DelStatus)
    SELECT TOP 1 ArticleKey ,ArticleTitle ,ArticleCategory ,ContentText ,ArticleTag ,ArticleDesc,AidStyle ,UpdateTime ,0 FROM Pri_ArticleDraft WHERE ArticleKey=@ArticleKey;
END" : "UPDATE Pub_Article SET DelStatus=1 WHERE ArticleKey=@ArticleKey "; //如果取消发布，直接逻辑删除该发布的记录
            return connection.Execute(sql, new { ArticleKey });
        }




        public Pub_Article_Entity GetPub_Article_Entity(string ArticleKey)
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,ArticleDesc,AidStyle,UpdateTime,DelStatus from Pub_Article where ArticleKey=@ArticleKey";

            var query = connection.Query<Pub_Article_Entity>(sql, new { ArticleKey = ArticleKey }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }

        public Pub_Article_Entity Get_NotDel_Pub_Article_Entity(string ArticleKey)
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,ArticleDesc,AidStyle,UpdateTime,DelStatus from Pub_Article where ArticleKey=@ArticleKey AND DelStatus=0";

            var query = connection.Query<Pub_Article_Entity>(sql, new { ArticleKey = ArticleKey }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }

        

        public List<Pub_Article_Entity> GetAllPub_Article_Entity(string orderby= "UpdateTime DESC")
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,ArticleDesc,AidStyle,UpdateTime,DelStatus from Pub_Article ORDER BY " + orderby;

            return connection.Query<Pub_Article_Entity>(sql).AsList();
         
        }


        public List<Aux_HomeArticlePager_Entity> GetAllNotDel_Pub_Article_Entity(string ArticleCategory, out int SumCount,int PageIndex=1,int PageSize=10)
        {
            int start = (PageIndex - 1) * PageSize + 1;
            int end = PageIndex * PageSize;
            string wheresql = " DelStatus=0  ";
            if(!string.IsNullOrWhiteSpace(ArticleCategory))
            {
                wheresql += " and ArticleCategory=@ArticleCategory ";
            }

            string sqlcount = string.Format("SELECT count(1) FROM Pub_Article  WHERE {0};", wheresql);
            SumCount = ConvertHelper.ToInt32(connection.ExecuteScalar(sqlcount, new { ArticleCategory }));

            string sql = string.Format(@"
with wt as 
(
    select ROW_NUMBER() OVER(ORDER BY UpdateTime DESC ) AS SNumber,p.Id,p.ArticleKey FROM Pub_Article p WHERE {0}
),acnt as(
    SELECT ArticleKey,COUNT(1) BrowseCount FROM  Pub_ArticleBrowseLog GROUP BY ArticleKey
)
select wt.SNumber,wt.ArticleKey,wt.Id,ArticleTitle,ArticleDesc,UpdateTime,acnt.BrowseCount
from wt left join Pub_Article d on wt.ArticleKey = d.ArticleKey
left join acnt on wt.ArticleKey=acnt.ArticleKey
where wt.SNumber BETWEEN {1} AND {2} order by d.UpdateTime DESC;", wheresql, start, end);
            var query = connection.Query<Aux_HomeArticlePager_Entity>(sql, new { ArticleCategory }).AsList();
            return query;

        }

        public int GetPub_Article_BrowseCount(string ArticleKey)
        {
            string sql = "SELECT COUNT(1)  cnt FROM  Pub_ArticleBrowseLog WHERE ArticleKey=@ArticleKey;";
            var sumcount = connection.ExecuteScalar(sql, new { ArticleKey });
            return ConvertHelper.ToInt32(sumcount);
        }



        //public List<Pub_Article_Entity> GetAllNotDel_Pub_Article_Entity(string orderby = "UpdateTime DESC")
        //{
        //    string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,ArticleDesc,AidStyle,UpdateTime,DelStatus from Pub_Article WHERE DelStatus=0 ORDER BY " + orderby;

        //    return connection.Query<Pub_Article_Entity>(sql).AsList();

        //}


        //public List<Pub_Article_Entity> GetAllNotDel_Pub_Article_EntityByArticleCategory(string ArticleCategory,string orderby = "UpdateTime DESC")
        //{
        //    string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,ArticleDesc,AidStyle,UpdateTime,DelStatus from Pub_Article WHERE DelStatus=0 and ArticleCategory=@ArticleCategory ORDER BY " + orderby;

        //    return connection.Query<Pub_Article_Entity>(sql,new { ArticleCategory }).AsList();

        //}



        public int DeletePub_ArticleWithHard(string ArticleKey)
        {
            return connection.Execute("DELETE FROM Pub_Article WHERE ArticleKey=@ArticleKey", new { ArticleKey = ArticleKey });
        }

        public int DeletePub_Article(string ArticleKey)
        {
            return connection.Execute("UPDATE Pub_Article SET DelStatus=1  WHERE ArticleKey=@ArticleKey", new { ArticleKey = ArticleKey });
        }
    }
}

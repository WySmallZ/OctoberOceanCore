using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using OctOcean.Entity;
using OctOcean.Utils;

namespace OctOcean.DataService
{
    public class Pri_ArticleImage_Dal
    {

        IDbConnection connection = null;
        public Pri_ArticleImage_Dal()
        {
            this.connection = new SqlConnection(OctOceanGlobal.Config.DefaultConnectionString);
        }


        public int InsertPri_ArticleDraft(Pri_ArticleImage_Entity entity)
        {
            string sql = "INSERT INTO Pri_ArticleImage(ImgKey,ArticleKey,ImgName,Src,Height,Width,UpdateTime)VALUES(@ImgKey,@ArticleKey,@ImgName,@Src,@Height,@Width,GETDATE())";
            return connection.Execute(sql, new { entity.ImgKey, entity.ArticleKey, entity.ImgName, entity.Src, entity.Height, entity.Width });
        }

        public int DeletePri_ArticleDraftByImgKey(string ImgKey)
        {
            return connection.Execute("DELETE FROM Pri_ArticleImage WHERE ImgKey=@ImgKey;", new { ImgKey = ImgKey });
        }
        


        public int DeletePri_ArticleDraftByArticleKey(string ArticleKey)
        {
            return connection.Execute("DELETE FROM Pri_ArticleImage WHERE ArticleKey = @ArticleKey ", new { ArticleKey = ArticleKey });
        }
    }
}

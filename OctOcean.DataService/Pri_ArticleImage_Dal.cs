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


        public int InsertPri_ArticleImage(Pri_ArticleImage_Entity entity)
        {
            string sql = "INSERT INTO Pri_ArticleImage(ImgKey,ArticleKey,ImgName,Src,Height,Width,UpdateTime)VALUES(@ImgKey,@ArticleKey,@ImgName,@Src,@Height,@Width,GETDATE())";
            return connection.Execute(sql, new { entity.ImgKey, entity.ArticleKey, entity.ImgName, entity.Src, entity.Height, entity.Width });
        }

        public int DeletePri_ArticleImageByImgKey(string ImgKey)
        {
            return connection.Execute("DELETE FROM Pri_ArticleImage WHERE ImgKey=@ImgKey;", new { ImgKey = ImgKey });
        }
        


        public int DeletePri_ArticleImageByArticleKey(string ArticleKey)
        {
            return connection.Execute("DELETE FROM Pri_ArticleImage WHERE ArticleKey = @ArticleKey ", new { ArticleKey = ArticleKey });
        }


        public IList<Pri_ArticleImage_Entity> GetAllPri_ArticleImage()
        {
            string sql = "select  ImgKey,ArticleKey,ImgName,Src,Height,Width,UpdateTime from Pri_ArticleImage ";

            var query = connection.Query<Pri_ArticleImage_Entity>(sql).AsList();
            return query;
        }

        public IList<Pri_ArticleImage_Entity> GetAllPri_ArticleImage(string ArticleKey)
        {
            string sql = "select  ImgKey,ArticleKey,ImgName,Src,Height,Width,UpdateTime from Pri_ArticleImage WHERE ArticleKey = @ArticleKey   ";

            var query = connection.Query<Pri_ArticleImage_Entity>(sql, new { ArticleKey = ArticleKey }).AsList();
            return query;
        }

        public  Pri_ArticleImage_Entity GetPri_ArticleImage_Entity(string ImgKey)
        {
            string sql = "select top 1  ImgKey,ArticleKey,ImgName,Src,Height,Width,UpdateTime from Pri_ArticleImage where ImgKey=@ImgKey ";

            var query = connection.Query<Pri_ArticleImage_Entity>(sql,new { ImgKey }).AsList();
            if (query != null && query.Count > 0)
                return query[0];
            return null;
        }


        public int UpdatePri_ArticleImage(Pri_ArticleImage_Entity entity)
        {
            string sql = "UPDATE Pri_ArticleImage SET ImgName=@ImgName,Src=@Src,Height=@Height,Width=@Width,UpdateTime=GETDATE() WHERE ImgKey=@ImgKey ";
            return connection.Execute(sql, new { entity.ImgKey,   entity.ImgName, entity.Src, entity.Height, entity.Width });

        }



        public string ReplaceImagesPlaceholder(string text,string ArticleKey)
        {

            //获取当前文章下的所有图片
           var allimages= GetAllPri_ArticleImage(ArticleKey); 

            foreach(var img in allimages)
            {
                string k= "[*" + img.ImgKey + "*]";
                string h = img.Height > 0 ? $"max-height:{img.Height}px;" : "";
                string w = img.Width > 0 ? $"max-width:{img.Width}px;" : "";

                string v = $"<img src=\"{img.Src }\" alt=\"{ img.ImgName}\" style=\"{h+w}\"/>";
                text= text.Replace(k, v);
            }
            return text;
        }
    }
}

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
   public  class Pri_ArticleDraft_Temp_Dal
    {
        IDbConnection connection = null;
        public Pri_ArticleDraft_Temp_Dal()
        {
            this.connection = new SqlConnection(OctOceanGlobal.Config.DefaultConnectionString);
        }

        public int InsertPri_ArticleDraft_Temp(Pri_ArticleDraft_Temp_Entity entity)
        {
            string sql = @"
IF NOT EXISTS(SELECT ArticleKey FROM Pri_ArticleDraft_Temp WHERE ArticleKey=@ArticleKey AND ISNULL(ArticleTitle,'')=@ArticleTitle AND ISNULL(ArticleCategory,'')=@ArticleCategory AND ISNULL(ContentText,'')=@ContentText AND ISNULL(ArticleTag,'')=@ArticleTag AND ISNULL(ArticleDesc,'')=@ArticleDesc AND ISNULL(AidStyle,'')=@AidStyle )
BEGIN
	INSERT INTO Pri_ArticleDraft_Temp(ArticleKey,ArticleTitle,ArticleCategory,ContentText,ArticleTag,ArticleDesc,AidStyle,UpdateTime ) VALUES(@ArticleKey,@ArticleTitle,@ArticleCategory,@ContentText,@ArticleTag,@ArticleDesc,@AidStyle,GETDATE())
END";
            return connection.Execute(sql, new { entity.ArticleKey, entity.ArticleTitle, entity.ArticleCategory, entity.ContentText, entity.ArticleTag,entity.ArticleDesc, entity.AidStyle });

        }

        public int GetSaveTempCountByArticleKey(string ArticleKey)
        {
            string sql = "SELECT COUNT(1) FROM Pri_ArticleDraft_Temp WHERE  ArticleKey=@ArticleKey";
            return ConvertHelper.ToInt32(connection.ExecuteScalar(sql, new { ArticleKey }));
        }
    }
}

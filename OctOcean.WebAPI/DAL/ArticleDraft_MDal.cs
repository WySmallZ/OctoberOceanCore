using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OctOcean.WebAPI.Models;

namespace OctOcean.WebAPI.DAL
{
    public class ArticleDraft_MDal
    {
        IDbConnection connection = null;
        public ArticleDraft_MDal()
        {
            this.connection = new SqlConnection(ConfigHelper.DefaultConnectionString);
        }


        public IList<ArticleDraft_M> GetAllArticleDraft()
        {
            string sql = "select  Id , ArticleKey,ArticleTitle,ArticleCategory,ArticleTag,UpdateTime,DelStatus from Pri_ArticleDraft where DelStatus=0 order by Id ";

            var query = connection.Query<ArticleDraft_M>(sql).AsList();
            return query;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using OctOcean.Entity;
using Dapper;
using System.Data;
using OctOcean.Utils;
using System.Data.SqlClient;

namespace OctOcean.DataService
{
   public class Pub_AccessRecord_Dal
    {
        IDbConnection connection = null;
        public Pub_AccessRecord_Dal()
        {
            this.connection = new SqlConnection(OctOceanGlobal.Config.DefaultConnectionString);
        }
        public void InsertAccessRecord(Pub_AccessRecord_Entity arEntity,Pub_ArticleBrowseLog_Entity ablEntity)
        {
            if(arEntity!=null)
            {
                string sql = "INSERT INTO Pub_AccessRecord ( PageTag, SessionID, IP, AccessUrl, CreateTime )VALUES  ( @PageTag, @SessionID, @IP, @AccessUrl, GETDATE()   )";
                connection.Execute(sql, new { arEntity.PageTag, arEntity.SessionID, arEntity.IP, arEntity.AccessUrl});
            }
            if (ablEntity != null)
            {
                string sql2 = "INSERT INTO Pub_ArticleBrowseLog(ArticleKey, IP, SessionID, AccessUrl, CreateTime) VALUES(@ArticleKey, @IP, @SessionID, @AccessUrl, GETDATE())";
                connection.Execute(sql2, new { ablEntity.ArticleKey, ablEntity.IP, ablEntity.SessionID, ablEntity.AccessUrl });
            }
        }
    }
}

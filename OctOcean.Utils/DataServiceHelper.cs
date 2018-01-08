using System;
using System.Collections.Generic;
using System.Text;
 

namespace OctOcean.Utils
{
    public class DataServiceHelper
    {
        public static string GetPagerSql(string TableName,string [] ShowColumns,string where, int PageSize, int PageIndex = 1, string PrimaryKey="Id", string OrderbySql="[Id] ASC" )
        {
            int start = (PageIndex - 1)*PageSize + 1; 
            int end = PageIndex*PageSize;

            string selectColumns = (ShowColumns == null || ShowColumns.Length == 0) ? "*" : ("_T_._S_N_,[" + string.Join("],[", ShowColumns) + "]");



            string sql = string.Format(@"
SELECT COUNT(1) FROM [{0}] WHERE {3}; 
with _T_ (_S_N_,_ID_) as 
(  
    select ROW_NUMBER() OVER(ORDER BY {1}),{2} from [{0}]  where {3} 
)  
select {4} from _T_ left join [{0}] syst on _T_._ID_ = syst.{2}
where _T_._S_N_ BETWEEN {5} AND {6} order by {1} 

", TableName, OrderbySql, PrimaryKey, where, selectColumns, start, end);
            return sql;

        }
    }
}

using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Hidistro.SqlDal._CMGJ
{

    public class aspnet_members_jfSQL
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public int getaspnet_members_jfByMID(int mid)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT Points FROM aspnet_members_jf WHERE MID = @mid");
            this.database.AddInParameter(sqlStringCommand, "@mid", DbType.Int32, mid);
            object obj2 = this.database.ExecuteScalar(sqlStringCommand);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }
    }
}

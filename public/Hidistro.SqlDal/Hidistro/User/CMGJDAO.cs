using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Hidistro.User
{
    public class CMGJDAO
    {
        private Database db = DatabaseFactory.CreateDatabase();

        //获取冻结金额
        public decimal getDJJF(int mid)
        {
            string query = string.Format("SELECT ISNULL(DAmount,0) FROM aspnet_Members WHERE UserId = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToDecimal(obj));
        }
        //获取积分
        public decimal getJF(int mid)
        {
            string query = string.Format("SELECT Points FROM aspnet_members_jf WHERE	MID = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToDecimal(obj));
        }
        //获取经验
        public decimal getJY(int mid)
        {
            string query = string.Format("SELECT points FROM	aspnet_Members WHERE	userid = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToDecimal(obj));
        }
        //创建积分
        public void makeJF(int mid, int jf)
        {
            string query = string.Format("INSERT INTO aspnet_members_jf VALUES({0},{1})", mid, jf);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
        }
        //添加积分
        public bool addJF(int mid, int jf)
        {
            string query = string.Format("INSERT INTO aspnet_members_jf VALUES({0},)", mid, jf);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            return this.db.ExecuteNonQuery(dbCommand) > 0;
        }
        //添加冻结金额
        public bool addDAmount(int mid, decimal amount, int jyz)
        {
            string query = string.Format("UPDATE aspnet_Members SET DAmount = ( ( SELECT ISNULL(DAmount,0) FROM aspnet_Members WHERE UserId = {0}	) + {1}),Points = ( SELECT Points FROM aspnet_Members WHERE UserId = {2}) +{3} WHERE	UserId = {4}", mid, amount, mid, jyz, mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            return this.db.ExecuteNonQuery(dbCommand) > 0;
        }
        //注册送
        public bool zhucesong(int mid, decimal amount)
        {
            string query = string.Format("UPDATE aspnet_Members SET TotalAmount = ((SELECT ISNULL(TotalAmount, 0) FROM aspnet_Members WHERE UserId = {0}) + {1}), AvailableAmount = ((SELECT ISNULL(AvailableAmount, 0) FROM aspnet_Members WHERE UserId = {2}  ) +{3}) WHERE UserId = {4}", mid, amount, mid, amount, mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            return this.db.ExecuteNonQuery(dbCommand) > 0;
        }
    }
}

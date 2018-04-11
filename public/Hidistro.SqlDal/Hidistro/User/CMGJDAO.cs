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
        //直接绑定为大校
        public bool setGradeId(int mid, int gid)
        {
            string query = string.Format("UPDATE aspnet_Members SET GradeId ={0} WHERE UserId = {1}", gid, mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            return this.db.ExecuteNonQuery(dbCommand) > 0;
        }
        /// <summary>
        /// 给用户账户加钱
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <param name="amount">加多少钱</param>
        /// <returns></returns>
        public bool setTotalAmount(int mid, decimal amount)
        {
            string query = string.Format("UPDATE aspnet_Members SET TotalAmount = ((SELECT ISNULL(TotalAmount, 0) FROM aspnet_Members WHERE UserId = {0}) + {1}), AvailableAmount = ((SELECT ISNULL(AvailableAmount, 0) FROM aspnet_Members WHERE UserId = {2}  ) +{3}) WHERE UserId = {4}", mid, amount, mid, amount, mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            return this.db.ExecuteNonQuery(dbCommand) > 0;
        }
        //推荐注册互送给新用户
        public bool tuisong(string uname)
        {
            string query = string.Format("UPDATE aspnet_Members SET AvailableAmount = 5 WHERE	UserName = '{0}'", uname);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            return this.db.ExecuteNonQuery(dbCommand) > 0;
        }
        /// <summary>
        /// 获取代理商状态
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns>状态(0代表正常，1代表冻结,9删除)</returns>
        public int getaspnet_DistributorsStatus(int mid)
        {
            string query = string.Format("SELECT ReferralStatus FROM aspnet_Distributors WHERE UserId = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToInt32(obj));
        }
        /// <summary>
        /// 获取充值金额
        /// </summary>
        /// <param name="PayId">支付id</param>
        /// <returns></returns>
        public decimal getChongzhiJine(string PayId)
        {
            string query = string.Format("SELECT TradeAmount FROM Hishop_MemberAmountDetailed WHERE PayId = {0}", PayId);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToDecimal(obj));
        }
        /// <summary>
        /// 送经验
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <param name="jingyan">送多少经验</param>
        /// <returns></returns>
        public bool updateJY(int mid, int jingyan)
        {
            string query = string.Format("update aspnet_Members set points = ((select points from aspnet_Members where UserId = {0}) + {1})  where UserId = {2}", mid, jingyan, mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            return this.db.ExecuteNonQuery(dbCommand) > 0;
        }
        /// <summary>
        /// 获取会员推荐者ID
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns></returns>
        public int getShangjiUID(int mid)
        {
            string query = string.Format("select ReferralUserId  from aspnet_Members where UserId = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToInt32(obj));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bandname"></param>
        /// <returns></returns>
        public int getUserByBindName(string bandname)
        {
            string query = string.Format("select UserId from aspnet_Members where UserBindName = {0}", bandname);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToInt32(obj));
        }
        /// <summary>
        /// 获取用户代理级别
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns></returns>
        public int getUserGID(int mid)
        {
            string query = string.Format("select GradeId from aspnet_Members where UserId = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToInt32(obj));
        }
        /// <summary>
        /// 获取折扣比例
        /// </summary>
        /// <param name="GID">级别id</param>
        /// <returns></returns>
        public int getDiscount(int gid)
        {
            string query = string.Format("select Discount from aspnet_MemberGrades where GradeId = {0}", gid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToInt32(obj));
        }
        /// <summary>
        /// 获取用户绑定金额
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public decimal getBanddingjine(int mid)
        {
            string query = string.Format("SELECT DAmount FROM aspnet_Members WHERE UserID = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToDecimal(obj));
        }
        /// <summary>
        /// 获取账户金额
        /// </summary>
        /// <returns></returns>
        public decimal getZhanghujine(int mid)
        {
            string query = string.Format("SELECT AvailableAmount FROM aspnet_Members WHERE UserID = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            object obj = db.ExecuteScalar(dbCommand);
            return (Convert.ToDecimal(obj));
        }
        /// <summary>
        /// 更新绑定金额
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public bool gxbdje(int mid)
        {
            string query = string.Format("UPDATE aspnet_Members SET DAmount = AvailableAmount + DAmount, AvailableAmount = 0 WHERE UserId = {0}", mid);
            DbCommand dbCommand = this.db.GetSqlStringCommand(query);
            return this.db.ExecuteNonQuery(dbCommand) > 0;
        }
    }
}

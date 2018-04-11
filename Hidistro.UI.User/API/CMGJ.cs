using Hidistro.ControlPanel.Members;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.SaleSystem.Vshop;
using Hidistro.SqlDal.Orders;
using Hidistro.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Hidistro.UI.User.API
{
    public class CMGJ : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private MemberInfo currentMember = MemberProcessor.GetCurrentMember();
        private CMGJDAO db = new CMGJDAO();
        //获取冻结金额
        public void getDJJE(HttpContext context)
        {
            context.Response.Write("{\"djje\":\"" + db.getDJJF(currentMember.UserId).ToString("F2") + "\"}");
        }
        //获取积分
        public void getJF(HttpContext context)
        {
            try
            {
                context.Response.Write("{\"jf\":\"" + db.getJF(currentMember.UserId) + "\"}");
            }
            catch (Exception ex)
            {
                db.makeJF(currentMember.UserId, 0);
                context.Response.Write("{\"jf\":\"0\"}");
            }
        }
        //获取经验
        public void getJY(HttpContext context)
        {
            context.Response.Write("{\"jy\":\"" + db.getJY(currentMember.UserId) + "\"}");
        }

        //设置为分销商
        public void makeFXS(HttpContext context)
        {
            string orderid = context.Request.Form["data"];
            OrderInfo orderinfo = new OrderDao().GetOrderInfo(orderid);
            Util.Utils.WriteLogs(orderinfo.UserId.ToString());
            Util.Utils.WriteLogs(orderinfo.GetTotal().ToString());
            string msg = "操作成功";
            if (MemberHelper.CreateDistributorByUserIds(orderinfo.UserId.ToString(), ref msg))
            {
                decimal total = orderinfo.GetTotal();
                if (db.addDAmount(orderinfo.UserId, total, 400000))
                {
                    if (!db.setGradeId(orderinfo.UserId, 11))
                    {
                        context.Response.Write("{\"msg\":\"操作失败\"}");
                    }
                }
                else
                {
                    context.Response.Write("{\"msg\":\"操作失败\"}");
                }
            }
            context.Response.Write("{\"msg\":\"" + msg + "\"}");

        }
        //注册送
        public void zhucesong(HttpContext context)
        {
            string str = context.Request.Form["data"];
            string msg = "操作成功";
            if (!db.setTotalAmount(Convert.ToInt32(str), 5))
            {
                msg = "操作失败";
            }
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
        //推荐注册送
        public void tuisong(HttpContext context)
        {
            //17634099996
            string str = context.Request.Form["uname"];
            string msg = "操作成功";
            int uid = db.getUserByBindName(str);
            //getShangjiUID
            int pid = db.getShangjiUID(uid);
            if (pid != 0)
            {
                if (!db.setTotalAmount(uid, 5))
                {
                    msg = "操作失败";
                }
                else
                {
                    if (!db.setTotalAmount(pid, 5))
                    {
                        msg = "操作失败";
                    }
                }
            }
            //if (!db.tuisong(str))
            //{
            //    msg = "操作失败";
            //}
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }

        //判断是否为代理
        public bool IsDaili(int mid)
        {
            if (db.getaspnet_DistributorsStatus(mid) == 0)
            {
                return true;
            }

            return false;
        }
        //充值送经验
        public void chongzhisong(HttpContext context)
        {
            string msg = "操作失败";
            string payid = context.Request.Form["payid"];
            if (this.IsDaili(currentMember.UserId))
            {
                int chongzhijine = Convert.ToInt32(db.getChongzhiJine(payid));
                if (db.updateJY(currentMember.UserId, chongzhijine))
                {
                    //返经验
                    if (fanjingyan(chongzhijine))
                    {
                        msg = "操作成功";
                    }
                }
            }
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
        //签到送
        public void qiandaoSong(HttpContext context)
        {
            string msg = "操作失败";
            string count = context.Request.Form["count"];
            if (this.IsDaili(currentMember.UserId))
            {
                if (fanjingyan(Convert.ToInt32(count)))
                {
                    msg = "操作成功";
                }
            }
            context.Response.Write("{\"msg\":\"" + msg + "\"}");

        }


        //经验返利
        public bool fanjingyan(int jy)
        {
            int sjy = Convert.ToInt32(Math.Floor(jy * 0.5));
            int pid = db.getShangjiUID(currentMember.UserId);
            int ppid = db.getShangjiUID(pid);
            if (pid != 0)
            {
                //是否是代理
                if (IsDaili(pid))
                {
                    //送经验
                    if (db.updateJY(pid, sjy))
                    {
                        if (db.updateJY(ppid, Convert.ToInt32(Math.Floor(sjy * 0.5))))
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }
        public void chekoucha(HttpContext context)
        {
            string msg = "操作失败";
            string orderId = context.Request.Form["orderId"];
            OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(orderId);
            decimal sum = orderInfo.Amount;
            zhekoufanli(sum);

            context.Response.Write("{\"msg\":\"" + sum + "\"}");
        }

        //折扣差返利
        /// <summary>
        /// 折扣差返利
        /// </summary>
        /// <param name="sum">订单金额</param>
        public bool zhekoufanli(decimal sum)
        {
            Util.Utils.WriteLogs("sum=" + sum);
            //获取上级ID
            int pid = db.getShangjiUID(currentMember.UserId);//上级用户ID
            int ujb = db.getUserGID(currentMember.UserId);//当前用户级别
            int pjb = db.getUserGID(pid);//上级用户级别
            int uzk = db.getDiscount(ujb);//用户折扣
            int pzk = db.getDiscount(pjb);//上级用户折扣

            fanjingyan(Convert.ToInt32(sum));

            if (ujb < pjb)//当前级别小于上级级别    返利折扣差
            {
                sum = sum / Convert.ToDecimal(uzk / 100.00);
                int zkc = uzk - pzk;
                Decimal ss = Convert.ToDecimal(zkc / 100.00);
                sum = sum * ss;
                if (db.setTotalAmount(pid, sum))
                {
                    return false;
                }
            }
            else//其余情况送 5%
            {
                // Util.Utils.WriteLogs("5%");
                // sum = sum * (uzk / 100);
                //Util.Utils.WriteLogs("sum" + sum);
                sum = Convert.ToDecimal(sum * 5 / 100);
                Util.Utils.WriteLogs("sum" + sum);
                if (db.setTotalAmount(pid, sum))
                {
                    //返利到账户成功
                    return false;
                }
            }
            return true;
        }


        public void getUserID(HttpContext context)
        {
            context.Response.Write("{\"id\":\"" + currentMember.UserId + "\"}");
        }
        /// <summary>
        /// 获取绑定金额
        /// </summary>
        /// <param name="context"></param>
        public void getBanddingjine(HttpContext context)
        {
            string mid = context.Request.Form["mid"];
            decimal bdje = db.getBanddingjine(Convert.ToInt32(mid));
            context.Response.Write(bdje.ToString("F2"));
        }
        //修正账户金额
        public void xiuzhengjine(HttpContext context)
        {
            int mid = currentMember.UserId;
            decimal zhje = db.getZhanghujine(mid);
            if (zhje < 0)
            {
                if (db.gxbdje(mid))
                {
                    context.Response.Write("{\"statc\":\"" + false + "\"}");
                }
                else
                {
                    context.Response.Write("{\"statc\":\"" + true + "\"}");
                }
            }
            else
            {
                context.Response.Write("{\"statc\":\"" + true + "\"}");
            }

        }
        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["action"])
            {
                case "getDJJE":
                    //this.IsDaili();
                    this.getDJJE(context);
                    return;
                case "getJF":
                    this.getJF(context);
                    return;
                case "getJY":
                    this.getJY(context);
                    return;
                case "makeFXS":
                    this.makeFXS(context);
                    return;
                case "zhucesongwuyuan":
                    this.zhucesong(context);
                    return;
                case "tuisong":
                    this.tuisong(context);
                    return;
                case "chongzhisong":
                    this.chongzhisong(context);
                    return;
                case "getUserID":
                    this.getUserID(context);
                    return;
                case "qiandaoSong":
                    this.qiandaoSong(context);
                    return;
                case "chekoucha":
                    this.chekoucha(context);
                    return;
                case "getBanddingjine":
                    this.getBanddingjine(context);
                    return;
                case "xiuzhengjine":
                    this.xiuzhengjine(context);
                    return;
            }
        }
    }
}

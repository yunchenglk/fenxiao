using Hidistro.ControlPanel.Members;
using Hidistro.Entities.Members;
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
            // MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            //{\"success\":false, \"msg\":\"提交失败\"}
            context.Response.Write("{\"djje\":\"" + db.getDJJF(currentMember.UserId) + "\"}");
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
            string str = context.Request.Form["data"];
            string[] arrs = str.Split(',');
            string msg = "操作成功";
            if (MemberHelper.CreateDistributorByUserIds(arrs[0].Trim(), ref msg))
            {
                string orderid = arrs[1].Trim();
                decimal total = new OrderDao().GetOrderInfo(orderid).GetTotal();
                if (db.addDAmount(Int32.Parse(arrs[0].Trim()), total, 400000))
                {
                    if (!db.setGradeId(Convert.ToInt32(arrs[0].Trim()), 11))
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
            if (!db.zhucesong(Convert.ToInt32(str), 5))
            {
                msg = "操作失败";
            }
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
        //推荐注册送
        public void tuisong(HttpContext context)
        {
            string str = context.Request.Form["uname"];
            string msg = "操作成功";
            if (!db.tuisong(str))
            {
                msg = "操作失败";
            }
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["action"])
            {
                case "getDJJE":
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

            }
        }
    }
}

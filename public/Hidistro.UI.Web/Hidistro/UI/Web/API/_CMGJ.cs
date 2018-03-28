using Hidistro.Entities.Members;
using Hidistro.SaleSystem.Vshop;
using Hidistro.SqlDal._CMGJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Hidistro.UI.Web.API
{
    public class _CMGJ : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        //获取会员积分
        public void getaspnet_members_jf(HttpContext context)
        {
            aspnet_members_jfSQL db = new aspnet_members_jfSQL();

            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            int p = db.getaspnet_members_jfByMID(currentMember.UserId);
            string msg = "";
            msg = "{\"success\":false,\"msg\":\"" + p + "\"}";

            context.Response.Write(msg);
            context.Response.End();
        }
        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["action"])
            {
                case "getaspnet_members_jf":
                    this.getaspnet_members_jf(context);
                    return;
            }
        }
    }
}

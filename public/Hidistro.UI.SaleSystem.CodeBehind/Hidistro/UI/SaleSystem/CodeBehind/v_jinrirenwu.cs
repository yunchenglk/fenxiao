using Hidistro.Entities.Members;
using Hidistro.UI.Common.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Hidistro.UI.SaleSystem.CodeBehind
{
    [ParseChildren(true)]
    public class v_jinrirenwu : VMemberTemplatedWebControl
    {
        protected override void AttachChildControls()
        {

        }
        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin_u_jinrirenwu.html";
            }
            base.OnInit(e);
        }
    }
}

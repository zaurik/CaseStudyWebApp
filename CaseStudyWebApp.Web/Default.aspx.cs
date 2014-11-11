using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CaseStudyWebApp.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                this.message.InnerText = "Welcome!";
            }
            else
            {
                this.message.InnerText = "Please log in or register.";
            }
        }
    }
}
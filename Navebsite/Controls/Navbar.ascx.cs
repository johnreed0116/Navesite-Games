﻿using System;
using System.Web.UI;
using NavebsiteBL;

namespace Navebsite.Controls
{
    public partial class Navbar : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            welcome.Text = "";
            var user = (User) Session["user"];
            profile.Visible = false;
            logout.Visible = false;
            login.Visible = false;
            register.Visible = false;
            library.Visible = false;
            if (Session["user"] != null)
            {
                welcome.Text = "Ahoy, " + Server.HtmlEncode(user.Username);
                profile.Visible = true;
                logout.Visible = true;
                library.Visible = true;
            }
            else
            {
                register.Visible = true;
                login.Visible = true;
            }
        }
    }
}
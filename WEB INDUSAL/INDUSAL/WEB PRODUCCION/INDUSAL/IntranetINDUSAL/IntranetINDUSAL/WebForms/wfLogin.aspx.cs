﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wfLogin : System.Web.UI.Page
{
    private clPermisos controlPermisos;

    protected void Page_Load(object sender, EventArgs e)
    {
        controlPermisos = new clPermisos();
        txUserName.Focus();
    }
    protected void btLogin_Click(object sender, EventArgs e)
    {
        if (controlPermisos.AutenticarUsuario(txUserName.Text))
        {
            Session["userID"] = txUserName.Text;
            Response.Redirect("~/WebForms/Principal.aspx");
        }
        else
        {
            lbErrorLogin.Visible = true;
        }

    }
}

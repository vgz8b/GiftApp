using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WebCamApplicationTheLast.Members
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string evnt = Request.QueryString["eventname"].ToString();
            string usrn = Request.QueryString["username"].ToString();
                
            SqlCommand com;

            string uname = HttpContext.Current.User.Identity.Name.ToString();
            SqlConnection con = new SqlConnection(connstr);
            com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = "Delete from aspnet_invites where username=@usrname and invitedby=@invbyna and eventname=@evntb";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("@usrname", uname);
            com.Parameters.AddWithValue("@invbyna", usrn);
            com.Parameters.AddWithValue("@evntb", evnt);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                delete.InnerHtml = "<font color=green>Successfullu Declined Event</font>";

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}
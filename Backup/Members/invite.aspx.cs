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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                string usrn = Request.QueryString["usr"].ToString();
                string evnt = Request.QueryString["eventname"].ToString();
                SqlCommand com;

                string uname = HttpContext.Current.User.Identity.Name.ToString();
                SqlConnection con = new SqlConnection(connstr);
                com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = "Insert into aspnet_invites values(@username,@invitedby,@eventname)";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@username", usrn);
                com.Parameters.AddWithValue("@invitedby", uname);
                com.Parameters.AddWithValue("@eventname", evnt);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    mess.InnerHtml = "<font color=green>Successfully Invited</font>";
                }
            }
            catch (Exception cx)
            {
                mess.InnerHtml = "<font color=red>Request Unsuccessful...try Again</font>";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}
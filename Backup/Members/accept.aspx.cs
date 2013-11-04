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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                com.CommandText = "Insert into aspnet_evntpresence values(@eventname,@username)";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@eventname", evnt);
                com.Parameters.AddWithValue("@username", uname);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    setpermission(uname,usrn);
                    accept.InnerHtml = "<font color=green>Successfully Accepted</font>";
                    deleteinvite(uname, usrn, evnt);
                }
            }
            catch (Exception rx)
            {
                accept.InnerHtml = rx.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
        public void setpermission(string cant, string seet)
        {
            
                string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                SqlCommand com;

                string uname = HttpContext.Current.User.Identity.Name.ToString();
                SqlConnection con = new SqlConnection(connstr);
                com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = "Insert into aspnet_permsn values(@can,@see)";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@can", cant);
                com.Parameters.AddWithValue("@see", seet);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
            
            
        }
        public void deleteinvite(string usname, string invby, string evntn)
        {

            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            SqlCommand com;

            string uname = HttpContext.Current.User.Identity.Name.ToString();
            SqlConnection con = new SqlConnection(connstr);
            com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = "Delete from aspnet_invites where username=@usrname and invitedby=@invbyna and eventname=@evntb";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("@usrname", usname);
            com.Parameters.AddWithValue("@invbyna", invby);
            com.Parameters.AddWithValue("@evntb", evntn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
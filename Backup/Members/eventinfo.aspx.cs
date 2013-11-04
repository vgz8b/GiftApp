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
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string evnt = Request.QueryString["event"].ToString();
             string str;
            SqlCommand com;
            SqlDataAdapter sqlda;
            DataTable dt;
            int rowcount;
           
            SqlConnection con = new SqlConnection(connstr);
            string uname = HttpContext.Current.User.Identity.Name.ToString();
            string ubar,usr,uplace,utime;
            string res = "";
            con.Open();
            
            str = "Select * from addevent";
            com = new SqlCommand(str);
            sqlda = new SqlDataAdapter(com.CommandText, con);
            dt = new DataTable();
            sqlda.Fill(dt);
            rowcount = dt.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                usr = dt.Rows[i]["username"].ToString();
                ubar = dt.Rows[i]["eventname"].ToString();
                
                uplace = dt.Rows[i]["place"].ToString();
                utime = dt.Rows[i]["time"].ToString();
                if (ubar == evnt)
                {
                    res = res + "<table border=1><tr><td>Username</td><td>Event Name</td><td>Place</td><td>Time</td></tr>";
                    res = res + "<tr><td><a href='Members.aspx?user=" + usr + "'>" + usr + "</a></td><td>" + ubar + "</td><td>" + uplace + "</td><td>" + utime + "</td></tr></table>";
                    eventsd.InnerHtml = res;
                }
            }
            con.Close();
            presentlist(evnt);

        }
        public void presentlist(string evnt)
        {
        string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            
             string str;
            SqlCommand com;
            SqlDataAdapter sqlda;
            DataTable dt;
            int rowcount;
           
            SqlConnection con = new SqlConnection(connstr);
            string uname = HttpContext.Current.User.Identity.Name.ToString();
            string ubar,usr;
            string res = "";
            con.Open();
            
            str = "Select * from aspnet_evntpresence";
            com = new SqlCommand(str);
            sqlda = new SqlDataAdapter(com.CommandText, con);
            dt = new DataTable();
            sqlda.Fill(dt);
            rowcount = dt.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                usr = dt.Rows[i]["username"].ToString();
                ubar = dt.Rows[i]["eventname"].ToString();
                if (ubar == evnt)
                {
                    res = res + "<table border=1><tr><td>" + evnt + "</td></tr>";
                    res = res + "<tr><td><a href='Members.aspx?user=" + usr + "'>" + usr + "</a></td></tr></table>";
                    liste.InnerHtml = res;
                }
            }
            con.Close();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}
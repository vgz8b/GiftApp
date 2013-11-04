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
    public partial class WebForm4 : System.Web.UI.Page
    {
        string uname=HttpContext.Current.User.Identity.Name.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            string usrn = Request.QueryString["username"].ToString();
            string barc = Request.QueryString["barcode"].ToString();
            if (uname != usrn)
            {
                SqlCommand com;
                 SqlConnection con = new SqlConnection(connstr);
                    com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = "Update aspnet_codes set plans=@plan where barcode=@bars";
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@plan", 1);
                    com.Parameters.AddWithValue("@bars", barc);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                        plan.InnerHtml = "<font color=green>Successfully planned</font>";
                    }
                  
                }
            updateplan(uname,usrn,barc);
                
            }

        public void updateplan(string una, string usrna, string barcs)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlCommand com;
            SqlConnection con = new SqlConnection(connstr);
            com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = "Insert into aspnet_plans values(@username,@productuser,@barcode)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("@username", una);
            com.Parameters.AddWithValue("@productuser", usrna);
            com.Parameters.AddWithValue("@barcode", barcs);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
               
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }


    }

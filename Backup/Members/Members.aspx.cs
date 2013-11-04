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
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        string uname = HttpContext.Current.User.Identity.Name.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            string usrn = Request.QueryString["user"].ToString();

            welcome.InnerHtml = "<h1>Welcome to " + usrn + " Profile";
                
           
            if (uname != usrn)
            {
                
                string str;
                SqlCommand com;
                SqlDataAdapter sqlda;
                DataTable dt;
                int rowcount;

                string ucode, ubar;
                string res = "";

                SqlConnection con = new SqlConnection(connstr);
                con.Open();
                str = "Select * from addevent";
                com = new SqlCommand(str);
                sqlda = new SqlDataAdapter(com.CommandText, con);
                dt = new DataTable();
                sqlda.Fill(dt);
                rowcount = dt.Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    ucode = dt.Rows[i]["eventname"].ToString();
                    ubar = dt.Rows[i]["username"].ToString();
                    string rest = checkinvite(usrn, uname, ucode);
                    if (ubar == uname && rest=="0")
                    {
                        res = res + "<a href='invite.aspx?eventname=" + ucode + "&usr=" + usrn + "'>Invite to " + ucode + " Event</a><br/>";
                        invite.InnerHtml = res;
                    }
                }
                con.Close();
            }

        }
        public string checkinvite(string usrn,string uname,string ucode)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            string str;
                SqlCommand com;
                SqlDataAdapter sqlda;
                DataTable dt;
                int rowcount;

                
                string res = "";
                int chk = 0;
                SqlConnection con = new SqlConnection(connstr);
                con.Open();
                str = "Select * from aspnet_invites";
                com = new SqlCommand(str);
                sqlda = new SqlDataAdapter(com.CommandText, con);
                dt = new DataTable();
                sqlda.Fill(dt);
                rowcount = dt.Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    string ucname = dt.Rows[i]["username"].ToString();
                    string ucinv = dt.Rows[i]["invitedby"].ToString();
                    string ucevnt = dt.Rows[i]["eventname"].ToString();
                    if (ucname == uname && ucinv == usrn)
                    {
                        if (ucevnt == ucode)
                        { 
                            chk = 1;
                        
                        }

                    }
                }
                if (chk == 1)
                {
                    res = res + "<font color=green>Already Invited for" + ucode + " Event</font>";
                    invite.InnerHtml = res;
                    return "1";
                }
                else
                {
                    return "0";
                }
            


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            string usrn = Request.QueryString["user"].ToString();


            if (uname != usrn)
            {

                string str;
                SqlCommand com;
                SqlDataAdapter sqlda;
                DataTable dt;
                int rowcount;

                string ucode, ubar;
               int res = 1;

                SqlConnection con = new SqlConnection(connstr);
                con.Open();
                str = "Select * from aspnet_permsn";
                com = new SqlCommand(str);
                sqlda = new SqlDataAdapter(com.CommandText, con);
                dt = new DataTable();
                sqlda.Fill(dt);
                rowcount = dt.Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    ucode = dt.Rows[i]["can"].ToString();
                    ubar = dt.Rows[i]["see"].ToString();
                    if (ucode == uname && ubar == usrn)
                    {
                       
                        res = 0;
                        
                    }
                }
                con.Close();
                if (res == 0)
                {

                    showlist(usrn);
                }
                else
                {
                    list.InnerHtml = "<font color=red>You have no permissions to view the list</font>";
                }
            }
        }
        public void showlist(string name)
        {
            string str;
            SqlCommand com;
            SqlDataAdapter sqlda;
            DataTable dt;
            int rowcount;
            string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnection con = new SqlConnection(connstr);
            string uname = HttpContext.Current.User.Identity.Name.ToString();
            string ubar, usr,stat;
            int chk = 0;
            string res = "";
            con.Open();

            str = "Select * from aspnet_codes";
            com = new SqlCommand(str);
            sqlda = new SqlDataAdapter(com.CommandText, con);
            dt = new DataTable();
            sqlda.Fill(dt);
            rowcount = dt.Rows.Count;
            
            
            for (int i = 0; i < rowcount; i++)
            {
                usr = dt.Rows[i]["username"].ToString();
                ubar = dt.Rows[i]["barcode"].ToString();
                stat = dt.Rows[i]["plans"].ToString();
                if (usr == name && stat=="0")
                {
                    chk = 1;
                    try
                    {
                        var url = "https://api.scandit.com/v2/products/" + ubar + "?key=suEZQ4YdpvcbSOwJ2_lfeCV_T_JhGB1VGWtXAnd5yv_";
                        var json = new System.Net.WebClient().DownloadString(url);

                        var jObj = (JObject)JsonConvert.DeserializeObject(json);


                        res = res + "<div><b>Product Name:" + jObj["basic"]["name"].ToString() + "</b></div>" + "<div><b>Product Category:" + jObj["basic"]["category"].ToString() + "</b></div>";

                        list.InnerHtml = res;



                    }
                    catch (Exception fx)
                    {
                        list.InnerHtml = res + "<div><b>product Name:<font color=red>No product Found</font></b></div>";
                    }
                    try
                    {
                        var prdct = "http://www.searchupc.com/handlers/upcsearch.ashx?request_type=3&access_token=DDF43D5B-33AE-4073-8E07-1EB1C62533CA&upc=" + ubar;
                        var prdctjs = new System.Net.WebClient().DownloadString(prdct);
                        var jsobj = (JObject)JsonConvert.DeserializeObject(prdctjs);

                        res = res + "<div><img src=" + jsobj["0"]["imageurl"] + "/><br/>";
                        res = res + "<table border=1><tr><td>Name</td><td>Price</td><td>Currency</td><td>Store</td><td>URL</td></tr>";
                        res = res + "<tr><td>" + jsobj["0"]["productname"] + "</td><td>" + jsobj["0"]["price"] + "</td><td>" + jsobj["0"]["currency"] + "</td><td>" + jsobj["0"]["storename"] + "</td><td><a href=" + jsobj["0"]["producturl"] + ">Click To Buy</a></td></tr></table></div>";
                        res = res + "<br/><a href='plan.aspx?barcode=" + ubar + "&username=" + usr + "'>Plan This</a>";
                        list.InnerHtml = res;

                    }
                    catch (Exception kx)
                    {
                        list.InnerHtml = res + "<div><b>Store Name:<font color=red>No store Found</font></b></div>";
                    }
                }
            }
            if (chk == 0)
            {
                list.InnerHtml = "<div><b>"+name+",has No products to show</b></div>";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}
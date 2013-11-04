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

namespace WebCamApplicationTheLast
{
    public partial class _Default : System.Web.UI.Page
    {
        string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
      
        protected void Page_Load(object sender, EventArgs e)
        {

           
            if (Session["ImgUrl"] != null)
            {
                Image1.Visible = true;
                Image1.ImageUrl = Session["ImgUrl"].ToString();
                IBarcodeReader reader = new BarcodeReader();
                // load a bitmap
                var barcodeBitmap = (Bitmap)Bitmap.FromFile("C:\\Users\\Tejaswi\\Downloads\\Backup\\Backup"+Image1.ImageUrl);
                // detect and decode the barcode inside the bitmap
                var result = reader.Decode(barcodeBitmap);
                // do something with the result
                if (result != null)
                {
                    txtDecodertype.InnerHtml = "<b>Type:"+result.BarcodeFormat.ToString()+"</b";
                    txtDecodercontent.InnerHtml = "<b>Barcode:"+result.Text+"</b>";
                    try
                    {
                        var url = "https://api.scandit.com/v2/products/"+ result.Text +"?key=suEZQ4YdpvcbSOwJ2_lfeCV_T_JhGB1VGWtXAnd5yv_";
                        var json = new System.Net.WebClient().DownloadString(url);

                        var jObj = (JObject)JsonConvert.DeserializeObject(json);
                        string res;

                        res = "<div><b>Product Name:" + jObj["basic"]["name"].ToString() + "</b></div>" + "<div><b>Product Category:" + jObj["basic"]["category"].ToString() + "</b></div>";
                        

                        js.InnerHtml = res;
                       

                    }
                    catch (Exception ex)
                    {
                        js.InnerHtml = "<div><b>product Name:<font color=red>No product Found</font></b></div>";
                    }
                    try
                    {
                        var prdct = "http://www.searchupc.com/handlers/upcsearch.ashx?request_type=3&access_token=DDF43D5B-33AE-4073-8E07-1EB1C62533CA&upc=" + result.Text;
                        var prdctjs = new System.Net.WebClient().DownloadString(prdct);
                        var jsobj = (JObject)JsonConvert.DeserializeObject(prdctjs);
                        string resjs;
                        resjs = "<div><img src=" + jsobj["0"]["imageurl"] + "/><br/>";
                        resjs = resjs + "<table border=1><tr><td>Name</td><td>Price</td><td>Currency</td><td>Store</td><td>URL</td></tr>";
                        resjs = resjs + "<tr><td>" + jsobj["0"]["productname"] + "</td><td>" + jsobj["0"]["price"] + "</td><td>" + jsobj["0"]["currency"] + "</td><td>" + jsobj["0"]["storename"] + "</td><td><a href=" + jsobj["0"]["producturl"] + ">Click To Buy</a></td></tr></table></div>";

                        buyp.InnerHtml = resjs;
                    }
                    catch (Exception px)
                    {
                        buyp.InnerHtml = "<div><b>Store Name:<font color=red>No store Found</font></b></div>";
                    }

                }
               
            }
        }
        protected int checkbarcode(string code,string uname)
        {
            string str;
            SqlCommand com;
            SqlDataAdapter sqlda;
            DataTable dt;
            int rowcount;
            int count=0;
            string ucode, ubar;
            SqlConnection con = new SqlConnection(connstr);
            con.Open();
            str = "Select * from aspnet_codes";
            com = new SqlCommand(str);
            sqlda = new SqlDataAdapter(com.CommandText, con);
            dt = new DataTable();
            sqlda.Fill(dt);
            rowcount = dt.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                ucode = dt.Rows[i]["username"].ToString();
                ubar = dt.Rows[i]["barcode"].ToString();
                if (ucode == uname && ubar == code)
                {
                    count = 1;
                }
               
            }
            con.Close();
            if (count == 1)
            {
                return 0;
            }
            else
            {
                return 1;
            }
            


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated )

            {
  SqlCommand com;
                int dec;
                string uname=HttpContext.Current.User.Identity.Name.ToString();

                if (Session["ImgUrl"] != null)
                {
                    Image1.Visible = true;
                    Image1.ImageUrl = Session["ImgUrl"].ToString();
                    IBarcodeReader reader = new BarcodeReader();
                    // load a bitmap
                    var barcodeBitmap = (Bitmap)Bitmap.FromFile("C:\\Users\\Tejaswi\\Downloads\\Backup\\Backup" + Image1.ImageUrl);
                    // detect and decode the barcode inside the bitmap
                    var result = reader.Decode(barcodeBitmap);



                    dec = checkbarcode(result.Text, uname);
                    if (dec == 1)
                    {
                        SqlConnection con = new SqlConnection(connstr);
                        com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        com.CommandText = "Insert into aspnet_codes values(@username,@barcode,@plans)";
                        com.Parameters.Clear();
                        com.Parameters.AddWithValue("@username", uname);

                        



                        com.Parameters.AddWithValue("@barcode", result.Text);

                        com.Parameters.AddWithValue("@plans", 0);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                            com.ExecuteNonQuery();
                            con.Close();
                            addlist.InnerHtml = "<font color=green>Successfully Added</font>";
                        }
                    }
                    else
                    {
                        addlist.InnerHtml = "<font color=red>Already Existed</font>";
                    }
                }
                else
                {
                    dec = checkbarcode(product.Text, uname);

                    if (dec == 1)
                    {
                        SqlConnection con = new SqlConnection(connstr);
                        com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        com.CommandText = "Insert into aspnet_codes values(@username,@barcode,@plans)";
                        com.Parameters.Clear();
                        com.Parameters.AddWithValue("@username", uname);

                        com.Parameters.AddWithValue("@barcode", product.Text);





                        com.Parameters.AddWithValue("@plans", 0);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                            com.ExecuteNonQuery();
                            con.Close();
                            addlist.InnerHtml = "<font color=green>Successfully Added</font>";
                        }
                    }
                    else
                    {
                        addlist.InnerHtml = "<font color=red>Already Existed</font>";
                    }
                }
                
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
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
                if (usr == uname)
                {
                    try
                    {
                        var url = "https://api.scandit.com/v2/products/" + ubar + "?key=suEZQ4YdpvcbSOwJ2_lfeCV_T_JhGB1VGWtXAnd5yv_";
                        var json = new System.Net.WebClient().DownloadString(url);

                        var jObj = (JObject)JsonConvert.DeserializeObject(json);


                        res = res + "<div><b>Product Name:" + jObj["basic"]["name"].ToString() + "</b></div>" + "<div><b>Product Category:" + jObj["basic"]["category"].ToString() + "</b></div>";

                        mylist.InnerHtml = res;
                       



                    }
                    catch (Exception fx)
                    {
                        mylist.InnerHtml = res + "<div><b>product Name:<font color=red>No product Found</font></b></div>";
                    }
                    try
                    {
                        var prdct = "http://www.searchupc.com/handlers/upcsearch.ashx?request_type=3&access_token=DDF43D5B-33AE-4073-8E07-1EB1C62533CA&upc=" + ubar;
                        var prdctjs = new System.Net.WebClient().DownloadString(prdct);
                        var jsobj = (JObject)JsonConvert.DeserializeObject(prdctjs);

                        res = res + "<div><img src=" + jsobj["0"]["imageurl"] + "/><br/>";
                        res = res + "<table border=1><tr><td>Name</td><td>Price</td><td>Currency</td><td>Store</td><td>URL</td></tr>";
                        res = res + "<tr><td>" + jsobj["0"]["productname"] + "</td><td>" + jsobj["0"]["price"] + "</td><td>" + jsobj["0"]["currency"] + "</td><td>" + jsobj["0"]["storename"] + "</td><td><a href=" + jsobj["0"]["producturl"] + ">Click To Buy</a></td></tr></table></div>";
                        mylist.InnerHtml = res;

                    }
                    catch (Exception kx)
                    {
                        mylist.InnerHtml = res + "<div><b>Store Name:<font color=red>No store Found</font></b></div>";
                    }
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Addevent.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
             string str;
            SqlCommand com;
            SqlDataAdapter sqlda;
            DataTable dt;
            int rowcount;
           
            SqlConnection con = new SqlConnection(connstr);
            string uname = HttpContext.Current.User.Identity.Name.ToString();
            string usr;
            string res = "";
            
            con.Open();
            
            str = "Select * from aspnet_Users";
            com = new SqlCommand(str);
            sqlda = new SqlDataAdapter(com.CommandText, con);
            dt = new DataTable();
            sqlda.Fill(dt);
            rowcount = dt.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                usr = dt.Rows[i]["username"].ToString();
                
                if (usr == search.Text || usr.Contains(search.Text))
                {
                    if (usr != uname)
                    {
                      
                        res = res + "<div><a href='/Members/Members.aspx?user=" + usr + "'>"+usr+"</a></div><br/>";
                        searchs.InnerHtml = res;

                    }
                }
            }

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string str;
            SqlCommand com;
            SqlDataAdapter sqlda;
            DataTable dt;
            int rowcount;
           
            SqlConnection con = new SqlConnection(connstr);
            string uname = HttpContext.Current.User.Identity.Name.ToString();
            string ubar,usr,invb;
            int res = 1;
            con.Open();
            
            str = "Select * from aspnet_invites";
            com = new SqlCommand(str);
            sqlda = new SqlDataAdapter(com.CommandText, con);
            dt = new DataTable();
            sqlda.Fill(dt);
            rowcount = dt.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                usr = dt.Rows[i]["username"].ToString();
                ubar = dt.Rows[i]["eventname"].ToString();
                invb = dt.Rows[i]["invitedby"].ToString();
                if (usr == uname)
                {
                    res = 0;
                    invt.InnerHtml = "<div><b><font color=green>" + ubar + "</font></b>&nbsp;<a href='/Members/accept.aspx?eventname=" + ubar + "&username=" + invb + "'>Accept</a>or<a href='/Members/decline.aspx?eventname=" + ubar + "&username=" + usr + "'>Decline</a>&nbsp;<a href='/Members/eventinfo.aspx?event="+ubar+"'>Check Event Info</a></div>";
                }
                
               
            }
            if (res == 1)
            {
                invt.InnerHtml = "You have no Invitations to accept";
            }

        }
        public void Clear()
        {
            Session.Remove("ImgUrl") ;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Clear(); 
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                var url = "https://api.scandit.com/v2/products/" + product.Text + "?key=suEZQ4YdpvcbSOwJ2_lfeCV_T_JhGB1VGWtXAnd5yv_";
                var json = new System.Net.WebClient().DownloadString(url);

                var jObj = (JObject)JsonConvert.DeserializeObject(json);
                string res;

                res = "<div><b>Product Name:" + jObj["basic"]["name"].ToString() + "</b></div>" + "<div><b>Product Category:" + jObj["basic"]["category"].ToString() + "</b></div>";


                js.InnerHtml = res;


            }
            catch (Exception ex)
            {
                js.InnerHtml = "<div><b>product Name:<font color=red>No product Found</font></b></div>";
            }
            try
            {
                var prdct = "http://www.searchupc.com/handlers/upcsearch.ashx?request_type=3&access_token=DDF43D5B-33AE-4073-8E07-1EB1C62533CA&upc=" + product.Text;
                var prdctjs = new System.Net.WebClient().DownloadString(prdct);
                var jsobj = (JObject)JsonConvert.DeserializeObject(prdctjs);
                string resjs;
                resjs = "<div><img src=" + jsobj["0"]["imageurl"] + "/><br/>";
                resjs = resjs + "<table border=1><tr><td>Name</td><td>Price</td><td>Currency</td><td>Store</td><td>URL</td></tr>";
                resjs = resjs + "<tr><td>" + jsobj["0"]["productname"] + "</td><td>" + jsobj["0"]["price"] + "</td><td>" + jsobj["0"]["currency"] + "</td><td>" + jsobj["0"]["storename"] + "</td><td><a href=" + jsobj["0"]["producturl"] + ">Click To Buy</a></td></tr></table></div>";

                buyp.InnerHtml = resjs;
            }
            catch (Exception px)
            {
                buyp.InnerHtml = "<div><b>Store Name:<font color=red>No store Found</font></b></div>";
            }
        }
    }
    
}

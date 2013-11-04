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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string GetFormattedDate(object dateTimeObject)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeObject.ToString(), out dateTime))
            {
                return String.Format("{0}-{1}-{2}T{3}:{4}:{5}",
                    dateTime.Year,
                    dateTime.Month.ToString().PadLeft(2, '0'),
                    dateTime.Day.ToString().PadLeft(2, '0'),
                    dateTime.Hour.ToString().PadLeft(2, '0'),
                    dateTime.Minute.ToString().PadLeft(2, '0'),
                    dateTime.Second.ToString().PadLeft(2, '0')
                    );
            }
            return null;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string connstr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                SqlCommand com;
                SqlConnection con = new SqlConnection(connstr);
                com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = "Insert into addevent values(@username,@eventname,@place,@time)";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@username", HttpContext.Current.User.Identity.Name);
                com.Parameters.AddWithValue("@eventname", eventname.Text);
                com.Parameters.AddWithValue("@place", place.Text);
                com.Parameters.AddWithValue("@time", TextBoxEditStageDetailsDateStart.Text);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    addevent.InnerHtml = "<font color=green>Successfully Added</font>";
                }
            }
            else
            {
                addevent.InnerHtml = "<font color=red>Make Sure You login</font>";
            }
        }
    }
}
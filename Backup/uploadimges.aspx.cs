using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using ZXing;
using System.Drawing;


namespace WebCamApplicationTheLast
{
    public partial class uploadimges : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Drawing.Image originalimg;
            string strFile = DateTime.Now.ToString("dd_MMM_yymmss") + ".jpg";

            FileStream log = new FileStream(Server.MapPath(strFile), FileMode.OpenOrCreate);

            byte[] buffer = new byte[1024];
            int c;
            while ((c = Request.InputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                log.Write(buffer, 0, c);
            }
            originalimg = System.Drawing.Image.FromStream(log);
            originalimg = originalimg.GetThumbnailImage(320, 240, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
            originalimg.Save(Server.MapPath("Images") + "\\" + strFile);
            //Write jpg filename to be picked up by regex and displayed on flash html page.
            log.Close();
            originalimg.Dispose();
            File.Delete(Server.MapPath(strFile));
            Response.Write("../Images/" + strFile);
            Session["ImgUrl"] = "/Images/" + strFile;
            Response.Redirect("PhotoCapture.aspx");
            
        }
        public bool ThumbnailCallback() { return false; }
    }
}
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoCapture.aspx.cs" Inherits="WebCamApplicationTheLast.PhotoCapture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
    
    .button
    {
     border:1px solid #ffffff;
     border-radius:3px 3px 3px 3px;
     color:#FFFFFF;
     background-color:#000000;
     width:88px;
    }
    
    </style>

</head>
<body style="overflow:hidden; max-height:400px; max-width:350px; background-color:#000000; color:#FFFFFF;">
    <form id="form1" runat="server">
    <div>
    	<table><tr><td valign="top">

	
	<!-- First, include the JPEGCam JavaScript Library -->
	<script type="text/javascript" src="webcam.js"></script>
	 <script type="text/javascript">

	     webcam.set_api_url('../uploadimges.aspx');
	     webcam.set_quality(90); // JPEG quality (1 - 100)
	     webcam.set_shutter_sound(true); // play shutter click sound

	     webcam.set_hook('onComplete', 'my_completion_handler');

	     function do_upload() {
	         // upload to server
	         document.getElementById('<%=upload_results.ClientID%>').innerHTML = '<h1>Yükleniyor...</h1>';
	         webcam.upload();
	     }

	     function my_completion_handler(msg) {
	         // extract URL 
	         if (msg.match(/(http\:\/\/\S+)/)) {
	             var image_url = RegExp.$1;
	             // show JPEG image in page
	             document.getElementById('<%=upload_results.ClientID%>').innerHTML =
                        '<h1>Resim Başarıyla Yüklendi!</h1>' +
                        '<img src="' + image_url + '';

	             // reset camera for another shot
	             webcam.reset();
	         }
	         else alert("Error: " + msg);
	     }

            </script>
    <table border="0" cellpadding="0" cellspacing="5">
                        <tr>
                            <td valign="top">
                                <h3 id="tk_pic" style="margin-left: 110px;">
                                    Capture barcode of product</h3>
                                <div id="pic_area">
                                    <table id="Table2" runat="server">
                                        <tr>
                                            <td>

                                                <script type="text/javascript" language="JavaScript">
                                                    document.write(webcam.get_html(320, 240));
                                                </script>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--<input type="button" value="Configure..." onclick="webcam.configure()" />--%>
                                                &nbsp;&nbsp;
                                                <input class="button" type="button" value="Çlick" onclick="webcam.freeze()" />
                                                &nbsp;&nbsp;
                                                <input class="button" type="button" value="Upload"  onclick="do_upload()" />
                                                &nbsp;&nbsp;
                                                <input class="button" type="button" value="Reset"  onclick="webcam.reset()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
	</td><td width="50">
                &nbsp;</td><td valign="top">
		
            
	</td></tr></table>
<div id="upload_results" runat="server" style="background-color:#eee;"></div>
    </div>
    </form>
</body>
</html>

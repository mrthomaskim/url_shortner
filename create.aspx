<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Create.aspx.vb" Inherits="Create" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!check out golinks.com!>
<html xmlns="http://www.w3.org/1999/xhtml">
 <link href="style.css" media="screen" rel="stylesheet" type="text/css" />
<head runat="server">
    <title>Trunk Club Go URL Shortner</title>
    <meta charset="utf-8" />
<!--[if IE]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

<link rel="stylesheet" href="style.css" type="text/css" />
    </head>
<body>
<div id="pagewidth" >
	<div id="header"> <img src="\images\tc-logo.png" alt="Trunk Club" style="max-height: 100%"/></div>
	<div id="wrapper" class="clearfix">
			<div id="maincol">
    <form id="form1" runat="server">
    <div><br/>Go will shorten your absurdly long links but not your life.<br />
        Go created links will only work internally at Trunk Club.<br />
        Go ahead and enjoy.</div>
    <div>
        <strong style="text-align: center">
        <br />
        What URL do you want to shorten:
      </strong>
    </div>
    <div>
      <asp:TextBox ID="txtURL" runat="server" Width="550px"></asp:TextBox>
      <asp:RequiredFieldValidator ID="rfvURL" runat="server" 
        ControlToValidate="txtURL" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
      <br />
&nbsp;</div>
    <div>
      <strong>Custom code (optional):
    </strong>
    </div>
    <div>
      <asp:Label ID="lblRoot" runat="server"></asp:Label>
      <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
      <br />
&nbsp;</div>
    <div>
      <asp:Button ID="btnSubmit" runat="server" Text="Shorten URL" />
    </div>
    <div>
      <asp:Label ID="lblMessage" runat="server"></asp:Label>
      <br />
      <asp:HyperLink ID="hlShortURL" runat="server" Target="_blank"></asp:HyperLink>
    </div>
    </form>
     </div>
	        <br />
            

	<div id="footer">
        <br />
        <center>
        <a href="mailto:helpdesk@trunkclub.com">help?</center></a>
	</div>
    
</body>
</html>

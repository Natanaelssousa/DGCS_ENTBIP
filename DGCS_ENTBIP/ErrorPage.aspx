<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="DGCS_ENTBIP.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="logo_natura2.ico" sizes="32x32" />

       <title>DGCS ENTBIP</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="Views/images/erro_aplicacao.png" />
        </div>
    </form>


             <script type="text/javascript">
             function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }
    </script>

</body>
</html>

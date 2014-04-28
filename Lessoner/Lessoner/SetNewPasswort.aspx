<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetNewPasswort.aspx.cs" Inherits="Lessoner.SetNewPasswort" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" id="viewpoint_device" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
    <script src="Javascript/Global.js"></script>
    <script src="Javascript/SetNewPasswort.js"></script>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
        </div>
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="page-header">
            <div class="container">
                <h2>Passwort festlegen</h2>
            </div>
        </div>
        <div class="container" style="text-align: center" id="SetPasswordContainer">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:TextBox TextMode="Password" runat="server" ID="Passwort" CssClass="form-control" placeholder="Passwort eingeben"></asp:TextBox><br />
                    <asp:TextBox TextMode="Password" runat="server" ID="RePasswort" CssClass="form-control" placeholder="Passwort erneut eingeben"></asp:TextBox><br />
                    <asp:LinkButton runat="server" CssClass="btn btn-primary pull-right" Text="Passwort Übernehmen" ID="SetPassword" OnClick="SetPassword_Click" OnClientClick="return SetPassword()"></asp:LinkButton><br />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="SetPassword" />
                </Triggers>
            </asp:UpdatePanel>
            <label style="display: none" class="label label-danger" id="PasswordsNotMatching">Die Passwörter stimmen nicht überein!</label>
            <div class="modal" id="LoadingModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" style="width: 170px !important; margin-top: 350px; margin-left: auto !important; margin-right: auto !important;">
                    <div class="modal-content">
                        <div class="modal-body">
                            <img src="Data/Images/loading.gif" alt="Lade" id="LoadingImage" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Schuelerverwaltung.aspx.cs" Inherits="Lessoner.Schuelerverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" id="viewpoint_device" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
    <script src="Javascript/profile.js"></script>
    <script src="Javascript/Global.js"></script>
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
    <title>Schüler Verwaltung</title>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                </div>
                <div class="collapse navbar-collapse">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="#">Hauptseite</a></li>
                        <li><a href="about.aspx">Über den Lessoner</a></li>
                        <li><a href="contact.aspx">Kontakt</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="page-header">
            <div class="container">
                <asp:UpdatePanel ID="PageHeader" runat="server">
                    <ContentTemplate>
                        <div class="col-sm-4">
                            <div class="btn-group maxwidth-xs">
                                <a class="btn btn-default dropdown-toggle maxwidth-xs pull-left" data-toggle="dropdown" runat="server" id="OpenClassMenu">Klasse<span class="caret"></span>
                                </a>
                                <ul class="dropdown-menu maxwidth-xs" id="ClassList" runat="server">
                                    <li><a onclick="">&lt;Neue Klasse&gt;</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <button type="button" class="btn btn-default maxwidth-xs">Verschieben</button>
                        </div>                        
                        <div class="col-sm-4">
                            <button type="button" class="btn btn-default maxwidth-xs hidden-xs pull-right">Klassenliste Drucken</button></div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="container">
            <asp:UpdatePanel runat="server" ID="MainList">
                <ContentTemplate>
                    <asp:Table runat="server" CssClass="table table-hover">
                        <asp:TableHeaderRow TableSection="TableHeader">
                            <asp:TableHeaderCell>Vorname</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Nachname</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Straße</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Hausnr.</asp:TableHeaderCell>
                            <asp:TableHeaderCell>PLZ</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Ort</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableFooterRow TableSection="TableFooter">
                            <asp:TableCell>
                                <button class="btn btn-default">Hinzufügen</button>
                            </asp:TableCell>
                        </asp:TableFooterRow>
                    </asp:Table>
                </ContentTemplate>
                <Triggers>

                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div runat="server" class="modal" id="LoadingModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog" style="width: 282px !important; margin-top: 350px;">
                <div class="modal-content">
                    <div class="modal-body">
                        <img src="Data/Images/loading.gif" alt="Lade" id="LoadingImage" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

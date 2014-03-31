<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lehrerverwaltung.aspx.cs" Inherits="Lessoner.Lehrerverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" id="viewpoint_device" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
    <script src="Javascript/Global.js"></script>
    <script src="Javascript/Lehrerverwaltung.js"></script>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <!--
        <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    -->
    <link href="CSS/Style.css" rel="stylesheet" />
    <title>Lehrer Verwaltung</title>
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
                <button type="button" class="btn btn-default maxwidth-xs hidden-xs pull-right">Lehrerliste Drucken</button>
            </div>
        </div>
        <div class="container">
            <div id="TeacherRightsKeeper" style="display: none">
                <div id="TeacherRights" runat="server"></div>
            </div>
            <asp:UpdatePanel runat="server" ID="MainList" UpdateMode="Always">
                <ContentTemplate>
                    <div id="soy" runat="server"></div>
                    <asp:Table runat="server" ID="TeacherList" CssClass="table">
                        <asp:TableHeaderRow TableSection="TableHeader">
                            <asp:TableHeaderCell>Titel</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Vorname</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Nachname</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Straße</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Hausnr.</asp:TableHeaderCell>
                            <asp:TableHeaderCell>PLZ</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Ort</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Loginname</asp:TableHeaderCell>
                            <asp:TableHeaderCell></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                    <button type="button" class="btn btn-default" onclick="AddNewTeacher()">Hinzufügen</button>
                    <button type="button" class="btn btn-primary pull-right" onclick="SaveTeachers()">Speichern</button>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="modal fade" id="DeleteConfirmModal" tabindex="-1" role="dialog" aria-labelledby="DeleteConfirmModalTitle" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="DeleteConfirmModalTitle">Bestätigen</h4>
                    </div>
                    <div class="modal-body">
                        Sind Sie sicher, dass sie diesen Lehrer löschen möchten?
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel runat="server" ID="DeleteConfirmPanel">
                            <ContentTemplate>
                                <asp:HiddenField runat="server" ID="DeleteTarget" />
                                <asp:LinkButton runat="server" CssClass="btn btn-default" ID="DeleteConfirmButton" OnClick="DeleteConfirmButton_Click" OnClientClick="jQuery('#LoadingModal').modal({backdrop:false, keyboard:false});">Ja</asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Nein</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div class="modal" id="LoadingModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 282px !important; margin-top: 350px; margin-left: auto !important; margin-right: auto !important;">
            <div class="modal-content">
                <div class="modal-body">
                    <img src="Data/Images/loading.gif" alt="Lade" id="LoadingImage" />
                </div>
            </div>
        </div>
    </div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Schuelerverwaltung.aspx.cs" Inherits="Lessoner.Schuelerverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" id="viewpoint_device" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
    <script src="Javascript/Global.js"></script>
    <script src="Javascript/Schuelerverwaltung.js"></script>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <!--
        <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    -->
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
                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="PageHeader">
                    <ContentTemplate>
                        <div class="col-sm-3">
                            <div class="btn-group maxwidth-xs" id="ClassSelecter" runat="server">
                                <a class="btn btn-default dropdown-toggle maxwidth-xs pull-left" data-toggle="dropdown" runat="server" id="OpenClassMenu">Keine Klasse<span class="caret"></span>
                                </a>
                                <ul class="dropdown-menu maxwidth-xs" id="ClassList" runat="server">
                                    <li><a onclick="jQuery('#NewClassModal').modal('show');">&lt;Neue Klasse&gt;</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-sm-3" style="text-align: center">
                            <button type="button" class="btn btn-default maxwidth-xs" onclick="jQuery('#RenameClassModal').modal('show')">Umbenennen</button>
                        </div>
                        <div class="col-sm-3" style="text-align: center">
                            <button type="button" class="btn btn-default maxwidth-xs" onclick="jQuery('#DeleteClassConfirmModal').modal('show')">Löschen</button>
                        </div>
                        <div class="col-sm-3">
                            <button type="button" class="btn btn-default maxwidth-xs hidden-xs pull-right">Klassenliste Drucken</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="container">
            <div id="StudentRightsKeeper" style="display: none">
                <div id="StudentRights" runat="server"></div>
            </div>
            <asp:UpdatePanel runat="server" ID="MainList" UpdateMode="Always">
                <ContentTemplate>
                    <div id="soy" runat="server"></div>
                    <asp:Table runat="server" ID="StudentList" CssClass="table">
                        <asp:TableHeaderRow TableSection="TableHeader">
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
                    <button type="button" class="btn btn-default" onclick="AddNewStudent()">Hinzufügen</button>
                    <button type="button" class="btn btn-primary pull-right" onclick="SaveStudents()">Speichern</button>
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
                        Sind Sie sicher, dass sie diesen Schüler löschen möchten?
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
        <div class="modal fade" id="DeleteClassConfirmModal" tabindex="-1" role="dialog" aria-labelledby="DeleteClassConfirmModalTitle" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="DeleteClassConfirmModalTitle">Bestätigen</h4>
                    </div>
                    <div class="modal-body">
                        Sind Sie sicher, dass sie diese Klasse und alle enthaltenen Schüler, Stundenpläne usw. löschen möchten?
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel runat="server" ID="DeleteClassConfirmPanel" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                <asp:LinkButton runat="server" CssClass="btn btn-default" ID="DeleteClassConfirmButton" OnClick="DeleteClassConfirmButton_Click" OnClientClick="jQuery('#LoadingModal').modal({backdrop:false, keyboard:false});">Ja</asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Nein</button>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DeleteClassConfirmButton" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="NewClassModal" tabindex="-1" role="dialog" aria-labelledby="NewClassModalTitle" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="NewClassUpdatePanel">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="NewClassModalTitle">Neue Klasse</h4>
                            </div>
                            <div class="modal-body">
                                Klassenname:
                                <asp:TextBox runat="server" ID="NewClassName" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton runat="server" ID="NewClassConfirm" OnClick="NewClassConfirm_Click" CssClass="btn btn-default" OnClientClick="jQuery('#LoadingModal').modal({backdrop:false, keyboard:false});">Übernehmen</asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Abbrechen</button>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="NewClassName" />
                        <asp:AsyncPostBackTrigger ControlID="NewClassConfirm" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="RenameClassModal" tabindex="-1" role="dialog" aria-labelledby="RenameClassModalTitle" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="RemoveClassPanel">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="RenameClassModalTitle">Neue Klasse</h4>
                            </div>
                            <div class="modal-body">
                                Neuer Klassenname:
                                <asp:TextBox runat="server" ID="RenameClassName" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton runat="server" ID="RenameClassConfirm" OnClick="RenameClassConfirm_Click" CssClass="btn btn-default" OnClientClick="jQuery('#LoadingModal').modal({backdrop:false, keyboard:false});">Übernehmen</asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Abbrechen</button>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RenameClassName" />
                        <asp:AsyncPostBackTrigger ControlID="RenameClassConfirm" />
                    </Triggers>
                </asp:UpdatePanel>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Lessoner.aspx.cs" Inherits="Lessoner.Lessoner" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!--Scripts oben wegen abruffehler-->
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Javascript/LessonerBuilder.js"></script>
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:ScriptManager runat="server"></asp:ScriptManager>
                <!-- Fehleranzeige ----------------------------->
                <div class="alert alert-danger alert-dismissable" id="ErrorDisplay" style="display: none">
                    <button type="button" class="close" aria-hidden="true" onclick="CloseError()">&times;</button>
                    <strong>Fehler: </strong>
                    <label id="ErrorText"></label>
                </div>
                <!---------------------------------------------->
                <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
                    <div class="container">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <a class="navbar-brand" href="#">Lessoner</a>
                        </div>
                        <div class="collapse navbar-collapse">
                            <ul class="nav navbar-nav">
                                <li class="active"><a href="#">Hauptseite</a></li>
                                <li><a href="about.aspx">Über den Lessoner</a></li>
                                <li><a href="kontakt.aspx">Kontakt</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="page-header">
                    <div class="container">
                        <div class="input-group left" style="float: left">
                            <asp:LinkButton CssClass="btn btn-default LessonerButtonLeft DisabledATag" ID="btnLastDate" runat="server" OnClick="btnLastDate_Click" OnClientClick="OpenLoadingIndicator('true');">
                                <span class="glyphicon glyphicon-arrow-left"></span>
                            </asp:LinkButton>
                            <asp:TextBox CssClass="form-control LessonerControlTextBox" ID="txtWeekBegin" runat="server" ReadOnly="true" />
                            <asp:LinkButton CssClass="btn btn-default LessonerButtonRight" ID="btnNextDate" runat="server" OnClick="btnNextDate_Click" OnClientClick="OpenLoadingIndicator('true');">
                                <span class="glyphicon glyphicon-arrow-right"></span>
                            </asp:LinkButton>
                        </div>

                        <div class="btn-group" style="float: right" id="divClassSelect" runat="server">
                            <a class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="lbtnOpenClassMenu">KLASSE<span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu" id="ClassList" runat="server">
                            </ul>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                </div>
                <div class="container">
                    <asp:Table runat="server" ID="tbTimetable" CssClass="table table-bordered" EnableViewState="false">
                        <asp:TableHeaderRow TableSection="TableHeader">
                            <asp:TableHeaderCell CssClass="tableStunde" runat="server">Zeit</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Montag
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Dienstag
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Mitwoch
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Donnerstag
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Freitag
                            </asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tbTimetable" />
                <asp:AsyncPostBackTrigger ControlID="lbtnOpenClassMenu" />
                <asp:AsyncPostBackTrigger ControlID="btnNextDate" />
                <asp:AsyncPostBackTrigger ControlID="btnLastDate" />
                <asp:AsyncPostBackTrigger ControlID="txtWeekBegin" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
<script src="Bootstrap/js/bootstrap.js"></script>
<script src="Javascript/LoginScript.js"></script>
<script src="Javascript/Global.js"></script>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/LessonerBuilder.aspx.cs" Inherits="Lessoner.LessonerBuilder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
</head>
<body onload="CheckLoggedin('Lessoner.aspx'); GetData()">
    <form runat="server">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
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
                            <asp:Panel class="navbar-form navbar-right" ID="LoginForm" runat="server">
                                <asp:ScriptManager runat="server"></asp:ScriptManager>
                                <asp:Panel runat="server" ID="LoginControlls">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" placeholder="Email" CssClass="form-control" ID="txtUsername"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox TextMode="Password" placeholder="Passwort" CssClass="form-control" ID="txtPasswort" runat="server"></asp:TextBox>
                                    </div>
                                    <asp:Button CssClass="btn btn-success" Text="Anmelden" OnClick="btnLoginSubmit_Click" ID="btnLoginSubmit" runat="server" />
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="page-header">
                    <div class="container">
                        <div class="input-group">
                            <asp:LinkButton CssClass="btn btn-default LessonerButtonLeft DisabledATag" id="btnLastDate" runat="server" OnClick="btnLastDate_Click">
                                <span class="glyphicon glyphicon-arrow-left"></span>
                            </asp:LinkButton>
                            <asp:TextBox CssClass="form-control LessonerControlTextBox" id="txtWeekBegin" runat="server" />
                            <asp:LinkButton CssClass="btn btn-default LessonerButtonRight" id="btnNextDate"  runat="server" onclick="btnNextDate_Click">
                                <span class="glyphicon glyphicon-arrow-right"></span>
                            </asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                Klasse1<span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <asp:Table runat="server" ID="tbTimetable" class="table table-bordered">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell CssClass="tableStunde">Zeit</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Montag</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Dienstag</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Mitwoch</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Donnerstag</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Freitag</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtUsername" />
                <asp:AsyncPostBackTrigger ControlID="txtPasswort" />
                <asp:AsyncPostBackTrigger ControlID="btnLoginSubmit" />
                <asp:AsyncPostBackTrigger ControlID="tbTimetable" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
<script src="JQuery/jquery-1.10.2.js"></script>
<script src="Bootstrap/js/bootstrap.js"></script>
<script src="Javascript/LoginScript.js"></script>
<script src="Javascript/LessonerBuilder.js"></script>
<script src="Javascript/Global.js"></script>
</html>

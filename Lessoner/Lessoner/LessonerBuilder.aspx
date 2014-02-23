<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/LessonerBuilder.aspx.cs" Inherits="Lessoner.LessonerBuilder" Async="true"%>

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
                            <asp:Panel CssClass="navbar-form navbar-right" ID="LoginForm" runat="server">
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
                        <div class="input-group left" style="float: left">
                            <asp:LinkButton CssClass="btn btn-default LessonerButtonLeft DisabledATag" ID="btnLastDate" runat="server" OnClick="btnLastDate_Click">
                                <span class="glyphicon glyphicon-arrow-left"></span>
                            </asp:LinkButton>
                            <asp:TextBox CssClass="form-control LessonerControlTextBox" ID="txtWeekBegin" runat="server" />
                            <asp:LinkButton CssClass="btn btn-default LessonerButtonRight" ID="btnNextDate" runat="server" OnClick="btnNextDate_Click">
                                <span class="glyphicon glyphicon-arrow-right"></span>
                            </asp:LinkButton>
                        </div>
                        <div class="btn-group" style="float: right">
                            <asp:LinkButton CssClass="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" ID="lbtnOpenClassMenu">
                                KLASSE<span class="caret"></span>
                            </asp:LinkButton>
                            <ul class="dropdown-menu" id="ClassList" runat="server">
                            </ul>
                        </div>

                    </div>
                <div style="width: 100%; border-bottom: 1px solid #eee; margin-top: 20px; margin-bottom: 5px;"></div>
                <div class="row">
                </div>
                </div>
                <div class="container">
                    <asp:Table runat="server" ID="tbTimetable" CssClass="table table-bordered">
                        <asp:TableHeaderRow TableSection="TableHeader">
                            <asp:TableHeaderCell CssClass="tableStunde">Zeit</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Montag</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Dienstag</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Mitwoch</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Donnerstag</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag">Freitag</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                    <!--
                            <asp:TableRow>
                            <asp:TableCell CssClass="LessonerBuilderCell">
                                    <button type="button" class="LessonEditButton btn-xs" style="float:left"><span class="glyphicon glyphicon-remove"></span></button>
                                    <button type="button" class="LessonEditButton btn-xs" style="float:right"><span class="glyphicon glyphicon-pencil"></span></button>
                            </asp:TableCell>
                            <asp:TableCell CssClass="LessonerBuilderCell">
                                    <button type="button" class="LessonEditButton btn-xs" style="float:left"><span class="glyphicon glyphicon-remove"></span></button>
                                    <button type="button" class="LessonEditButton btn-xs" style="float:right"><span class="glyphicon glyphicon-plus"></span></button>
                            </asp:TableCell>
                        </asp:TableRow>
                        -->
                </div>
                <!--Dialoge-->
                <div class="modal fade" id="LessonEdit" tabindex="-1" role="dialog" aria-labelledby="LessonEditTitle" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="LessonEditTitle">Modal title</h4>
                            </div>
                            <div class="modal-body">
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                                <button type="button" class="btn btn-primary">Übernehmen</button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtUsername" />
                <asp:AsyncPostBackTrigger ControlID="txtPasswort" />
                <asp:AsyncPostBackTrigger ControlID="btnLoginSubmit" />
                <asp:AsyncPostBackTrigger ControlID="tbTimetable" />
                <asp:AsyncPostBackTrigger ControlID="lbtnOpenClassMenu" />
                <asp:AsyncPostBackTrigger ControlID="btnNextDate" />
                <asp:AsyncPostBackTrigger ControlID="btnLastDate" />
                <asp:AsyncPostBackTrigger ControlID="txtWeekBegin" />
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

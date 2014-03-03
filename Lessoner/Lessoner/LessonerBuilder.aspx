<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/LessonerBuilder.aspx.cs" Inherits="Lessoner.LessonerBuilder" Async="true" %>

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
                            <asp:TextBox CssClass="form-control LessonerControlTextBox" ID="txtWeekBegin" runat="server" ReadOnly="true" />
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
                    <div class="row">
                    </div>
                </div>
                <div class="container">
                    <asp:Table runat="server" ID="tbTimetable" CssClass="table table-bordered" EnableViewState="false">
                        <asp:TableHeaderRow TableSection="TableHeader">
                            <asp:TableHeaderCell CssClass="tableStunde" runat="server">Zeit</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Montag
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditMonday">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Dienstag
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditTuesday">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Mitwoch
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditWednesday">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Donnerstag
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditThursday">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Freitag
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditFriday">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
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
                                <h4 class="modal-title" id="LessonEditTitle">Stunde bearbeiten</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row" style="text-align: center">
                                    <div class="col-md-4">
                                        <div class="btn-group" style="float: left">
                                            <!--TODO: Platzhalter entfernen(?)-->
                                            <button class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="ddLessonName">
                                                Chicken nuggets
                                         <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu" id="ulLessonNames" runat="server">
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="btn-group">
                                            <button class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="ddTeacher">
                                                Do you want Soy Susedg with that?
                                         <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu" id="ulTeacher" runat="server" style="text-align: left !important">
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="btn-group" style="float: right">
                                            <button class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="ddLessonMod">
                                                No *runs away*
                                         <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu" id="ulLessonMod" runat="server">
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4" style="float: left">
                                        <div class="input-group">
                                            <span class="input-group-addon">Von</span>
                                            <asp:TextBox runat="server" ID="txtCountBegin" CssClass="form-control LessonerNumericBox" Text="Gimme back my Nuggets" ReadOnly="true" />
                                            <span class="input-group-addon UpDownButtonContainer">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="IncBegin" OnClick="IncBegin_Click"><span class="glyphicon glyphicon-chevron-up UpDownGlyph" style="left:0.75pt;"></span></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="DecBegin" OnClick="DecBegin_Click"><span class="glyphicon glyphicon-chevron-down UpDownGlyph"></span></asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-xs-4"></div>
                                    <div class="col-xs-4" style="float: left">
                                        <div class="input-group">
                                            <span class="input-group-addon">Bis</span>
                                            <asp:TextBox runat="server" ID="txtCountEnd" CssClass="form-control LessonerNumericBox" Text="*Calls Painis Cupcake*" ReadOnly="true" />
                                            <span class="input-group-addon UpDownButtonContainer">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="IncEnd" OnClick="IncEnd_Click"><span class="glyphicon glyphicon-chevron-up UpDownGlyph" style="left:0.75pt;"></span></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="DecEnd" OnClick="DecEnd_Click"><span class="glyphicon glyphicon-chevron-down UpDownGlyph"></span></asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                                <asp:Button runat="server" CssClass="btn btn-primary" Text="Übernehmen" ID="btnApply" OnClick="Apply_Click" data-lessonid="" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade AbortModal" id="AskAbort" tabindex="-1" role="dialog" aria-labelledby="AskAbortTitle" aria-hidden="true">
                    <div class="modal-dialog modal-sm AbortModalDialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="AskAbortTitle">Achtung</h4>
                            </div>
                            <div class="modal-body">
                                <!--<span class="glyphicon glyphicon-warning-sign" style="float: left; font-size: 38px; margin-right: 20px;"></span>-->
                                Möchten sie die änderungen an dieser Stunde speichern?
                            </div>
                            <div class="modal-footer" style="margin-top: 0px; text-align: center;">
                                <asp:Button runat="server" CssClass="btn btn-default" Text="Speichern" OnClick="Apply_Click" ID="btnSave" />
                                <button type="button" class="btn btn-default" onclick="HideLessonEditModal();">Nicht Speichern</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Zurück</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="DeleteConfirm" tabindex="-1" role="dialog" aria-labelledby="DeleteConfirmTitle" aria-hidden="true">
                    <div class="modal-dialog modal-sm AbortModalDialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="DeleteConfirmTitle">Bestätigen</h4>
                            </div>
                            <div class="modal-body">
                                <!--<span class="glyphicon glyphicon-warning-sign" style="float: left; font-size: 38px; margin-right: 20px;"></span>-->
                                Sind sie sicher, dass sie diese Stunde entfernen möchten?
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn btn-default" Text="Ja" OnClick="btnDeleteConfirm_Click" ID="btnDeleteConfirm" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Nein</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="EditDay" tabindex="-1" role="dialog" aria-labelledby="EditDayTitle" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="EditDayTitle">Tag bearbeiten</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <!--TODO: Eigene Checkbox Programmieren?-->
                                        <asp:CheckBox runat="server" Text="Findet statt" ID="chkTakesPlace" OnCheckedChanged="chkTakesPlace_CheckedChanged" AutoPostBack="true" Checked="false" /><!--Nah, Place already taken-->
                                    </div>
                                    <div class="col-md-8">
                                        <div class="input-group" style="float: right">
                                            <span class="input-group-addon">Information</span>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDayInfo"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                                    <asp:Button CssClass="btn btn-primary" runat="server" ID="btnApplyDay" OnClick="ApplyDay_Click" Text="Übernehmen" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade AbortModal" id="AbortDay" tabindex="-1" role="dialog" aria-labelledby="AskAbortTitle" aria-hidden="true">
                    <div class="modal-dialog modal-sm AbortModalDialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="AbortDayTitle">Achtung</h4>
                            </div>
                            <div class="modal-body">
                                <!--<span class="glyphicon glyphicon-warning-sign" style="float: left; font-size: 38px; margin-right: 20px;"></span>-->
                                Möchten sie die änderungen an diesem Tag speichern?
                            </div>
                            <div class="modal-footer" style="margin-top: 0px; text-align: center;">
                                <asp:Button runat="server" CssClass="btn btn-default" Text="Speichern" OnClick="ApplyDay_Click" ID="btnSaveDay" />
                                <button type="button" class="btn btn-default" onclick="HideDayEditModdalWithAbort();">Nicht Speichern</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Zurück</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal" id="LoadingModal" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-body"><!--TODO:Ladezeichen ändern(?)-->
                                <img src="Data/Images/loading.gif" alt="Lade"/>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnEditMonday" />
                <asp:AsyncPostBackTrigger ControlID="btnEditTuesday" />
                <asp:AsyncPostBackTrigger ControlID="btnEditWednesday" />
                <asp:AsyncPostBackTrigger ControlID="btnEditThursday" />
                <asp:AsyncPostBackTrigger ControlID="btnEditFriday" />
                <asp:AsyncPostBackTrigger ControlID="chkTakesPlace" />
                <asp:AsyncPostBackTrigger ControlID="txtDayInfo" />
                <asp:AsyncPostBackTrigger ControlID="IncBegin" />
                <asp:AsyncPostBackTrigger ControlID="DecBegin" />
                <asp:AsyncPostBackTrigger ControlID="IncEnd" />
                <asp:AsyncPostBackTrigger ControlID="DecEnd" />
                <asp:AsyncPostBackTrigger ControlID="IncBegin" />
                <asp:AsyncPostBackTrigger ControlID="DecBegin" />
                <asp:AsyncPostBackTrigger ControlID="txtUsername" />
                <asp:AsyncPostBackTrigger ControlID="txtPasswort" />
                <asp:AsyncPostBackTrigger ControlID="btnLoginSubmit" />
                <asp:AsyncPostBackTrigger ControlID="tbTimetable" />
                <asp:AsyncPostBackTrigger ControlID="lbtnOpenClassMenu" />
                <asp:AsyncPostBackTrigger ControlID="btnNextDate" />
                <asp:AsyncPostBackTrigger ControlID="btnLastDate" />
                <asp:AsyncPostBackTrigger ControlID="txtWeekBegin" />
                <asp:AsyncPostBackTrigger ControlID="btnApply" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
<script src="Bootstrap/js/bootstrap.js"></script>
<script src="Javascript/LoginScript.js"></script>
<script src="Javascript/Global.js"></script>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/LessonerBuilder.aspx.cs" Inherits="Lessoner.LessonerBuilder" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewpoint_device" />
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
    <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" UpdateMode="Always" ID="MainPanel">
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
                            <div class="navbar-form navbar-right">
                                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="LoginControllsUpdatePanel">
                                    <ContentTemplate>
                                        <div class="btn-group" style="display: none" runat="server" id="PageDropDown">
                                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                                <span runat="server" id="User"></span><span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu">
                                                <li id="LinkLessoner" runat="server"><a href="/lessoner.aspx">Stundenplan</a></li>
                                                <li id="LinkLessonerBuilder" runat="server"><a href="/lessonerbuilder.aspx">Stundenplanerstellung</a></li>
                                                <li id="LinkStudentManagement" runat="server"><a href="/schuelerverwaltung.aspx">Schülerverwaltung</a></li>
                                                <li id="LinkTeacherMamagement" runat="server"><a href="/lehrerverwaltung.aspx">Lehrerverwaltung</a></li>
                                                <li role="presentation" class="divider"></li>
                                                <li><a>Passwort ändern</a></li>
                                                <li><asp:LinkButton ID="Logoutbutton" runat="server" OnClick="Logoutbutton_Click">Abmelden</asp:LinkButton></li>
                                            </ul>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
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

                        <div class="btn-group" style="float: right">
                            <a class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="lbtnOpenClassMenu">KLASSE<span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu" id="ClassList" runat="server">
                            </ul>
                        </div>
                        <button class="btn btn-default" style="float: right;" data-toggle="modal" data-target="#CopyModal">Kopieren</button>
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
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditMonday" OnClientClick="OpenLoadingIndicator('true');">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Dienstag
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditTuesday" OnClientClick="OpenLoadingIndicator('true');">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Mitwoch
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditWednesday" OnClientClick="OpenLoadingIndicator('true');">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Donnerstag
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditThursday" OnClientClick="OpenLoadingIndicator('true');">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="tableTag" runat="server">
                                Freitag
                                <asp:LinkButton CssClass="LessonEditButton btn-xs" runat="server" OnClick="EditDay_Click" ID="btnEditFriday" OnClientClick="OpenLoadingIndicator('true');">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                            </asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
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
                                        <div class="btn-group pull-left">
                                            <button class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="ddLessonName">
                                                <span class="caret">Kein Fach</span>
                                            </button>
                                            <ul class="dropdown-menu" id="ulLessonNames" runat="server" style="text-align: left !important">
                                                <li><a onclick="jQuery('#NewLessonModal').modal({backdrop:false});">&lt;Neues Fach&gt;</a></li>
                                            </ul>
                                        </div>
                                        <button type="button" runat="server" class="pull-left btn btn-danger pull-right" id="RemoveLessonName" onclick="ReadyRemoveLessonName()"><span class="glyphicon glyphicon-remove"></span></button>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="btn-group">
                                            <button class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="ddTeacher">
                                                <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu" id="ulTeacher" runat="server" style="text-align: left !important">
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="btn-group" style="float: right">
                                            <button class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="ddLessonMod">
                                                <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu" id="ulLessonMod" runat="server" enableviewstate="false" style="text-align: left !important">
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4" style="float: left">
                                        <div class="input-group">
                                            <span class="input-group-addon">Von</span>
                                            <asp:TextBox runat="server" ID="txtCountBegin" CssClass="form-control LessonerNumericBox" Text="" ReadOnly="true" />
                                            <span class="input-group-addon UpDownButtonContainer">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="IncBegin" OnClientClick="btnIncBegin_Click(); return false;"><span class="glyphicon glyphicon-chevron-up UpDownGlyph" style="left:0.75pt;"></span></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="DecBegin" OnClientClick="btnDecBegin_Click(); return false;"><span class="glyphicon glyphicon-chevron-down UpDownGlyph"></span></asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-xs-4"></div>
                                    <div class="col-xs-4" style="float: left">
                                        <div class="input-group">
                                            <span class="input-group-addon">Bis</span>
                                            <asp:TextBox runat="server" ID="txtCountEnd" CssClass="form-control LessonerNumericBox" Text="" ReadOnly="true" />
                                            <span class="input-group-addon UpDownButtonContainer">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="IncEnd" OnClientClick="btnIncEnd_Click(); return false;"><span class="glyphicon glyphicon-chevron-up UpDownGlyph" style="left:0.75pt;"></span></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="DecEnd" OnClientClick="btnDecEnd_Click(); return false;"><span class="glyphicon glyphicon-chevron-down UpDownGlyph"></span></asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <div class="btn-group pull-left" style="float: right">
                                            <button class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server" id="ddRoom">
                                                <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu" id="RoomList" runat="server" enableviewstate="false" style="text-align: left !important">
                                                <li><a onclick="jQuery('#NewRoomModal').modal({backdrop:false});">&lt;Neuer Raum&gt;</a></li>
                                            </ul>
                                        </div>
                                        <button type="button" runat="server" class="pull-left btn btn-danger pull-right" id="RemoveRoom" onclick="ReadyRemoveRoom()"><span class="glyphicon glyphicon-remove"></span></button>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                                <asp:Button runat="server" CssClass="btn btn-primary" Text="Übernehmen" ID="btnApply" OnClick="Apply_Click" data-lessonid="" OnClientClick="HideLessonEditModalNoAbort();OpenLoadingIndicator('false');" />
                            </div>
                        </div>
                    </div>
                    <%--Variablen--%>
                    <asp:HiddenField ID="Modal_TeacherID" runat="server" Value="" />
                    <asp:HiddenField ID="Modal_LessonNameID" runat="server" Value="" />
                    <asp:HiddenField ID="Modal_LessonModID" runat="server" Value="" />
                    <asp:HiddenField ID="Modal_LessonBegin" runat="server" Value="" />
                    <asp:HiddenField ID="Modal_LessonEnd" runat="server" Value="" />
                    <asp:HiddenField ID="Modal_RoomID" runat="server" Value="" />
                    <%--_________________--%>
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
                                Sind sie sicher, dass sie diese Stunde entfernen möchten?
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn btn-default" Text="Ja" OnClick="btnDeleteConfirm_Click" ID="btnDeleteConfirm" OnClientClick="CloseDeleteConfirmModal();OpenLoadingIndicator('true');" />
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
                                        <asp:CheckBox runat="server" Text="Findet statt" ID="chkTakesPlace" Checked="false" /><!--Nah, Place already taken-->
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
                                    <asp:Button CssClass="btn btn-primary" runat="server" ID="btnApplyDay" OnClick="ApplyDay_Click" Text="Übernehmen" OnClientClick="HideDayEditModal();OpenLoadingIndicator('true');" />
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
                                Möchten sie die änderungen an diesem Tag speichern?
                            </div>
                            <div class="modal-footer" style="margin-top: 0px; text-align: center;">
                                <asp:Button runat="server" CssClass="btn btn-default" Text="Speichern" OnClick="ApplyDay_Click" ID="btnSaveDay" OnClientClick="OpenLoadingIndicator('false');" />
                                <button type="button" class="btn btn-default" onclick="HideDayEditModdalWithAbort();">Nicht Speichern</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Zurück</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="CopyModal" tabindex="-1" role="dialog" aria-labelledby="CopyModalTitle" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="CopyModalTitle">Stundenplan Kopieren</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="input-group" id="DateFromGroup">
                                            <span class="input-group-addon">Von</span>
                                            <input type="text" class="form-control" runat="server" id="txtDateFrom" onblur="txtDateFrom_Blur()" />
                                            <span class="input-group-addon UpDownButtonContainer">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <a id="btnDateFromInc" onclick="btnDateFromInc_Click();"><span class="glyphicon glyphicon-chevron-up UpDownGlyph" style="left: 0.75pt;"></span></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a id="btnDateFromDec" onclick="btnDateFromDec_Click();"><span class="glyphicon glyphicon-chevron-down UpDownGlyph"></span></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="input-group" id="DateToGroup">
                                            <span class="input-group-addon">Bis</span>
                                            <input type="text" class="form-control" runat="server" id="txtDateTo" onblur="txtDateTo_Blur()" />
                                            <span class="input-group-addon UpDownButtonContainer">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <a id="btnDateToInc" onclick="btnDateToInc_Click();"><span class="glyphicon glyphicon-chevron-up UpDownGlyph" style="left: 0.75pt;"></span></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a id="btnDateToDec" onclick="btnDateToDec_Click();"><span class="glyphicon glyphicon-chevron-down UpDownGlyph"></span></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="input-group" style="width: 50%; margin: auto">
                                            <span class="input-group-addon">Alle</span>
                                            <input type="text" id="txtWeekSpace" class="form-control LessonerNumericBox" readonly="readonly" />
                                            <span class="input-group-addon UpDownButtonContainer">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <a id="btnWeekSpaceInc" onclick="WeekSpaceInc();"><span class="glyphicon glyphicon-chevron-up UpDownGlyph" style="left: 0.75pt;"></span></a>
                                                        </td>
                                                        <td rowspan="2">Wochen</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a id="btnWeekSpaceDec" onclick="WeekSpaceDec();"><span class="glyphicon glyphicon-chevron-down UpDownGlyph"></span></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                                <asp:LinkButton CssClass="btn btn-primary" runat="server" OnClientClick="return PrepareCopy()" OnClick="btnCopyTimeTable_Click" ID="btnCopyTimeTable">Kopieren</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="Modal_CopyStartDate" runat="server" />
                    <asp:HiddenField ID="Modal_CopyEndDate" runat="server" />
                    <asp:HiddenField ID="Modal_WeekSpace" runat="server" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCopyTimeTable" />
                <asp:AsyncPostBackTrigger ControlID="btnEditMonday" />
                <asp:AsyncPostBackTrigger ControlID="btnEditTuesday" />
                <asp:AsyncPostBackTrigger ControlID="btnEditWednesday" />
                <asp:AsyncPostBackTrigger ControlID="btnEditThursday" />
                <asp:AsyncPostBackTrigger ControlID="btnEditFriday" />
                <asp:AsyncPostBackTrigger ControlID="chkTakesPlace" />
                <asp:AsyncPostBackTrigger ControlID="txtDayInfo" />
                <asp:AsyncPostBackTrigger ControlID="tbTimetable" />
                <asp:AsyncPostBackTrigger ControlID="lbtnOpenClassMenu" />
                <asp:AsyncPostBackTrigger ControlID="btnNextDate" />
                <asp:AsyncPostBackTrigger ControlID="btnLastDate" />
                <asp:AsyncPostBackTrigger ControlID="txtWeekBegin" />
                <asp:AsyncPostBackTrigger ControlID="btnApply" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="modal fade AbortModal" id="NewRoomModal" tabindex="-1" role="dialog" aria-labelledby="NewRoomModalTitle" aria-hidden="true">
            <div class="modal-dialog AbortModalDialog">
                <div class="modal-content">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="NewRoomModalTitle">Neuer Raum</h4>
                            </div>
                            <div class="modal-body">
                                <div class="input-group">
                                    <span class="input-group-addon">Name</span>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="RoomName"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                                <asp:Button CssClass="btn btn-primary" runat="server" Text="Übernehmen" ID="AddRoom" OnClick="AddRoom_Click" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="AddRoom" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div runat="server" class="modal" id="LoadingModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 170px !important; margin-top: 350px; margin-left: auto !important; margin-right: auto !important;">
                <div class="modal-content">
                    <div class="modal-body">
                        <!--TODO:Ladezeichen ändern(?)-->
                        <img src="Data/Images/loading.gif" alt="Lade" id="LoadingImage" />
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade AbortModal" id="RemoveRoomConfirm" tabindex="-1" role="dialog" aria-labelledby="RemoveRoomConfirmTitle" aria-hidden="true">
            <div class="modal-dialog modal-sm AbortModalDialog">
                <div class="modal-content">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title" id="RemoveRoomConfirmTitle">Bestätigen</h4>
                            </div>
                            <div class="modal-body">
                                Sind sie sicher dass sie den Raum <span id="DeleteRoomName"></span>löschen möchten? (Alle Stunden die diesen Raum verwenden erhalten den Raum "Kein Raum")
                            </div>
                            <div class="modal-footer" style="margin-top: 0px; text-align: center;">
                                <asp:Button runat="server" CssClass="btn btn-default" Text="Ja" OnClick="RemoveRoom_Click" ID="RemoveConfirmButton" OnClientClick="" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Nein</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RemoveConfirmButton" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="modal fade AbortModal" id="NewLessonModal" tabindex="-1" role="dialog" aria-labelledby="NewLessonModalTitle" aria-hidden="true">
            <div class="modal-dialog AbortModalDialog">
                <div class="modal-content">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="NewLessonModalTitle">Neues Fach</h4>
                            </div>
                            <div class="modal-body">
                                <div class="input-group">
                                    <span class="input-group-addon">Name</span>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="NormalLessonName"></asp:TextBox>
                                </div>
                                <div class="input-group">
                                    <span class="input-group-addon">Name Kurz</span>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="ShortLessonName"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                                <asp:Button CssClass="btn btn-primary" runat="server" Text="Übernehmen" ID="AddLesson" OnClick="AddLesson_Click" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="AddRoom" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="modal fade AbortModal" id="RemoveLessonNameConfirm" tabindex="-1" role="dialog" aria-labelledby="RemoveLessonNameConfirmTitle" aria-hidden="true">
            <div class="modal-dialog modal-sm AbortModalDialog">
                <div class="modal-content">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title" id="RemoveLessonNameConfirmTitle">Bestätigen</h4>
                            </div>
                            <div class="modal-body">
                                Sind sie sicher dass sie das Fach <span id="LessonNameToRemove"></span>löschen möchten?</br> <u><b>Achtung!</b> Wird dieses Fach verwendet kann es aus Sicherheitsgründen nicht gelöscht werden.</u>
                            </div>
                            <div class="modal-footer" style="margin-top: 0px; text-align: center;">
                                <asp:Button runat="server" CssClass="btn btn-default" Text="Ja" OnClick="RemoveLessonNameButton_Click" ID="RemoveLessonNameButton" OnClientClick="" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Nein</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RemoveConfirmButton" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="modal fade AbortModal" id="LessonNameNotDeleted" tabindex="-1" role="dialog" aria-labelledby="LessonNameNotDeletedTitle" aria-hidden="true">
            <div class="modal-dialog modal-sm AbortModalDialog">
                <div class="modal-content">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title" id="LessonNameNotDeletedTitle">Bestätigen</h4>
                            </div>
                            <div class="modal-body">
                                Das gewählte Fach konnte nicht gelöscht werden.
                            </div>
                            <div class="modal-footer" style="margin-top: 0px; text-align: center;">
                                <button type="button" class="btn btn-default" data-dismiss="modal">OK</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RemoveConfirmButton" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>
</body>
<script src="Bootstrap/js/bootstrap.js"></script>
<script src="Javascript/LoginScript.js"></script>
<script src="Javascript/Global.js"></script>
</html>

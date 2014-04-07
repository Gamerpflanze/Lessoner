<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Lessoner.aspx.cs" Inherits="Lessoner.Lessoner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" id="viewpoint_device" />
    <title></title>
    <!--Scripts oben wegen abruffehler-->
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Javascript/Lessoner.js"></script>
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
    <link href="CSS/print.css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" runat="server" enctype="multipart/form-data" method="post">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="MainPanel" UpdateMode="Always">
            <ContentTemplate>

                <!-- Fehleranzeige ----------------------------->
                <div class="alert alert-danger alert-dismissable" id="ErrorDisplay" style="display: none">
                    <button type="button" class="close" aria-hidden="true" onclick="CloseError()">&times;</button>
                    <strong>Fehler: </strong>
                    <label id="ErrorText"></label>
                </div>
                <!---------------------------------------------->
                <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
                    <div class="container hidden-print">
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
                                            </ul>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="page-header hidden-print">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-3" id="WeekSelectContainer" runat="server">
                                <div class="input-group left maxwidth-sm" style="float: left">
                                    <asp:LinkButton CssClass="btn btn-default LessonerButtonLeft DisabledATag input-group-addon" ID="btnLastDate" runat="server" OnClick="btnLastDate_Click" OnClientClick="jQuery('#LoadingModal').modal({backdrop:'static', keyboard:false});">
                                        <span class="glyphicon glyphicon-arrow-left"></span>
                                    </asp:LinkButton>
                                    <asp:TextBox CssClass="form-control LessonerControlTextBox maxwidth-sm" ID="txtWeekBegin" runat="server" ReadOnly="true" />
                                    <asp:LinkButton CssClass="btn btn-default LessonerButtonRight" ID="btnNextDate" runat="server" OnClick="btnNextDate_Click" OnClientClick="jQuery('#LoadingModal').modal({backdrop:'static', keyboard:false});">
                                        <span class="glyphicon glyphicon-arrow-right"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-3 hidden-sm" style="text-align: center" runat="server" id="PrintButtonContainer">
                                <button class="btn btn-primary btn-large" onclick="javascript:window.print()"><span class="glyphicon glyphicon-print"></span>   Stundenplan drucken</button>
                            </div>
                            <div class="col-md-3" runat="server" id="ClassTeacherSwitchContainer">
                                <div class="btn-group maxwidth-sm" style="float: right" id="ClassTeacherSwitch" runat="server" data-id="-1" data-name="">
                                    <a class="btn btn-default dropdown-toggle maxwidth-sm" data-toggle="dropdown" runat="server" id="ClassTeacherSwitchButton">Klasse<span class="caret"></span></a>
                                    <ul class="dropdown-menu maxwidth-sm" id="ulClassTeacherSwitch" runat="server">
                                        <li>
                                            <asp:LinkButton runat="server" ID="ClassSwitch" OnClick="ClassSwitch_Click">Klasse</asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="TeacherSwitch" OnClick="TeacherSwitch_Click">Lehrer</asp:LinkButton></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-md-3" runat="server" id="ClassTeacherSelecter">
                                <div class="btn-group maxwidth-sm" style="float: right" id="SelectClass" runat="server" data-id="-1" data-name="">
                                    <a class="btn btn-default dropdown-toggle maxwidth-sm" data-toggle="dropdown" runat="server" id="lbtnOpenClassMenu">KLASSE<span class="caret"></span></a>
                                    <ul class="dropdown-menu maxwidth-sm" id="ClassList" runat="server">
                                    </ul>
                                </div>
                                <div class="btn-group maxwidth-sm" style="float: right; display: none" id="SelectTeacher" runat="server" data-id="-1" data-name="">
                                    <a class="btn btn-default dropdown-toggle maxwidth-sm" data-toggle="dropdown" runat="server" id="TeacherMenu">LEHRER<span class="caret"></span></a>
                                    <ul class="dropdown-menu maxwidth-sm" id="TeacherList" runat="server">
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="table-responsive">
                        <asp:Table runat="server" ID="tbTimetable" CssClass="table table-bordered table-responsive" EnableViewState="false">
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
        <div class="modal fade" id="LessonInfoModal" tabindex="-1" role="dialog" aria-labelledby="LessonInfoModalTitle" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="LessonInfoPanel">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="LessonInfoModalTitle">Stundeninfos</h4>
                            </div>
                            <div class="modal-body">
                                <asp:TextBox MaxLength="512" runat="server" TextMode="MultiLine" ID="LessonInfoText" CssClass="form-control" Style="height: 200px;"></asp:TextBox>
                                <asp:Button runat="server" ID="ApplyInfoText" CssClass="btn btn-default pull-right" Text="Übernehmen" OnClick="ApplyInfoText_Click" />
                                <asp:Table runat="server" CssClass="table table-hover" ID="FileTable">
                                    <asp:TableHeaderRow TableSection="TableHeader">
                                        <asp:TableHeaderCell>Dateien</asp:TableHeaderCell>
                                        <asp:TableHeaderCell></asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                </asp:Table>
                                <div class="input-group" style="width: 100% !important;">
                                    <asp:FileUpload CssClass="pull-left btn btn-default" OnUploadedFileError="FileUploader_UploadedFileError" OnUploadedComplete="FileUploader_UploadedComplete" runat="server" ID="FileUploader" />
                                    <asp:Button runat="server" Text="Hochladen" CssClass="btn btn-default pull-right" ID="UploadButton" OnClick="UploadButton_Click" />
                                    <asp:HiddenField runat="server" ID="CurrentLesson" />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="UploadButton" />
                            <asp:AsyncPostBackTrigger ControlID="ApplyInfoText" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
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
<script src="Bootstrap/js/bootstrap.js"></script>
<script src="Javascript/LoginScript.js"></script>
<script src="Javascript/Global.js"></script>
</html>

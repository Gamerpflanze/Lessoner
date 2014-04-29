<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Default.aspx.cs" Inherits="Lessoner.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewpoint_device" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Lessoner - Hauptseite
    </title>

    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
    <script src="Javascript/Global.js"></script>

</head>
<body onload="">
    <form runat="server">
        <div class="modal fade" id="LoginModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Einloggen</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:ScriptManager runat="server"></asp:ScriptManager>
                                <asp:Panel runat="server" ID="LoginControlls">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" placeholder="Login" class="form-control" ID="txtUsername"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox TextMode="Password" placeholder="Passwort" class="form-control" ID="txtPasswort" runat="server"></asp:TextBox>
                                    </div>
                                    <asp:LinkButton class="btn btn-success" OnClick="btnLoginSubmit_Click" ID="btnLoginSubmit" OnClientClick="jQuery('#LoadingModal').modal({backdrop:false})" runat="server" >Anmelden</asp:LinkButton>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtUsername" />
                                <asp:AsyncPostBackTrigger ControlID="txtPasswort" />
                                <asp:AsyncPostBackTrigger ControlID="btnLoginSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Schließen</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="alert alert-danger alert-dismissable" id="ErrorDisplay" style="display: none">
            <button type="button" class="close" aria-hidden="true" onclick="CloseError()">&times;</button>
            <strong>Fehler: </strong>
            <label id="ErrorText"></label>
        </div>
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
                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="LoginControllsUpdatePanel">
                    <ContentTemplate>
                        <div class="collapse navbar-collapse">
                            <ul class="nav navbar-nav" runat="server">
                                <li class="active"><a href="#">Hauptseite</a></li>
                                <li id="LinkLessoner" runat="server" style="display: none;"><a href="/lessoner.aspx">Stundenplan</a></li>
                                <li id="LinkLessonerBuilder" runat="server" style="display: none;"><a href="/lessonerbuilder.aspx">Stundenplanerstellung</a></li>
                                <li id="LinkStudentManagement" runat="server" style="display: none;"><a href="/schuelerverwaltung.aspx">Schülerverwaltung</a></li>
                                <li id="LinkTeacherMamagement" runat="server" style="display: none;"><a href="/lehrerverwaltung.aspx">Lehrerverwaltung</a></li>
                            </ul>
                            <ul class="nav navbar-nav navbar-right">
                                <li>
                                    <a href="#" runat="server" data-toggle="modal" data-target="#LoginModal" id="OpenLogin">Einloggen</a>
                                </li>
                                <li class="dropdown" style="display: none" runat="server" id="PageDropDown">
                                    <a class="dropdown-toggle" data-toggle="dropdown">
                                        <span runat="server" id="User"></span><span class="caret"></span>
                                    </a>
                                    <ul class="dropdown-menu">
                                        <li><a onclick="jQuery('#ChangePasswordModal').modal('show');" style="cursor:pointer">Passwort ändern</a></li>
                                        <li>
                                            <asp:LinkButton ID="Logoutbutton" runat="server" OnClick="Logoutbutton_Click">Abmelden</asp:LinkButton></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="jumbotron">
            <div class="container">
                <h1>Herzlich Willkommen!</h1>
                <p>
                    Der “Lessoner” dient dazu einen Stundenplan zu erstellen auf den Schüler und Lehrer Zugriff haben.
             Des weiteren lassen sich Hausaufgaben in den Stundenplan eintragen die wiederum von den Schülern abgefragt werden können.
             So kann man keine Hausaufgaben mehr “vergessen”.
                </p>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <h2>Stundenplan</h2>
                    <p>
                        <br />
                        Behalte den Überblick über deine Stunden.
               Fällt morgen die letzte Stunde aus oder solltest du lieber deine Sportsachen einpacken, hier erfährst du es.
                    <table id="tbTimetable" class="table table-bordered table-responsive">
                        <thead>
                            <tr>
                                <th class="tableStunde">Zeit</th>
                                <th class="tableTag" data-takesplace="True">Montag</th>
                                <th class="tableTag" data-takesplace="True">Dienstag</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr data-infotype="0">
                                <td>
                                    <table class="HourTable">
                                        <tbody>
                                            <tr>
                                                <td class="HourNumber" rowspan="2" style="font-size: 16px; font-weight: bold;">1</td>
                                                <td class="HourCell">07:30</td>
                                            </tr>
                                            <tr>
                                                <td class="HourCell">08:15</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td class="LessonCell" data-listid="0" data-infotype="1" rowspan="2" data-toggle="modal" data-target="#Programmieren"><span style="text-align: center;" class="visible-lg">Programmieren</span><span style="text-align: center;" class="hidden-lg">PR</span></td>
                                <td class="LessonCell" data-listid="2" data-infotype="1" rowspan="2" data-toggle="modal" data-target="#Wirtschaft"><span style="text-align: center;" class="visible-lg">Wirtschaft</span><span style="text-align: center;" class="hidden-lg">W</span></td>
                            </tr>
                            <tr data-infotype="0">
                                <td>
                                    <table class="HourTable">
                                        <tbody>
                                            <tr>
                                                <td class="HourNumber" rowspan="2" style="font-size: 16px; font-weight: bold;">2</td>
                                                <td class="HourCell">08:15</td>
                                            </tr>
                                            <tr>
                                                <td class="HourCell">09:00</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td class="LessonCell" style="display: none;"></td>
                                <td class="LessonCell" style="display: none;"></td>
                            </tr>
                            <tr data-infotype="0">
                                <td>
                                    <table class="HourTable">
                                        <tbody>
                                            <tr>
                                                <td class="HourNumber" rowspan="2" style="font-size: 16px; font-weight: bold;">3</td>
                                                <td class="HourCell">09:15</td>
                                            </tr>
                                            <tr>
                                                <td class="HourCell">10:00</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td class="LessonCell" data-listid="1" data-infotype="1" rowspan="4" data-toggle="modal" data-target="#Elektroprozesstechnik"><span style="text-align: center;" class="visible-lg">Elektroprozesstechnik</span><span style="text-align: center;" class="hidden-lg">EP</span></td>
                                <td class="LessonCell" data-listid="3" data-infotype="1" rowspan="2" data-toggle="modal" data-target="#Mathe"><span style="text-align: center;" class="visible-lg">Mathe</span><span style="text-align: center;" class="hidden-lg">M</span></td>
                            </tr>
                            <tr data-infotype="0">
                                <td>
                                    <table class="HourTable">
                                        <tbody>
                                            <tr>
                                                <td class="HourNumber" rowspan="2" style="font-size: 16px; font-weight: bold;">4</td>
                                                <td class="HourCell">10:00</td>
                                            </tr>
                                            <tr>
                                                <td class="HourCell">10:45</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td class="LessonCell" style="display: none;"></td>
                                <td class="LessonCell" style="display: none;"></td>
                            </tr>
                            <tr data-infotype="0">
                                <td>
                                    <table class="HourTable">
                                        <tbody>
                                            <tr>
                                                <td class="HourNumber" rowspan="2" style="font-size: 16px; font-weight: bold;">5</td>
                                                <td class="HourCell">11:00</td>
                                            </tr>
                                            <tr>
                                                <td class="HourCell">11:45</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td class="LessonCell" style="display: none;"></td>
                                <td class="LessonCell" data-listid="4" data-infotype="1" data-toggle="modal" data-target="#Frei"><span style="text-align: center;" class="visible-lg">Frei</span><span style="text-align: center;" class="hidden-lg">frei</span></td>

                            </tr>
                            <tr data-infotype="0">
                                <td>
                                    <table class="HourTable">
                                        <tbody>
                                            <tr>
                                                <td class="HourNumber" rowspan="2" style="font-size: 16px; font-weight: bold;">6</td>
                                                <td class="HourCell">11:45</td>
                                            </tr>
                                            <tr>
                                                <td class="HourCell">12:30</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td class="LessonCell" style="display: none;"></td>
                                <td class="LessonCell" data-listid="4" data-infotype="1" data-toggle="modal" data-target="#BN"><span style="text-align: center;" class="visible-lg">Betriebssysteme/Netzwerke</span><span style="text-align: center;" class="hidden-lg">BN</span></td>
                            </tr>
                        </tbody>
                    </table>
                    </p>
                </div>
                <div class="col-md-3">
                    <h2>Hausaufgaben</h2>
                    <p>
                        Schnell noch Englisch machen und dann Mathe Seite 166, oder war es doch 168?
                    Ein Blick in die jeweilige Stunde hilft dir weiter und dein Hund muss nicht als Sündenbock enden.<br />
                        Probiere es gleich aus und klicke auf eine Stunde links in der Tabelle.
                    </p>
                </div>
                <div class="col-md-3">
                    <h2>Austausch</h2>
                    <p>
                        Mal einen Tag nicht da gewesen und dadurch wichtigen Stoff verpasst?
                    Kein Problem! Dein Lehrer kann für dich und alle anderen Schüler Dokumente zum nachlesen bereitstellen.
         
                    </p>
                </div>
            </div>

            <hr />

            <footer>
                <p>&copy; Von Florian Fürstenberg und Pascal Gönnewicht</p>
            </footer>
        </div>

        <div class="modal fade" id="Programmieren">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Programmieren</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    Lehrer:<br />
                                    Raum:<br />
                                    Hausaufgaben:
                                </div>
                                <div class="col-md-3">
                                    Herr Hoffmeister<br />
                                    505<br />
                                    Keine
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="Elektroprozesstechnik">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Elektroprozesstechnik</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    Lehrer:<br />
                                    Raum:<br />
                                    Hausaufgaben:
                                </div>
                                <div class="col-md-2">
                                    Herr Auffenberg<br />
                                    407<br />
                                    Buch Seite 116, Aufgaben 2-8<br />
                                    Aufgabe 4 mit den Widerständen R1 = 1kOhm und R2 = 10kOhm
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="Wirtschaft">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Wirtschaft</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    Lehrer:<br />
                                    Raum:<br />
                                    Hausaufgaben:
                                </div>
                                <div class="col-md-2">
                                    Herr Stracke<br />
                                    407<br />
                                    Wirtschaftsteil lesen<br />
                                    Test steht an!
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="Mathe">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Mathe</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    Lehrer:<br />
                                    Raum:<br />
                                    Hausaufgaben:
                                </div>
                                <div class="col-md-2">
                                    Herr Schneider<br />
                                    416<br />
                                    Keine
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="Frei">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Frei</h4>
                    </div>
                    <div class="modal-body">
                        Frei, auf Wunsch Eigenverantwortliches Arbeiten in Raum 407.
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="BN">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Betriebssysteme/Netzwerke</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2">
                                    Lehrer:<br />
                                    Raum:<br />
                                    Hausaufgaben:
                                </div>
                                <div class="col-md-2">
                                    Herr Sielaff<br />
                                    504<br />
                                    Wiederholung in Subnetting
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ChangePasswordModal" tabindex="-1" role="dialog" aria-labelledby="ChangePasswordModalTitle" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel runat="server" ID="ChangePassordModal">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="ChangePasswordModalTitle">Passwort ändern</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group" id="OldPass">
                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Altes Passwort" TextMode="Password" ID="OldPasswort"></asp:TextBox>
                                </div>
                                <hr />
                                <div class="form-group" id="NewPass1">
                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Neues Passwort" TextMode="Password" ID="NewPassword1"></asp:TextBox>
                                </div>
                                <div class="form-group" id="NewPass2">
                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Neues Passwort wiederholen" TextMode="Password" ID="NewPassword2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                                <asp:Button runat="server" CssClass="btn btn-default" Text="Passwort Speichern" ID="SavePassword" OnClick="SavePassword_Click" OnClientClick="jQuery('#LoadingModal').modal({backdrop:false});"/>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div runat="server" class="modal" id="LoadingModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog" style="width: 170px !important; margin-top: 350px; margin-left: auto !important; margin-right: auto !important;">
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

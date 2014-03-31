<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Default.aspx.cs" Inherits="Lessoner.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewpoint_device" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Lessoner - Hauptseite
    </title>
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
    <script src="Javascript/Global.js"></script>

</head>
<body onload="">
    <div class="modal fade" id="LoginModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel">Einloggen</h4>
                </div>
                <div class="modal-body">
                    <form role="form" id="LoginForm" runat="server">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:ScriptManager runat="server"></asp:ScriptManager>
                                <asp:Panel runat="server" ID="LoginControlls">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" placeholder="Email" class="form-control" ID="txtUsername"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox TextMode="Password" placeholder="Passwort" class="form-control" ID="txtPasswort" runat="server"></asp:TextBox>
                                    </div>
                                    <asp:Button class="btn btn-success" Text="Anmelden" OnClick="btnLoginSubmit_Click" ID="btnLoginSubmit" runat="server" />
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtUsername" />
                                <asp:AsyncPostBackTrigger ControlID="txtPasswort" />
                                <asp:AsyncPostBackTrigger ControlID="btnLoginSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </form>
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
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#">Hauptseite</a></li>
                    <li id="display" style="display: none"><a href="Lessoner.aspx">Stundenplan</a></li>
                </ul>
                <div class="navbar-form navbar-right">
                    <button class="btn btn-primary navbar-right maxwidth-xs" data-toggle="modal" data-target="#LoginModal">Einloggen</button>
                </div>
                <!--Login Anfang ------------------------------------------------------------------------------------------------------------------------------------->
                <!--Das Tool: https://chrome.google.com/webstore/detail/web-developer/bfbameneiokkgbdmiekhjnmfkcnldhhm -->
                <%--<form class="navbar-form navbar-right" role="form" id="LoginForm" runat="server">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:ScriptManager runat="server"></asp:ScriptManager>
                            <asp:Panel runat="server" ID="LoginControlls">
                                <div class="form-group">
                                    <asp:TextBox runat="server" placeholder="Email" class="form-control" ID="txtUsername"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox TextMode="Password" placeholder="Passwort" class="form-control" ID="txtPasswort" runat="server"></asp:TextBox>
                                </div>
                                <asp:Button class="btn btn-success" Text="Anmelden" OnClick="btnLoginSubmit_Click" ID="btnLoginSubmit" runat="server" />
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtUsername" />
                            <asp:AsyncPostBackTrigger ControlID="txtPasswort" />
                            <asp:AsyncPostBackTrigger ControlID="btnLoginSubmit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </form>--%>
                <!--Login Ende --------------------------------------------------------------------------------------------------------------------------------------->
            </div>
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
            <p><a class="btn btn-primary btn-lg" role="button">Mehr erfahren »</a></p>
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
                        <%--TODO: Modals für den Stunenplan einfügen--%>
                        <thead>
                            <tr>
                                <th class="tableStunde">Zeit</th>
                                <th class="tableTag" data-id="151" data-takesplace="True">Montag
                                </th>
                                <th class="tableTag" data-id="152" data-takesplace="True"></th>
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
                                <td class="LessonCell" data-listid="0" data-infotype="1" rowspan="2"><span style="text-align: center;" class="visible-lg">Programmieren</span><span style="text-align: center;" class="hidden-lg">PR</span></td>
                                <td class="LessonCell" data-listid="2" data-infotype="1" rowspan="2"><span style="text-align: center;" class="visible-lg">Wirtschaft</span><span style="text-align: center;" class="hidden-lg">W</span></td>
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
                                <td class="LessonCell" data-listid="1" data-infotype="1" rowspan="4"><span style="text-align: center;" class="visible-lg">Elektroprozesstechnik</span><span style="text-align: center;" class="hidden-lg">EP</span></td>
                                <td class="LessonCell" data-listid="3" data-infotype="1" rowspan="2"><span style="text-align: center;" class="visible-lg">Mathe</span><span style="text-align: center;" class="hidden-lg">M</span></td>
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
                                <td class="LessonCell" data-listid="4" data-infotype="1"><span style="text-align: center;" class="visible-lg">frei</span><span style="text-align: center;" class="hidden-lg">BN</span></td>

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
                                <td class="LessonCell" data-listid="4" data-infotype="1"><span style="text-align: center;" class="visible-lg">Betriebssysteme/Netzwerke</span><span style="text-align: center;" class="hidden-lg">BN</span></td>
                            </tr>
                        </tbody>
                    </table>
                </p>
            </div>
            <div class="col-md-3">
                <h2>Hausaufgaben</h2>
                <p>
                    Schnell noch Englisch machen und dann Mathe Seite 166, oder war es doch 168?
               Ein Blick in die jeweilige Stunde hilft dir weiter und dein Hund muss nicht als Sündenbock enden.
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
            <p><a href="impressum.aspx">Impressum</a></p>
        </footer>
    </div>

</body>
</html>

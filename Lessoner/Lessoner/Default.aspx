<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Default.aspx.cs" Inherits="Lessoner.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    <li><a href="about.aspx">Über den Lessoner</a></li>
                    <li id="display" style="display: none"><a href="Lessoner.aspx">Stundenplan</a></li>

                </ul>
                <!--<form class="navbar-form navbar-right" role="form" id="LoginForm">
                    <div class="form-group">
                        <input type="text" placeholder="Email" class="form-control" id="Username" />
                    </div>
                    <div class="form-group">
                        <input type="password" placeholder="Passwort" class="form-control" id="Password" />
                    </div>
                    <input type="button" class="btn btn-success" value="Anmelden" onclick="SendLoginData('Default.aspx')" />
                </form>-->
                <form class="navbar-form navbar-right" role="form" id="LoginForm" runat="server">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:ScriptManager runat="server"></asp:ScriptManager>
                            <asp:Panel runat="server" ID="LoginControlls">
                                <div class="btn-group">
                                    <button type="button" class="btn dropdown-toggle formdropdown navbar-inverse NavbarDropDown" data-toggle="dropdown">
                                        Anmelden
                                        <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu drop navbar-inverse ">
                                        <asp:Panel runat="server" ID="LoginElements">
                                            <li class="LoginElement">
                                                <asp:TextBox runat="server" placeholder="Email" CssClass="form-control" ID="txtUsername"></asp:TextBox></li>
                                            <li class="LoginElement">
                                                <asp:TextBox TextMode="Password" placeholder="Passwort" CssClass="form-control" ID="txtPasswort" runat="server"></asp:TextBox></li>
                                            <li class="LoginElement">
                                                <asp:Button CssClass="btn btn-success" Text="Anmelden" OnClick="btnLoginSubmit_Click" ID="btnLoginSubmit" runat="server" /></li>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="UserElements">
                                            <li>
                                                <asp:Button CssClass="btn btn-primary" runat="server" ID="btnLogout" Text="Abmelden" /></li>
                                        </asp:Panel>
                                    </ul>
                                </div>
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
            <div class="col-md-4">
                <h2>Stundenplan</h2>
                <p>
                    - Bild eines Stundenplanes -
                    <br />
                    Behalte den Überblick über deine Stunden.
               Fällt morgen die letzte Stunde aus oder solltest du lieber deine Sportsachen einpacken, hier erfährst du es.
                </p>
            </div>
            <div class="col-md-4">
                <h2>Hausaufgaben</h2>
                <p>
                    Schnell noch Englisch machen und dann Mathe Seite 166, oder war es doch 168?
               Ein Blick in die jeweilige Stunde hilft dir weiter und dein Hund muss nicht als Sündenbock enden.
                </p>
            </div>
            <div class="col-md-4">
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
    <script src="../../dist/js/bootstrap.min.js"></script>

</body>
</html>

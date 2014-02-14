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
</head>
<body onload="CheckLoggedin('Lessoner.aspx')">
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
                <a class="navbar-brand" href="#">Lessoner</a>
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li><a href="#">Hauptseite</a></li>
                    <li><a href="about.aspx">Über den Lessoner</a></li>
                    <li><a href="kontakt.aspx">Kontakt</a></li>
                </ul>
                <form class="navbar-form navbar-right" role="form" id="LoginForm">
                    <span class="label label-danger" id="LoginError"></span>
                    <div class="form-group">
                        <input type="text" placeholder="Email" class="form-control" id="Username" />
                    </div>
                    <div class="form-group">
                        <input type="password" placeholder="Passwort" class="form-control" id="Password" />
                    </div>
                    <input type="button" class="btn btn-success" value="Anmelden" onclick="SendLoginData('Lessoner.aspx')" />
                </form>
            </div>
        </div>
    </div>


    <div class="page-header">
        <div class="container">
            <div class="input-group">
                <button class="btn btn-default LessonerButtonLeft" id="LastDate" onclick="LastDate()" disabled="disabled"><span class="glyphicon glyphicon-arrow-left"></span></button>
                <input type="text" class="form-control LessonerControlTextBox" id="WeekBegin" disabled/>
                <button class="btn btn-default LessonerButtonRight" id="NextDate" onclick="NextDate()"><span class="glyphicon glyphicon-arrow-right"></span></button>
                <p></p>
            </div>
        </div>

        <div class="container">

            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th class="tableStunde">Stunde</th>
                        <th class="tableTag">Montag</th>
                        <th class="tableTag">Dienstag</th>
                        <th class="tableTag">Mittwoch</th>
                        <th class="tableTag">Donnerstag</th>
                        <th class="tableTag">Freitag</th>
                    </tr>
                </thead>
                <tbody id="Lessoner">

                    <tr>
                        <td class="tableCenter"></td>
                    </tr>

                </tbody>
            </table>

        </div></div>
        <script src="JQuery/jquery-1.10.2.js"></script>
        <script src="Bootstrap/js/bootstrap.js"></script>
        <script src="Javascript/LoginScript.js"></script>
        <script src="Javascript/Lessoner.js"></script>
</body>
</html>

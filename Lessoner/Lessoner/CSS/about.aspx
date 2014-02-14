<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="Lessoner.about" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>
        Über den Lessoner
    </title>
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
</head>
    <body onload="CheckLoggedin('about.aspx')">
    <div class="alert alert-danger alert-dismissable" id="ErrorDisplay" style="display:none">
        <button type="button" class="close" aria-hidden="true" onclick="CloseError()">&times;</button>
        <strong>Fehler: </strong> <label id="ErrorText"></label>
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
            <li><a href="default.aspx">Hauptseite</a></li>
            <li class="active"><a href="#">Über den Lessoner</a></li>
            <li><a href="contact.aspx">Kontakt</a></li>
          </ul>
            <form class="navbar-form navbar-right" role="form" id="LoginForm">
            <span class="label label-danger" id="LoginError"></span>
            <div class="form-group">
              <input type="text" placeholder="Email" class="form-control" id="Username"/>
            </div>
            <div class="form-group">
              <input type="password" placeholder="Passwort" class="form-control" id="Password"/>
            </div>
            <input type="button" class="btn btn-success" value="Anmelden" onclick="SendLoginData('about.aspx')"/>
          </form>
        </div>
      </div>
    </div>

      <div class="jumbotron">
      <div class="container">
        <h1>Herzlich Willkommen!</h1>
        <p>Der “Lessoner” dient dazu einen Stundenplan zu erstellen auf den Schüler und Lehrer Zugriff haben.
             Des weiteren lassen sich Hausaufgaben in den Stundenplan eintragen die wiederum von den Schülern abgefragt werden können.
             So kann man keine Hausaufgaben mehr “vergessen”.</p>
        <p><a class="btn btn-primary btn-lg" role="button">Mehr erfahren »</a></p>
      </div>
    </div>
    <script src="../../dist/js/bootstrap.min.js"></script>
    <script src="Javascript/Global.js"></script>

  </body>
</html>
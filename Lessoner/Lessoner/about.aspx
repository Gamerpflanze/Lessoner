<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="Lessoner.about" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>
        Lessoner - Hauptseite
    </title>
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
</head>
    <body>

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
            <li><a href="default.aspx">Hauptseite</a></li>
            <li class="active"><a href="#">Über den Lessoner</a></li>
            <li><a href="kontakt.aspx">Kontakt</a></li>
          </ul>
            <form class="navbar-form navbar-right" role="form">
            <div class="form-group">
              <input type="text" placeholder="Email" class="form-control">
            </div>
            <div class="form-group">
              <input type="password" placeholder="Passwort" class="form-control">
            </div>
            <button type="submit" class="btn btn-success">Anmelden</button>
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

    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="../../dist/js/bootstrap.min.js"></script>
  </body>
</html>
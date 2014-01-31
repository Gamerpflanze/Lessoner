<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/profile.aspx.cs" Inherits="Lessoner.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title id="title">
        Dein Profil
    </title>
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
</head>
    <body onload="CheckLoggedin('Default.aspx'); information()">
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
            <li class="active"><a href="#">Hauptseite</a></li>
            <li><a href="about.aspx">Über den Lessoner</a></li>
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
            <input type="button" class="btn btn-success" value="Anmelden" onclick="SendLoginData('Default.aspx')"/>
          </form>
        </div>
      </div>
    </div>

    <div class="container">
        <div class="row">
        <div class="col-md-4">
          <h2>Stundenplan</h2>
          <p> - Bild eines Stundenplanes - <br />
              Behalte den Überblick über deine Stunden.
               Fällt morgen die letzte Stunde aus oder solltest du lieber deine Sportsachen einpacken, hier erfährst du es.</p>
        </div>
        <div class="col-md-4">
          <h2>Hausaufgaben</h2>
          <p>Schnell noch Englisch machen und dann Mathe Seite 166, oder war es doch 168?
               Ein Blick in die jeweilige Stunde hilft dir weiter und dein Hund muss nicht als Sündenbock enden.</p>
       </div>
        <div class="col-md-4">
          <h2>Austausch</h2>
          <p>Mal einen Tag nicht da gewesen und dadurch wichtigen Stoff verpasst?
              Kein Problem! Dein Lehrer kann für dich und alle anderen Schüler Dokumente zum nachlesen bereitstellen.
              hallo!
        <label id="username"></label>
          </p>
        </div>
      </div>
        

    </div>

    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="../../dist/js/bootstrap.min.js"></script>
    <script src="Javascript/profile.js"></script>
  </body>
</html>

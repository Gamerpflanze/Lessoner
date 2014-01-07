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
            <li><a href="#contact">Kontakt</a></li>
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

       <div class="container">

      <div class="starter-template">
        <h1>Bootstrap starter template</h1>
        <p class="lead">Use this document as a way to quickly start any new project.<br> All you get is this text and a mostly barebones HTML document.</p>
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
          </p>
        </div>
      </div>

      <hr>

      <footer>
        <p>&copy; Von Florian Fürsenberg und Pascal Gönnewicht</p>
      </footer>
    </div>
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="../../dist/js/bootstrap.min.js"></script>
  </body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/profile.aspx.cs" Inherits="Lessoner.profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewpoint_device" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title id="title">Dein Profil
    </title>
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
    <script src="Javascript/profile.js"></script>
    <script src="Javascript/Global.js"></script>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
</head>
<body>
    <form role="form" id="LoginForm" runat="server">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:ScriptManager runat="server"></asp:ScriptManager>

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
                        </div>
                    </div>
                </div>
                <div class="page-header">
                    <div class="container">
                        <div class="col-sm-6">
                            <h1>
                                <asp:Label ID="lblProfileOf" runat="server"></asp:Label></h1>
                        </div>
                        <div class="col-sm-6">
                            <button type="button" class="btn btn-default maxwidth-xs pull-right" data-toggle="modal" data-target="#editProfile"><span class="h1 hidden-xs glyphicon glyphicon-pencil"></span><span class="visible-xs">Bearbeiten</span></button>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="pull-left">
                        <div class="hidden-xs">
                            <asp:Image runat="server" src="Data/ProfileImages/Kunst.jpg" ID="ProfilePicture" CssClass="img-thumbnail" />
                        </div>
                    </div>
                    <div class="pull-left">
                        <div class="h1 text-primary">
                            <asp:Label runat="server" ID="PersonTyle"></asp:Label>
                        </div>
                        <div class="h2">
                            <small>Klasse: </small>
                            <asp:Label runat="server" ID="Class"></asp:Label>
                        </div>
                        <div class="h2">
                            Adresse:
                            <div class="h3">
                                <ul>
                                    <li>
                                        <small>Straße: </small>
                                        <asp:Label runat="server" ID="Street"></asp:Label>
                                    </li>
                                    <li>
                                        <small>Hausnr.: </small>
                                        <asp:Label runat="server" ID="Homenumber"></asp:Label>
                                    </li>
                                    <li>
                                        <small>PLZ: </small>
                                        <asp:Label runat="server" ID="Plz"></asp:Label>
                                    </li>
                                    <li>
                                        <small>Ort: </small>
                                        <asp:Label runat="server" ID="Place"></asp:Label>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="editProfile" tabindex="-1" role="dialog" aria-labelledby="editProfileTitle" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="editProfileTitle">Bearbeiten</h4>
                            </div>
                            <div class="modal-body">
                                <button type="button" class="btn btn-primary btn-lg btn-block">Adresse</button>
                                <button type="button" class="btn btn-primary btn-lg btn-block">Profilbild</button>
                                <button type="button" class="btn btn-primary btn-lg btn-block">Passwort</button>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Schließen</button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </form>
    <script src="../../dist/js/bootstrap.min.js"></script>
</body>
</html>

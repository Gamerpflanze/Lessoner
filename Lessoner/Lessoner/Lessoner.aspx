<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Lessoner.aspx.cs" Inherits="Lessoner.Lessoner" %>

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
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li><a href="default.aspx">Hauptseite</a></li>
                    <li><a href="about.aspx">Über den Lessoner</a></li>
                    <li><a href="kontakt.aspx">Kontakt</a></li>
                    <li class="active"><a href="Lessoner.aspx">Stundenplan</a></li>
                </ul>
                <form class="navbar-form navbar-right" role="form" id="LoginForm" runat="server">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:ScriptManager runat="server"></asp:ScriptManager>
                                <asp:Panel runat="server" ID="LoginControlls">
                                <div class="form-group">
                                    <asp:TextBox runat="server" placeholder="Email" class="form-control" id="txtUsername"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                <asp:TextBox TextMode="Password" placeholder="Passwort" class="form-control" id="txtPasswort" runat="server"></asp:TextBox>
                                </div>
                                <asp:Button class="btn btn-success" Text="Anmelden" onclick="btnLoginSubmit_Click" id="btnLoginSubmit" runat="server" />
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger  ControlID="txtUsername" />
                            <asp:AsyncPostBackTrigger ControlID="txtPasswort" />
                            <asp:AsyncPostBackTrigger ControlID="btnLoginSubmit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </form>
            </div>
        </div>
    </div>


    <div class="page-header">
        <div class="container">
            <div class="input-group">
                <button class="btn btn-default LessonerButtonLeft" id="LastDate" onclick="LastDate()" disabled="disabled"><span class="glyphicon glyphicon-arrow-left"></span></button>
                <input type="text" class="form-control LessonerControlTextBox" id="WeekBegin" disabled />
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
                        <td id="R1C1" class="tableCenter"></td>
                        <td id="R1C2" class="tableCenter"></td>
                        <td id="R1C3" class="tableCenter"></td>
                        <td id="R1C4" class="tableCenter"></td>
                        <td id="R1C5" class="tableCenter"></td>
                        <td id="R1C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R2C1" class="tableCenter"></td>
                        <td id="R2C2" class="tableCenter"></td>
                        <td id="R2C3" class="tableCenter"></td>
                        <td id="R2C4" class="tableCenter"></td>
                        <td id="R2C5" class="tableCenter"></td>
                        <td id="R2C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R3C1" class="tableCenter"></td>
                        <td id="R3C2" class="tableCenter"></td>
                        <td id="R3C3" class="tableCenter"></td>
                        <td id="R3C4" class="tableCenter"></td>
                        <td id="R3C5" class="tableCenter"></td>
                        <td id="R3C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R4C1" class="tableCenter"></td>
                        <td id="R4C2" class="tableCenter"></td>
                        <td id="R4C3" class="tableCenter"></td>
                        <td id="R4C4" class="tableCenter"></td>
                        <td id="R4C5" class="tableCenter"></td>
                        <td id="R4C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R5C1" class="tableCenter"></td>
                        <td id="R5C2" class="tableCenter"></td>
                        <td id="R5C3" class="tableCenter"></td>
                        <td id="R5C4" class="tableCenter"></td>
                        <td id="R5C5" class="tableCenter"></td>
                        <td id="R5C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R6C1" class="tableCenter"></td>
                        <td id="R6C2" class="tableCenter"></td>
                        <td id="R6C3" class="tableCenter"></td>
                        <td id="R6C4" class="tableCenter"></td>
                        <td id="R6C5" class="tableCenter"></td>
                        <td id="R6C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R7C1" class="tableCenter"></td>
                        <td id="R7C2" class="tableCenter"></td>
                        <td id="R7C3" class="tableCenter"></td>
                        <td id="R7C4" class="tableCenter"></td>
                        <td id="R7C5" class="tableCenter"></td>
                        <td id="R7C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R8C1" class="tableCenter"></td>
                        <td id="R8C2" class="tableCenter"></td>
                        <td id="R8C3" class="tableCenter"></td>
                        <td id="R8C4" class="tableCenter"></td>
                        <td id="R8C5" class="tableCenter"></td>
                        <td id="R8C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R9C1" class="tableCenter"></td>
                        <td id="R9C2" class="tableCenter"></td>
                        <td id="R9C3" class="tableCenter"></td>
                        <td id="R9C4" class="tableCenter"></td>
                        <td id="R9C5" class="tableCenter"></td>
                        <td id="R9C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R10C1" class="tableCenter"></td>
                        <td id="R10C2" class="tableCenter"></td>
                        <td id="R10C3" class="tableCenter"></td>
                        <td id="R10C4" class="tableCenter"></td>
                        <td id="R10C5" class="tableCenter"></td>
                        <td id="R10C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R11C1" class="tableCenter"></td>
                        <td id="R11C2" class="tableCenter"></td>
                        <td id="R11C3" class="tableCenter"></td>
                        <td id="R11C4" class="tableCenter"></td>
                        <td id="R11C5" class="tableCenter"></td>
                        <td id="R11C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R12C1" class="tableCenter"></td>
                        <td id="R12C2" class="tableCenter"></td>
                        <td id="R12C3" class="tableCenter"></td>
                        <td id="R12C4" class="tableCenter"></td>
                        <td id="R12C5" class="tableCenter"></td>
                        <td id="R12C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R13C1" class="tableCenter"></td>
                        <td id="R13C2" class="tableCenter"></td>
                        <td id="R13C3" class="tableCenter"></td>
                        <td id="R13C4" class="tableCenter"></td>
                        <td id="R13C5" class="tableCenter"></td>
                        <td id="R1C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R14C1" class="tableCenter"></td>
                        <td id="R14C2" class="tableCenter"></td>
                        <td id="R14C3" class="tableCenter"></td>
                        <td id="R14C4" class="tableCenter"></td>
                        <td id="R14C5" class="tableCenter"></td>
                        <td id="R14C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R15C1" class="tableCenter"></td>
                        <td id="R15C2" class="tableCenter"></td>
                        <td id="R15C3" class="tableCenter"></td>
                        <td id="R15C4" class="tableCenter"></td>
                        <td id="R15C5" class="tableCenter"></td>
                        <td id="R15C6" class="tableCenter"></td>
                    </tr>
                    <tr>
                        <td id="R16C1" class="tableCenter"></td>
                        <td id="R16C2" class="tableCenter"></td>
                        <td id="R16C3" class="tableCenter"></td>
                        <td id="R16C4" class="tableCenter"></td>
                        <td id="R16C5" class="tableCenter"></td>
                        <td id="R16C6" class="tableCenter"></td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
    <script src="Javascript/LessonerBuilder.js"></script>
    <script src="JQuery/jquery-1.10.2.js"></script>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <script src="Javascript/LoginScript.js"></script>
    <script src="Javascript/Lessoner.js"></script>
</body>
</html>

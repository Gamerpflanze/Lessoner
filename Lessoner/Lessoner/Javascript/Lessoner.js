/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />
/// <reference path="Global.js" />


function addrow()
{
    //TODO: jQuery Script für die Buttons des Stundenplanes erstellen
    
    //TODO: jQuery Script für den Stundenplan des jeweiligen Datums anzeigen



    $('#Lessoner tr:last').after('<tr><td class="tableCenter">2</td></tr>');
}

function getdays() {
    jQuery.ajax({
        type: "POST",
        url: "Lessoner.aspx/GetDays",
        async: false,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        //data:"{param:1}",
        success: function (data) { setdays(data) },
        error: function (data) {
            DisplayErrorCode(2004)
        }
    });
}
//#0 FindetStatt
//#1 Name
//#2 Titel
//#3 Vorname
//#4 Nachname
//#5 Stunde_Beginn
//#6 Stunde_Ende
//#7 Stunde
//#8 Uhrzeit
//#9 TagName
function setdays(data) {
//(data.d[1][1][7])

}

function Enter(sender)
{

}
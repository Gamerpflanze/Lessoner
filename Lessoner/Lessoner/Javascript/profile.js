/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />

function leher() {

    //TODO: Abfrage ob User Lehrer oder Schüler ist

}

function getdata() {
    $.ajax({
        type: "POST",
        url: "profile.aspx/getprofiledata",
        async: true,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (data) { AddData(data) },
        error: function (data) {
            DisplayErrorCode(2004)
        }
    });
}   //TODO: getprofiledata auf dem Server erstellen zum Daten abfragen Pascal macht das :D
//Data\ProfileImages

function information() {
    $.ajax({
        type: "POST",
        url: "profile.aspx/informations",
        async: false,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (data) {
            jQuery("#username").text(data.d[0] + " " + data.d[1] + " " + data.d[2]);
        },
        error: function (message) {
            LoginError('2003');
        }
    });
}
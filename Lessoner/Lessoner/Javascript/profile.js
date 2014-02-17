/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />

function leher() {

    //TODO: Abfrage ob User Lehrer oder Schüler ist

}
//url: "profile.aspx/GetProfileData",
function getdata() {
    jQuery.ajax({
        type: "POST",
        url: "profile.aspx/GetData",
        async: false,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        //data:"{param:1}",
        success: function (data) {setdata(data)},
        error:function(data){
            DisplayErrorCode(2004)}
    });
}

function setdata(data) {
    jQuery("#vnname").text(data.d[1] + " " + data.d[2]);
    jQuery("#strasseNr").text(data.d[3] + " " + data.d[4]);
    jQuery("#PLZOrt").text(data.d[5] + " " + data.d[6]);
    jQuery("#Klasse").text(data.d[7]);
}
//TODO: getprofiledata auf dem Server erstellen zum Daten abfragen Pascal macht das :D !GetStudentInfos!
//Data\ProfileImages
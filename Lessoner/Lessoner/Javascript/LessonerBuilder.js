/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" 
/// <reference path="Global.js" />
var Dates;
var CurrentIndex = 0;
function GetData() {
    $.ajax({
        type: "POST",
        url: "LessonerBuilder.aspx/GetData",
        async: true,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (data) { AddData(data) },
        error: function (data) {
            DisplayErrorCode(2004)
        }
    });
}

function AddData(data) {
    if (data.d == "NoAccessAllowed") {
        DisplayError("Sie haben keinen erlaubten Zugriff auf diese Seite (3001)")
    }
    else if (data.d == "NotLoggedIn") {
        DisplayError("Sie sind nicht Angemeldet (0001)")
    }
    else {
        Dates = data.d[0];
        jQuery("#WeekBegin").val(data.d[0][0]);
        var TBody = jQuery("#Lessoner");
        for (var i = 0; i < data.d[1].length; i++) {
            var row = jQuery("<tr></tr>");
            var time = jQuery("<td></td>");
            var Days = new Array();
            //TODO: Taginformationen holen
            for (var j = 0; j < data.d[1]; j++) {
                Days.push(jQuery("<td></td>"));
            }

            // column.text(data.d[1][i][0] + "<br>" + data.d[1][i][0] + "<br>" + data.d[1][i][0]);
            var Number = jQuery("<p></p>");
            var Begin = jQuery("<p></p>");
            var End = jQuery("<p></p>");

            Number.text(data.d[1][i][0]);
            Number.addClass("LessonerText");

            Begin.text(data.d[1][i][1]);
            Begin.addClass("LessonerText");

            End.text(data.d[1][i][2]);
            End.addClass("LessonerText");

            time.append(Number);
            time.append(Begin);
            time.append(End);

            row.append(time);
            TBody.append(row);
        }
    }
}

function NextDate() {
    jQuery("#LastDate").removeAttr("disabled");
    CurrentIndex++;
    jQuery("#WeekBegin").val(Dates[CurrentIndex]);
    if (CurrentIndex == 5) {
        jQuery("#NextDate").attr("disabled", "disabled");
    }
}

function LastDate() {
    jQuery("#NextDate").removeAttr("disabled");
    CurrentIndex--;
    jQuery("#WeekBegin").val(Dates[CurrentIndex]);
    if (CurrentIndex == 0) {
        jQuery("#LastDate").attr("disabled", "disabled");
    }
}

var WantedModalClose = false;
function KeepEditModalOpen() {
    var Modal = jQuery("#LessonEdit");
    Modal.removeClass("fade");
    Modal.addClass("in");
    Modal.modal("show");
    $('#LessonEdit').on('hide.bs.modal', function (e) {
        $('#LessonEdit').addClass('fade');
        if (WantedModalClose) {
            WantedModalClose = false
            return true;
        }
        else {
            $('#AskAbort').modal({ backdrop: false });
            return false;
        }
    });
    $('#AskAbort').on('hidden.bs.modal', function (e) {
        return false;
    });
    jQuery(".modal-backdrop:first").remove();
    jQuery(".modal-backdrop:last").addClass("fade");
}
function KeepAbortModalOpen() {
    var Modal = jQuery("#AskAbort");
    Modal.removeClass("fade");
    Modal.addClass("in");
    Modal.modal("show");
    $('#AskAbort').on('hide.bs.modal', function (e) {
        jQuery("#AskAbort").addClass("fade");
    });
    jQuery(".modal-backdrop:first").remove();
    jQuery(".modal-backdrop:last").addClass("fade");
}

function OpenLessonEditModal() {
    $('#LessonEdit').modal({
        backdrop: true,
        keyboard: false,
        show: true,
        remote: false
    });
    $('#LessonEdit').on('hide.bs.modal', function (e) {
        $('#LessonEdit').addClass('fade');
    });
    $('#AskAbort').on('hidden.bs.modal', function (e) {
        return false;
    });
}
function HideLessonEditModal() {
    jQuery("#AskAbort").modal("hide");
    setTimeout(function () {
        WantedModalClose = true;
        jQuery("#LessonEdit").modal("hide");
    }, 250)
}
function HideLessonEditModalNoAbort() {
    WantedModalClose = true;
    jQuery("#LessonEdit").modal("hide");
}
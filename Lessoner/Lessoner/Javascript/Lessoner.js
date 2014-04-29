/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />
/// <reference path="Global.js" />

var CanDelete = true;
function addrow() {
    $('#Lessoner tr:last').after('<tr><td class="tableCenter">2</td></tr>');
}
function CantDeleteFiles()
{
    CanDelete = false;
}
function getdays() {
    jQuery.ajax({
        type: "POST",
        url: "Lessoner.aspx/GetDays",
        async: false,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (data) { setdays(data) },
        error: function (data) {
            DisplayErrorCode(2004)
        }
    });
}

function LoadLessonInfoModal(ID, Open) {
    jQuery("#CurrentLesson").val(ID);
    jQuery('#LoadingModal').modal({ backdrop: 'static', keyboard: false });
    $.ajax({
        async: true,
        type: "POST",
        url: "Lessoner.aspx/LoadLessonInfoModal",
        data: JSON.stringify({ "ID": parseInt(ID) }),
        dataType: "JSON",
        contentType: "application/json; charset=utf-8;",
        success: function (data) {
            jQuery("#LoadingModal").modal("hide");
            jQuery("#LessonInfoText").val(data.d[0]);
            jQuery("#FileTable").children("tbody").remove();
            for (var i = 0; i < data.d[1][0].length; i++) {
                var Row = jQuery("<tr></tr>");
                var FileName = jQuery("<td></td>");
                var ButtonCell = jQuery('<td width="96px"></td>');
                var DownloadButton = jQuery('<button type="button" class="btn btn-primary"><span class="glyphicon glyphicon-download"></span></button>');
                if (CanDelete) {
                    var DeleteButton = jQuery('<button type="button" class="btn btn-danger" onclick="DeleteFile(' + "'" + data.d[1][0][i] + "'" + ',' + ID + ')"><span class="glyphicon glyphicon-remove"></span></button>');
                }
                FileName.html(data.d[1][1][i]);

                Row.append(FileName);
                if (CanDelete) {
                    ButtonCell.append(DeleteButton);
                }
                ButtonCell.append(DownloadButton);
                DownloadButton.attr("onclick", "window.location.href='/Data/FileDownload.aspx?File=" + data.d[1][0][i] + "'");
                Row.append(ButtonCell);

                jQuery("#FileTable").append(Row);

            }
            if (Open) {
                jQuery("#LessonInfoModal").modal("show");
            }
        },
        error: function (message) {
            jQuery("#LoadingModal").modal("hide");
            alert("error");
            alert(message);
        }
    });
}
function DeleteFile(FileName, ID) {
    $.ajax({
        async: true,
        type: "POST",
        url: "Lessoner.aspx/DeleteFile",
        data: JSON.stringify({ "FileName": FileName }),
        dataType: "JSON",
        contentType: "application/json; charset=utf-8;",
        success: function (data) {
            LoadLessonInfoModal(ID, false);
        },
        error: function () { alert("error");}
    });
}

function Enter(sender) {

}
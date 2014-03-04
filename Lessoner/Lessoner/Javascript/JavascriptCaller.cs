using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lessoner
{
    public static class JavascriptCaller
    {
        public const string KeepEditModal = @"KeepEditModalOpen();";
        public const string HideEditModal = @"HideLessonEditModal();";
        public const string OpenLessonEditModal = @"OpenLessonEditModal();";
        public const string HideEditModalNoAbort = @"HideLessonEditModalNoAbort();";
        public const string KeepAbortModalOpen = @"KeepAbortModalOpen();";
        public const string OpenDeleteConfirmModal = @"jQuery('#DeleteConfirm').modal('show');";
        public const string CloseDeleteConfirmModal = @"var Modal = jQuery('#DeleteConfirm');
                                                        Modal.removeClass('fade');
                                                        Modal.addClass('in');
                                                        Modal.modal('show');
                                                        $('#DeleteConfirm').on('hide.bs.modal', function (e) {
                                                            jQuery('#DeleteConfirm').addClass('fade');
                                                        });
                                                        jQuery('.modal-backdrop:first').remove();
                                                        jQuery('.modal-backdrop:last').addClass('fade');
                                                        jQuery('#DeleteConfirm').modal('hide');";

        public const string OpenEditDayModal = @"OpenDayEditModal();";
        public const string KeepEditDayModal = @"KeepDayEditModalOpen();";
        public const string HideEditDayModal = @"HideDayEditModal();";
        public const string HideEditDayModalWithAbort = @"HideDayEditModdalWithAbort();";
        public const string CloseLoadingIndicator = @"CloseLoadingIndicator();";
        public const string ClearLoadingIndicator = @"ClearLoadingIndicator();";
    }
}
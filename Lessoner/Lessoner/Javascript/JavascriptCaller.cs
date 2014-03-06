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
        public const string CloseDeleteConfirmModal = @"CloseDeleteConfirmModal();";

        public const string OpenEditDayModal = @"OpenDayEditModal();";
        public const string KeepEditDayModal = @"KeepDayEditModalOpen();";
        public const string HideEditDayModal = @"HideDayEditModal();";
        public const string HideEditDayModalWithAbort = @"HideDayEditModdalWithAbort();";
        public const string CloseLoadingIndicator = @"CloseLoadingIndicator();";
        public const string ClearLoadingIndicator = @"ClearLoadingIndicator();";
    }
}
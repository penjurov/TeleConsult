var RequestConsultationViewModel = {
    form: '#requestForm',
    validator: null,

    init: function () {
        var self = this;

        self.initEvents();
        self.initValidation();
    },

    initEvents: function () {
        var self = RequestConsultationViewModel;

        $('#btnSave').on('click', self.save);
        $('#ViewModel_PreliminaryDiagnosisCode').on('change', self.getDiagnosis);
    },

    initValidation: function () {
        var self = RequestConsultationViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    getDiagnosis: function() {
        var params = {},
            url,
            onSuccess;

        params = {
            code: $('#ViewModel_PreliminaryDiagnosisCode').val(),
        };

        url = $('#GetDiagnosisUrl').val();

        onSuccess = function (diagnosis) {
            $('#ViewModel_PreliminaryDiagnosisDescription').val(diagnosis);
        }

        ajaxModel.get(url, params, onSuccess)
    },

    save: function () {
        var self = RequestConsultationViewModel,
            params = {},
            url,
            onSuccess;

        if ($(self.form).valid()) {
            params = {
                PatientInitials: $('#ViewModel_PatientInitials').val(),
                PatientAge: $('#ViewModel_PatientAge').val(),
                Gender: $('#ViewModel_Gender').val(),
                PreliminaryDiagnosisCode: $('#ViewModel_PreliminaryDiagnosisCode').val(),
                Anamnesis: $('#ViewModel_Anamnesis').val(),
                SpecialityId: $('#ViewModel_SpecialityId').val(),
                Type: $('#ViewModel_Type').val(),
            };

            url = $(this).data('url');

            onSuccess = function () {
                window.location.href = $('#HomePageUrl').val();
            }

            ajaxModel.post(url, params, onSuccess)
        }
    }
};

$(document).ready(function () {
    RequestConsultationViewModel.init();
});
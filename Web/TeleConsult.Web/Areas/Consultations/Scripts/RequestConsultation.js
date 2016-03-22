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

        $('.date').datetimepicker({
            format: 'DD/MM/YYYY h:mm A'
        });
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
            onSuccess,
            verificationToken;

        if ($(self.form).valid()) {
            params = {
                PatientInitials: $('#ViewModel_PatientInitials').val(),
                PatientAge: $('#ViewModel_PatientAge').val(),
                Gender: $('#ViewModel_Gender').val(),
                PreliminaryDiagnosisCode: $('#ViewModel_PreliminaryDiagnosisCode').val(),
                PreliminaryDiagnosisDescription: $('#ViewModel_PreliminaryDiagnosisDescription').val(),
                Anamnesis: $('#ViewModel_Anamnesis').val(),
                SpecialityId: $('#ViewModel_SpecialityId').val(),
                Type: $('#ViewModel_Type').val(),
                BloodExaminations: self.getBloodExaminations(),
                Urinalysis: self.getUrinalysis(),
                VisualExaminations: self.getVisualExaminations()
            };

            url = $(this).data('url');

            onSuccess = function () {
                window.location.href = $('#HomePageUrl').val();
            }

            verificationToken = $(self.form).find('[name="__RequestVerificationToken"]').val();

            ajaxModel.post(url, params, onSuccess, null, verificationToken)
        }
    },

    getBloodExaminations: function () {
        var result = [];

        result = _.map(BloodExaminationViewModel.grid.getAll(), function (item) {
            return item.record;
        });

        return result;
    },

    getUrinalysis: function () {
        var result = [];

        result = _.map(UrinalysisViewModel.grid.getAll(), function (item) {
            return item.record;
        });

        return result;
    },

    getVisualExaminations: function () {
        var result = [];

        result = _.map(VisualExaminationViewModel.grid.getAll(), function (item) {
            var imageData = JSON.parse(localStorage.getItem('examination' + item.record.ID));

            item.record.FileType = imageData.FileType;
            item.record.FileContent = imageData.FileContent;

            return item.record;
        });

        return result;
    }
};

$(document).ready(function () {
    RequestConsultationViewModel.init();
});
var ConsultationViewModel = {
    form: '#consultationForm',
    validator: null,

    init: function () {
        var self = this;

        self.initEvents();
        self.initValidation();
    },

    initEvents: function () {
        var self = ConsultationViewModel;

        $('#btnSave').on('click', self.save);

        $('#ViewModel_PreliminaryDiagnosisCode').on('change', function () {
            self.getDiagnosis($(this), $('#ViewModel_PreliminaryDiagnosisDescription'));
        });

        $('#ViewModel_FinalDiagnosisCode').on('change', function () {
            self.getDiagnosis($(this), $('#ViewModel_FinalDiagnosisDescription'));
        });

        $('.date').datetimepicker({
            format: 'DD/MM/YYYY h:mm A'
        });

        $('#ViewModel_ConfirmationCode').val('');
    },

    initValidation: function () {
        var self = ConsultationViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    getDiagnosis: function(source, destination) {
        var params = {},
            url,
            onSuccess;

        params = {
            code: source.val(),
        };

        url = $('#GetDiagnosisUrl').val();

        onSuccess = function (diagnosis) {
            destination.val(diagnosis);
        }

        ajaxModel.get(url, params, onSuccess)
    },

    save: function () {
        var self = ConsultationViewModel,
            params = {},
            url,
            onSuccess,
            verificationToken;

        if ($(self.form).valid()) {
            params = {
                Id: $('#ViewModel_Id').val(),
                PatientInitials: $('#ViewModel_PatientInitials').val(),
                PatientAge: $('#ViewModel_PatientAge').val(),
                PatientGender: $('#ViewModel_PatientGender').val(),
                PreliminaryDiagnosisCode: $('#ViewModel_PreliminaryDiagnosisCode').val(),
                PreliminaryDiagnosisDescription: $('#ViewModel_PreliminaryDiagnosisDescription').val(),
                Anamnesis: $('#ViewModel_Anamnesis').val(),
                SpecialityId: $('#ViewModel_SpecialityId').val(),
                ConsultationType: $('#ViewModel_ConsultationType').val(),
                BloodExaminations: self.getBloodExaminations(),
                Urinalysis: self.getUrinalysis(),
                VisualExaminations: self.getVisualExaminations(),
                IsConsultation: $('#ViewModel_PatientInitials').length === 0,
                Conclusion: $('#ViewModel_Conclusion').val(),
                FinalDiagnosisCode: $('#ViewModel_FinalDiagnosisCode').val(),
                FinalDiagnosisDescription: $('#ViewModel_FinalDiagnosisDescription').val(),
                ConfirmationCode: $('#ViewModel_ConfirmationCode').val() || '34e2f125-894c-4cba-a0d3-e714992d5858' // Sending not valid Confirmation code to skip back-end error validation on edit, or when logged user is Specialist
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
            return item.record;
        });

        return result;
    }
};

$(document).ready(function () {
    ConsultationViewModel.init();
});
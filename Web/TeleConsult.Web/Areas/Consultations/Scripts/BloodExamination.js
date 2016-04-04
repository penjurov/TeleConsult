var BloodExaminationViewModel = {
    grid: null,
    gridId: '#bloodExaminationsGrid',
    dialog: '#bloodExaminationDialog',
    form: '#bloodExaminationForm',
    modal: null,
    validator: null,

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();
        self.initValidation();
    },

    initGrid: function () {
        var self = BloodExaminationViewModel,
            consultationId,
            columns,
            isConsultation;

        columns = [
                { title: 'Хемоглобин', field: 'Hemoglobin', align: 'center' },
                { title: 'Еритроцити', field: 'Erythrocytes', align: 'center' },
                { title: 'Hct', field: 'Hct', align: 'center' },
                { title: 'Leuc', field: 'Leuc', align: 'center' },
                { title: 'Mchc', field: 'Mchc', align: 'center' },
                { title: 'Mch', field: 'Mch', align: 'center' },
                { title: 'Mcv', field: 'Mcv', align: 'center' },
                { title: 'Ret', field: 'Ret', align: 'center' },
                { title: 'Sue', field: 'Sue', align: 'center' },
                { title: 'Време кървете', field: 'BleedingTime', align: 'center' },
                { title: 'Време съсирване', field: 'CoagulationTime', align: 'center' },
                { title: 'Морфология еритроцити', field: 'MorphologyErythrocytes', align: 'center' },
                { title: 'Кръвна захар', field: 'BloodSugar', align: 'center' },
                { title: 'Дата', field: 'Date', align: 'center' }
        ];

        isConsultation = $('#IsConsultation').val() === 'True';

        if (!isConsultation) {
            columns.push({ title: '', field: 'Edit', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Редакция', events: { 'click': self.edit } });
            columns.push({ title: '', field: 'Delete', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-remove', tooltip: 'Изтриване', events: { 'click': self.remove } });
        } else {
            columns.push({ title: '', field: 'View', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Разглеждане', events: { 'click': self.edit } });
        }

        self.grid = $(self.gridId).grid({
            primaryKey: 'ID',
            columns: columns,
            pager: { enable: true, limit: 10, sizes: [10, 20, 50, 100] },
            autoLoad: false,
            uiLibrary: 'bootstrap',
            notFoundText: 'Няма добавени записи'
        });

        consultationId = $('#ViewModel_Id').val();

        if (consultationId) {
            self.grid.reload({ consultationId: consultationId });
        }
    },

    initEvents: function () {
        var self = BloodExaminationViewModel;

        $('#addBloodExamination').on('click', self.add);
        $('#btnSaveBloodExamination').on('click', self.save);
        $('#btnCancelBloodExamination').on('click', function () {
            self.modal.modal('hide');
        });
    },

    initValidation: function () {
        var self = BloodExaminationViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    add: function() {
        BloodExaminationViewModel.showDialog('Ново кръвно изследване');
    },

    edit: function (e) {
        BloodExaminationViewModel.showDialog('Редакция кръвно изследване', e);
    },

    remove: function (e) {
        confirm('Сигурни ли сте че искате да изтриете записа?', function () {
            BloodExaminationViewModel.grid.removeRow(e.data.id);
        });
    },

    showDialog: function (title, record) {
        var self = BloodExaminationViewModel;

        self.populateFields(record);

        self.modal = $(self.dialog).modal();
        self.modal.find('.modal-title').text(title);
    },

    populateFields: function (e) {
        var self = BloodExaminationViewModel,
            record;

        if (e) {
            record = e.data.record;

            $('#bloodExaminationCounter').val(e.data.id);
            $('#BloodExaminationViewModel_Id').val(record.Id);
            $('#BloodExaminationViewModel_Hemoglobin').val(record.Hemoglobin);
            $('#BloodExaminationViewModel_Erythrocytes').val(record.Erythrocytes);
            $('#BloodExaminationViewModel_Hct').val(record.Hct);
            $('#BloodExaminationViewModel_Leuc').val(record.Leuc);
            $('#BloodExaminationViewModel_Mchc').val(record.Mchc);
            $('#BloodExaminationViewModel_Mch').val(record.Mch);
            $('#BloodExaminationViewModel_Mcv').val(record.Mcv);
            $('#BloodExaminationViewModel_Ret').val(record.Ret);
            $('#BloodExaminationViewModel_Sue').val(record.Sue);
            $('#BloodExaminationViewModel_BleedingTime').val(record.BleedingTime);
            $('#BloodExaminationViewModel_CoagulationTime').val(record.CoagulationTime);
            $('#BloodExaminationViewModel_MorphologyErythrocytes').val(record.MorphologyErythrocytes);
            $('#BloodExaminationViewModel_BloodSugar').val(record.BloodSugar);
            $('#BloodExaminationViewModel_Date').val(record.Date);

        } else {
            $('#BloodExaminationViewModel_Id').val('');
            $('#bloodExaminationCounter').val('');
            $(self.dialog).find(':text').val('');
        }
    },

    save: function () {
        var self = BloodExaminationViewModel,
            data,
            count;

        if ($(self.form).valid()) {
            data = {
                Id: $('#BloodExaminationViewModel_Id').val(),
                Hemoglobin: $('#BloodExaminationViewModel_Hemoglobin').val(),
                Erythrocytes: $('#BloodExaminationViewModel_Erythrocytes').val(),
                Hct: $('#BloodExaminationViewModel_Hct').val(),
                Leuc: $('#BloodExaminationViewModel_Leuc').val(),
                Mchc: $('#BloodExaminationViewModel_Mchc').val(),
                Mch: $('#BloodExaminationViewModel_Mch').val(),
                Mcv: $('#BloodExaminationViewModel_Mcv').val(),
                Ret: $('#BloodExaminationViewModel_Ret').val(),
                Sue: $('#BloodExaminationViewModel_Sue').val(),
                BleedingTime: $('#BloodExaminationViewModel_BleedingTime').val(),
                CoagulationTime: $('#BloodExaminationViewModel_CoagulationTime').val(),
                MorphologyErythrocytes: $('#BloodExaminationViewModel_MorphologyErythrocytes').val(),
                BloodSugar: $('#BloodExaminationViewModel_BloodSugar').val(),
                Date: $('#BloodExaminationViewModel_Date').val(),
            };

            if ($('#bloodExaminationCounter').val()) {
                count = parseInt($('#bloodExaminationCounter').val());
                self.grid.updateRow(count, data);
            } else {
                self.grid.addRow(data);
            }

            self.modal.modal('hide');
        }
    }
}

$(document).ready(function () {
    BloodExaminationViewModel.init();
});
var UrinalysisViewModel = {
    grid: null,
    gridId: '#urinalysisGrid',
    dialog: '#urinalysisDialog',
    form: '#urinalysisForm',
    modal: null,
    validator: null,

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();
        self.initValidation();
    },

    initGrid: function () {
        var self = UrinalysisViewModel,
            consultationId,
            columns,
            isConsultation;

        columns = [
                { title: 'Специфично тегло', field: 'SpecificGravity', width: 105, align: 'center' },
                { title: 'Ph', field: 'Ph', align: 'center' },
                { title: 'Белтък', field: 'Protein', align: 'center' },
                { title: 'Белтък - количество', field: 'ProteinWeight', width: 100, align: 'center' },
                { title: 'Захар', field: 'Glucose', align: 'center' },
                { title: 'Захар - количество', field: 'GlucoseWeight', width: 100, align: 'center' },
                { title: 'Кетонни тела', field: 'KetoneBodies', align: 'center' },
                { title: 'Билирубин', field: 'Bilirubin', width: 105, align: 'center' },
                { title: 'Уробилиноген', field: 'Urobilinogen', width: 130, align: 'center' },
                { title: 'Кръв', field: 'Blood', align: 'center' },
                { title: 'Порфобилиноген', field: 'Porphobilinogen', width: 135, align: 'center' },
                { title: 'Амилаза', field: 'Amylase', align: 'center' },
                { title: '17 - кетостероиди', field: 'Ketosteroids', width: 115, align: 'center' },
                { title: 'Диуреза (24 ч.)', field: 'Diuresis', width: 80, align: 'center' },
                { title: 'Седимент', field: 'Sediments', align: 'center' },
                { title: 'Формени елементи', field: 'FormedElements', align: 'center' },
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
        var self = UrinalysisViewModel;

        $('#addUrinalysis').on('click', self.add);
        $('#btnSaveUrinalysis').on('click', self.save);
        $('#btnCancelUrinalysis').on('click', function () {
            self.modal.modal('hide');
        });
    },

    initValidation: function () {
        var self = UrinalysisViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    add: function () {
        UrinalysisViewModel.showDialog('Ново изследване на урина');
    },

    edit: function (e) {
        UrinalysisViewModel.showDialog('Редакция изследване на урина', e);
    },

    remove: function (e) {
        confirm('Сигурни ли сте че искате да изтриете записа?', function () {
            UrinalysisViewModel.grid.removeRow(e.data.id);
        });
    },

    showDialog: function (title, record) {
        var self = UrinalysisViewModel;

        self.populateFields(record);

        self.modal = $(self.dialog).modal();
        self.modal.find('.modal-title').text(title);
    },

    populateFields: function (e) {
        var self = UrinalysisViewModel,
            record;

        if (e) {
            record = e.data.record;

            $('#urinalysisCounter').val(e.data.id);
            $('#UrinalysisViewModel_Id').val(record.Id);
            $('#UrinalysisViewModel_SpecificGravity').val(record.SpecificGravity);
            $('#UrinalysisViewModel_Ph').val(record.Ph);
            $('#UrinalysisViewModel_Protein').val(record.Protein);
            $('#UrinalysisViewModel_ProteinWeight').val(record.ProteinWeight);
            $('#UrinalysisViewModel_Glucose').val(record.Glucose);
            $('#UrinalysisViewModel_GlucoseWeight').val(record.GlucoseWeight);
            $('#UrinalysisViewModel_KetoneBodies').val(record.KetoneBodies);
            $('#UrinalysisViewModel_Bilirubin').val(record.Bilirubin);
            $('#UrinalysisViewModel_Urobilinogen').val(record.Urobilinogen);
            $('#UrinalysisViewModel_Blood').val(record.Blood);
            $('#UrinalysisViewModel_Porphobilinogen').val(record.Porphobilinogen);
            $('#UrinalysisViewModel_Amylase').val(record.Amylase);
            $('#UrinalysisViewModel_Ketosteroids').val(record.Ketosteroids);
            $('#UrinalysisViewModel_Diuresis').val(record.Diuresis);
            $('#UrinalysisViewModel_Sediments').val(record.Sediments);
            $('#UrinalysisViewModel_FormedElements').val(record.FormedElements);
            $('#UrinalysisViewModel_Date').val(record.Date);
        } else {
            $('#UrinalysisViewModel_Id').val('');
            $('#urinalysisCounter').val('');
            $(self.dialog).find(':text').val('');
        }
    },

    save: function () {
        var self = UrinalysisViewModel,
            data,
            count;

        if ($(self.form).valid()) {
            data = {
                Id: $('#UrinalysisViewModel_Id').val(),
                SpecificGravity: $('#UrinalysisViewModel_SpecificGravity').val(),
                Ph: $('#UrinalysisViewModel_Ph').val(),
                Protein: $('#UrinalysisViewModel_Protein').val(),
                ProteinWeight: $('#UrinalysisViewModel_ProteinWeight').val(),
                Glucose: $('#UrinalysisViewModel_Glucose').val(),
                GlucoseWeight: $('#UrinalysisViewModel_GlucoseWeight').val(),
                KetoneBodies: $('#UrinalysisViewModel_KetoneBodies').val(),
                Bilirubin: $('#UrinalysisViewModel_Bilirubin').val(),
                Urobilinogen: $('#UrinalysisViewModel_Urobilinogen').val(),
                Blood: $('#UrinalysisViewModel_Blood').val(),
                Porphobilinogen: $('#UrinalysisViewModel_Porphobilinogen').val(),
                Amylase: $('#UrinalysisViewModel_Amylase').val(),
                Ketosteroids: $('#UrinalysisViewModel_Ketosteroids').val(),
                Diuresis: $('#UrinalysisViewModel_Diuresis').val(),
                Sediments: $('#UrinalysisViewModel_Sediments').val(),
                FormedElements: $('#UrinalysisViewModel_FormedElements').val(),
                Date: $('#UrinalysisViewModel_Date').val()
            };

            if ($('#urinalysisCounter').val()) {
                count = parseInt($('#urinalysisCounter').val());
                self.grid.updateRow(count, data);
            } else {
                self.grid.addRow(data);
            }

            self.modal.modal('hide');
        }
    }
}

$(document).ready(function () {
    UrinalysisViewModel.init();
});
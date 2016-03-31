var VisualExaminationViewModel = {
    grid: null,
    gridId: '#visualExaminationsGrid',
    dialog: '#visualExaminationDialog',
    form: '#visualExaminationForm',
    modal: null,
    validator: null,
    image: '#examination',

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();
        self.initValidation();

        localStorage.clear();
    },

    initGrid: function () {
        var self = VisualExaminationViewModel,
            consultationId;

        self.grid = $(self.gridId).grid({
            primaryKey: 'ID',
            columns: [
                { title: 'Вид', field: 'TypeName' },
                { title: 'Дата', field: 'Date' },
                { title: '', field: 'Edit', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Редакция', events: { 'click': self.edit } },
                { title: '', field: 'Delete', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-remove', tooltip: 'Изтриване', events: { 'click': self.remove } }
            ],
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
        var self = VisualExaminationViewModel;

        $(self.form).find('[type=file]').on('change', self.setImage);

        $('#addVisualExamination').on('click', self.add);
        $('#btnSaveVisualExamination').on('click', self.save);
        $('#btnCancelVisualExamination').on('click', function () {
            self.modal.modal('hide');
        });
    },

    initValidation: function () {
        var self = VisualExaminationViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    add: function () {
        VisualExaminationViewModel.showDialog('Ново образно изследване');
    },

    edit: function (e) {
        VisualExaminationViewModel.showDialog('Редакция образно изследване', e);
    },

    remove: function (e) {
        confirm('Сигурни ли сте че искате да изтриете записа?', function () {
            VisualExaminationViewModel.grid.removeRow(e.data.id);
        });
    },

    showDialog: function (title, record) {
        var self = VisualExaminationViewModel;

        self.populateFields(record);

        self.modal = $(self.dialog).modal();
        self.modal.find('.modal-title').text(title);
    },

    populateFields: function (e) {
        var self = VisualExaminationViewModel,
            record;

        if (e) {
            record = e.data.record;

            $('#visualExaminationCounter').val(e.data.id);
            $('#VisualExaminationViewModel_Id').val(record.Id);
            $('#VisualExaminationViewModel_Type').val(record.Type);
            $('#VisualExaminationViewModel_InputInformation').val(record.InputInformation);
            $('#VisualExaminationViewModel_ConsultInformation').val(record.ConsultInformation);
            $('#VisualExaminationViewModel_Date').val(record.Date);

            $(self.image).attr('src', record.FileContent);
            $(self.image).show();
            self.imageType = record.FileType;
        } else {
            $('#VisualExaminationViewModel_Id').val('');
            $('#visualExaminationCounter').val('');
            $(self.dialog).find('textarea').val('');
            $(self.dialog).find(':text').val('');
            self.clearImage();
            $(self.image).hide();
        }
    },

    setImage: function () {
        var self = VisualExaminationViewModel,
            reader;

        if (this.files && this.files[0]) {
            reader = new FileReader();

            self.imageType = this.files[0].name.split('.')[1];

            reader.onload = function (e) {
                $(self.image).attr('src', e.target.result);
                $(self.image).show();
            }

            reader.readAsDataURL(this.files[0]);
        } else {
            self.clearImage();
        }
    },

    clearImage: function () {
        var self = VisualExaminationViewModel;

        $(self.form).find('[type=file]').val('');
        $(self.image).attr('src', '');
        $(self.image).hide();
    },

    save: function () {
        var self = VisualExaminationViewModel,
            data,
            count;

        if ($(self.form).valid()) {
            data = {
                Id: $('#VisualExaminationViewModel_Id').val(),
                Type: $('#VisualExaminationViewModel_Type').val(),
                TypeName: $('#VisualExaminationViewModel_Type').find('option:selected').text(),
                InputInformation: $('#VisualExaminationViewModel_InputInformation').val(),
                ConsultInformation: $('#VisualExaminationViewModel_ConsultInformation').val(),
                Date: $('#VisualExaminationViewModel_Date').val(),
                FileType: self.imageType,
                FileContent: $(self.image).attr('src')
            };

            if ($('#visualExaminationCounter').val()) {
                count = parseInt($('#visualExaminationCounter').val());
                self.grid.updateRow(count, data);
            } else {
                self.grid.addRow(data);
            }

            self.modal.modal('hide');
        }
    }
}

$(document).ready(function () {
    VisualExaminationViewModel.init();
});
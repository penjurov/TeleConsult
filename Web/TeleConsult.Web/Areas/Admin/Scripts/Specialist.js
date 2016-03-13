var SpecialistViewModel = {
    grid: null,
    gridId: '#specialistsGrid',
    validator: null,
    dialog: '#specialistDialog',
    form: '#specialistForm',
    modal: null,

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();
        self.initValidation();

        self.search();
    },

    initGrid: function() {
        var self = SpecialistViewModel;

        self.grid = $(self.gridId).grid({
            dataKey: 'ID',
            params: { sortBy: '', direction: '' },
            columns: [
                { title: 'Специалист', field: 'Names', width: 400,  sortable: true },
                { title: 'УИН', field: 'Uin', width: 100, sortable: true },
                { title: 'Специалност', field: 'SpecialityName', sortable: true },
                { title: 'Лечебно заведение', field: 'HospitalName', sortable: true },
                { title: 'Имейл', field: 'Email', sortable: true },
                { title: 'Телефон', field: 'PhoneNumber', width: 150, sortable: true },
                { title: '', field: 'Edit', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-pencil', tooltip: "Редакция", events: { 'click': self.edit } },
                { title: '', field: 'Delete', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-remove', tooltip: "Изтриване", events: { 'click': self.changeStatus } }
            ],
            pager: { enable: true, limit: 10, sizes: [10, 20, 50, 100] },
            autoLoad: false,
            uiLibrary: 'bootstrap',
            notFoundText: 'Няма намерени записи'
        });

        self.grid.on('cellDataBound', function (e, $wrapper, id, column, record) {
            var $icon, iconType;

            if ('Names' === column.field) {
                $wrapper.text($.format('{0} {1} {2}', record.TitleName, record.FirstName, record.LastName))
            }

            if ('Delete' === column.field) {
                iconType = record.IsDeleted ? 'play' : 'remove';
                $icon = $('<span/>')
                        .addClass('ui-icon glyphicon glyphicon-' + iconType + ' display-inline-block cursor-pointer')
                        .attr('title', record.IsDeleted ? 'Активирай' : 'Изтрий');

                $wrapper.empty().append($icon);
            }
        });
    },

    initEvents: function () {
        var self = SpecialistViewModel;

        $('#btnAddSpecialist').on('click', self.add);
        $('#btnSaveSpecialist').on('click', self.save);
        $('#btnCancel').on('click', function () {
            self.modal.modal('hide');
        });

        $('#btnSearch').on('click', self.search);
    },

    initValidation: function () {
        var self = SpecialistViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    search: function () {
        var self = SpecialistViewModel,
            params;

        params = {
            Name: $('#searchName').val(),
            IsDeleted: $('#searchStatus').val(),
            Title: $('#searchTitle').val(),
            SpecialityId: $('#searchSpeciality').val(),
            HospitalId: $('#searchHospital').val(),
        };

        self.grid.reload(params);
    },

    add: function () {
        SpecialistViewModel.showDialog('Добави специалист');
    },

    changeStatus: function (e) {
        var self = SpecialistViewModel,
            url = e.data.record.IsDeleted ?
                    $(self.grid).data('url-activate') :
                    $(self.grid).data('url-delete'),

            params = { id: e.data.record.Id },
            onSuccess;

        onSuccess = function () {
            self.grid.reload();
        };

        ajaxModel.post(url, params, onSuccess);
    },

    edit: function (e) {
        var self = SpecialistViewModel;

        self.showDialog('Редактирай специалист', e.data.record);
    },

    showDialog: function (title, record) {
        var self = SpecialistViewModel;

        self.populateFields(record);
        self.modal = $(self.dialog).modal();
        self.modal.find('.modal-title').text(title);
        self.modal.off('hide.bs.modal')
            .on('hide.bs.modal', function (event) {
                self.validator.resetForm();
                $('input.validation-error').removeClass('validation-error');
            });
    },

    populateFields: function (record) {
        var self = SpecialistViewModel;

        if (record) {
            $('#loginInfoWrapper').hide();

            $('#Id').val(record.Id);
            $('#ViewModel_UserName').val(record.UserName);
            $('#ViewModel_Password').val('DummyPassToSkipValidationOnEdit');
            $('#ViewModel_Title').val(record.Title);
            $('#ViewModel_FirstName').val(record.FirstName);
            $('#ViewModel_LastName').val(record.LastName);
            $('#ViewModel_Uin').val(record.Uin);
            $('#ViewModel_PhoneNumber').val(record.PhoneNumber);
            $('#ViewModel_Email').val(record.Email);
            $('#ViewModel_HospitalId').val(record.HospitalId);
            $('#ViewModel_SpecialityId').val(record.SpecialityId);
        } else {
            $('#loginInfoWrapper').show();

            $('#Id').val('');
            $(self.dialog).find(':text, select').val('');
        }
    },

    save: function () {
        var self = SpecialistViewModel,
            params = {},
            url,
            onSuccess;

        if ($(self.form).valid()) {
            params = {
                ID: $('#Id').val(),
                Title: $('#ViewModel_Title').val(),
                FirstName: $('#ViewModel_FirstName').val(),
                LastName: $('#ViewModel_LastName').val(),
                Uin: $('#ViewModel_Uin').val(),
                PhoneNumber: $('#ViewModel_PhoneNumber').val(),
                Email: $('#ViewModel_Email').val(),
                HospitalId: $('#ViewModel_HospitalId').val(),
                SpecialityId: $('#ViewModel_SpecialityId').val(),
                UserName: $('#ViewModel_UserName').val(),
                Password: $('#ViewModel_Password').val()
            };

            url = $(this).data('url');

            onSuccess = function () {
                self.modal.modal('hide');
                self.grid.reload();
            }

            ajaxModel.post(url, params, onSuccess)
        }
    }
}

$(document).ready(function () {
    SpecialistViewModel.init();
});
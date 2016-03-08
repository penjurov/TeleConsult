var SpecialityViewModel = {
    grid: null,
    gridId: '#specialityGrid',
    validator: null,
    dialog: '#specialityDialog',
    form: '#specialityForm',
    modal: null,

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();
        self.initValidation();

        self.search();
    },

    initGrid: function () {
        var self = SpecialityViewModel;

        self.grid = $(self.gridId).grid({
            dataKey: 'ID',
            params: { sortBy: '', direction: '' },
            columns: [
                { title: 'Специалност', field: 'Name', sortable: true },
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
        var self = SpecialityViewModel;

        $('#btnAddSpeciality').on('click', self.add);
        $('#btnSaveSpeciality').on('click', self.save);
        $('#btnCancel').on('click', function () {
            self.modal.modal('hide');
        });

        $('#btnSearch').on('click', self.search);
    },

    initValidation: function () {
        var self = SpecialityViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    search: function () {
        var self = SpecialityViewModel,
            params;

        params = {
            Name: $('#searchName').val(),
            IsDeleted: $('#searchStatus').val()
        };

        self.grid.reload(params);
    },

    add: function () {
        SpecialityViewModel.showDialog('Добави специалност');
    },

    changeStatus: function (e) {
        var self = SpecialityViewModel,
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
        var self = SpecialityViewModel;

        self.showDialog('Редактирай специалност', e.data.record);
    },

    showDialog: function (title, record) {
        var self = SpecialityViewModel;

        self.populateEquipmentFields(record);
        self.modal = $(self.dialog).modal();
        self.modal.find('.modal-title').text(title);
        self.modal.off('hide.bs.modal')
            .on('hide.bs.modal', function (event) {
                self.validator.resetForm();
                $('input.validation-error').removeClass('validation-error');
            });
    },

    populateEquipmentFields: function (record) {
        var self = SpecialityViewModel;

        if (record) {
            $('#Id').val(record.Id);
            $('#ViewModel_Name').val(record.Name);
        } else {
            $('#Id').val('');
            $('#ViewModel_Name').val('');
        }
    },

    save: function () {
        var self = SpecialityViewModel,
            params = {},
            url,
            onSuccess;

        if ($(self.form).valid()) {
            params = {
                ID: $('#Id').val(),
                Name: $('#ViewModel_Name').val()
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
    SpecialityViewModel.init();
});
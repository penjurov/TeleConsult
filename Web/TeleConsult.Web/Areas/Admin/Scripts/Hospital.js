var HospitalViewModel = {
    grid: null,
    gridId: '#hospitalsGrid',
    validator: null,
    dialog: '#hospitalDialog',
    form: '#hospitalForm',
    modal: null,

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();
        self.initValidation();

        self.search();
    },

    initGrid: function() {
        var self = HospitalViewModel;

        self.grid = $(self.gridId).grid({
            dataKey: 'ID',
            params: { sortBy: '', direction: '' },
            columns: [
                { title: 'Лечебно заведение', field: 'Name', sortable: true },
                { title: 'Телефон', field: 'Phone', width: 150, sortable: true },
                { title: 'Адрес', field: 'Address', width: 500, sortable: true },
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
        var self = HospitalViewModel;

        $('#btnAddHospital').on('click', self.add);
        $('#btnSaveHospital').on('click', self.save);
        $('#btnCancel').on('click', function () {
            self.modal.modal('hide');
        });

        $('#btnSearch').on('click', self.search);
        $('#btnGetLocation').on('click', self.getLocation);
        $('#btnShowLocation').on('click', self.showLocation);
    },

    initValidation: function () {
        var self = HospitalViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    search: function () {
        var self = HospitalViewModel,
            params;

        params = {
            Name: $('#searchName').val(),
            IsDeleted: $('#searchStatus').val()
        };

        self.grid.reload(params);
    },

    add: function () {
        HospitalViewModel.showDialog('Добави лечебно заведение');
    },

    changeStatus: function (e) {
        var self = HospitalViewModel,
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
        var self = HospitalViewModel;

        self.showDialog('Редактирай лечебно заведение', e.data.record);
    },

    showDialog: function (title, record) {
        var self = HospitalViewModel;

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
        var self = HospitalViewModel;

        if (record) {
            $('#Id').val(record.Id);
            $('#ViewModel_Name').val(record.Name);
            $('#ViewModel_Address').val(record.Address);
            $('#ViewModel_Phone').val(record.Phone);
            $('#ViewModel_Latitude').val(record.Latitude);
            $('#ViewModel_Longitude').val(record.Longitude);
        } else {
            $('#Id').val('');
            $(self.dialog).find(':text').val('');
        }
    },

    save: function () {
        var self = HospitalViewModel,
            params = {},
            url,
            onSuccess;

        if ($(self.form).valid()) {
            params = {
                ID: $('#Id').val(),
                Name: $('#ViewModel_Name').val(),
                Address: $('#ViewModel_Address').val(),
                Phone: $('#ViewModel_Phone').val(),
                Latitude: $('#ViewModel_Latitude').val(),
                Longitude: $('#ViewModel_Longitude').val()
            };

            url = $(this).data('url');

            onSuccess = function () {
                self.modal.modal('hide');
                self.grid.reload();
            }

            ajaxModel.post(url, params, onSuccess)
        }
    },

    getLocation: function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                $('#ViewModel_Latitude').val(position.coords.latitude.toFixed(2)).change();
                $('#ViewModel_Longitude').val(position.coords.longitude.toFixed(2)).change();
            });
        } else{
            alert('Моля разрешете геолокацията на браузера Ви.')
        }
    },

    showLocation: function () {
        var long = parseFloat($('#ViewModel_Longitude').val()),
            lat = parseFloat($('#ViewModel_Latitude').val());

        if (long && lat && !isNaN(long) && !isNaN(lat)) {
            window.open("https://maps.google.com/maps?q=" + lat + ' ' + long, '_blank');
        } else {
            alert('Невалидни координати.');
        }
    }
}

$(document).ready(function () {
    HospitalViewModel.init();
});
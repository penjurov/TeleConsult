var EmergencyConsultationViewModel = {
    grid: null,
    gridId: '#emergencyConsultationsGrid',

    setSpecialistDialog: '#setSpecialistDialog',
    setSpecialistForm: '#setSpecialistForm',
    modal: null,

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();
        self.initHub();

        self.search();
    },

    initGrid: function () {
        var self = EmergencyConsultationViewModel;

        self.grid = $(self.gridId).grid({
            primaryKey: 'ID',
            columns: [
                { title: 'Пациент', field: 'PatientInitials', align: 'center', sortable: true },
                { title: 'Възраст', field: 'PatientAge', align: 'center', sortable: true },
                { title: 'Пол', field: 'Gender', align: 'center', sortable: true },
                { title: 'Предполагаема диагноза', field: 'PreliminaryDiagnosisDescription', align: 'center', width: 700, sortable: true },
                { title: 'Дата', field: 'Date', align: 'center', sortable: true },
                { title: '', field: 'SetSpecialist', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Избери специалист', events: { 'click': self.chooseSpecialist } },
            ],
            pager: { enable: true, limit: 10, sizes: [10, 20, 50, 100] },
            autoLoad: false,
            uiLibrary: 'bootstrap',
            notFoundText: 'Няма добавени записи'
        });
    },

    initEvents: function () {
        var self = EmergencyConsultationViewModel;

        $('#btnSetSpecialist').on('click', self.setSpecialist);
        $('#btnCancel').on('click', function () {
            self.modal.modal('hide');
        });
    },

    initHub: function () {
        var self = EmergencyConsultationViewModel,
            consultationHub = $.connection.consultationHub;

        consultationHub.client.refreshEmergency = function (consultationId, isInsert) {
            var ids;

            if (isInsert) {
                self.search();
            } else {
                ids = _.map(self.grid.getAll(), function (item) {
                    return item.record.Id;
                });

                if (ids.indexOf(consultationId) > -1) {
                    self.search();
                }
            }
        };

        $.connection.hub.start();
    },

    search: function () {
        var self = EmergencyConsultationViewModel,
            params;

        params = {
            Hospital: $('#searchHospital').val()
        };

        self.grid.reload(params);
    },

    chooseSpecialist: function (e) {
        var self = EmergencyConsultationViewModel,
            url,
            onSuccess,
            specialistsSelect;

        url = $('#getSpecialistOnSchedule').val();

        onSuccess = function (result) {
            $('#consultationId').val(e.data.record.Id);

            specialistsSelect = $('#specialists');

            specialistsSelect.empty();

            _.each(result, function (item) {
                specialistsSelect.append($('<option />').val(item.Value).text(item.Text));
            });

            self.modal = $(self.setSpecialistDialog).modal();
        }

        ajaxModel.get(url, { specialityId: e.data.record.SpecialityId }, onSuccess);
    },

    setSpecialist: function () {
        var self = EmergencyConsultationViewModel,
            url,
            params,
            onSuccess;

        url = $(this).data('url');

        params = {
            consultationId: $('#consultationId').val(),
            consultantId: $('#specialists').find(':selected').val()
        }

        onSuccess = function () {
            self.modal.modal('hide');
            self.grid.reload();
        }

        ajaxModel.post(url, params, onSuccess);
    }
}


$(document).ready(function () {
    EmergencyConsultationViewModel.init();
});
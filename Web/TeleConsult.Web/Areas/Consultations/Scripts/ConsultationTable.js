var ConsultationTableViewModel = {
    grid: null,
    gridId: '#consultationsGrid',
    currentSpecialistId: '#CurrentSpecialistId',

    init: function () {
        var self = this;

        self.initEvents();
        self.initGrid();
        self.initHub();

        self.search();
    },

    initGrid: function () {
        var self = ConsultationTableViewModel;

        self.grid = $(self.gridId).grid({
            primaryKey: 'ID',
            columns: [
                { title: 'Пациент', field: 'PatientInitials', align: 'center', sortable: true },
                { title: 'Възраст', field: 'PatientAge', align: 'center', sortable: true },
                { title: 'Пол', field: 'Gender', align: 'center', sortable: true },
                { title: 'Предполагаема диагноза', field: 'PreliminaryDiagnosisDescription', align: 'center', width: 700, sortable: true },
                { title: 'Вид', field: 'Type', align: 'center', sortable: true },
                { title: 'Дата', field: 'Date', align: 'center', sortable: true },
                { title: '', field: 'Edit', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Редакция', events: { 'click': self.edit } }
            ],
            pager: { enable: true, limit: 10, sizes: [10, 20, 50, 100] },
            autoLoad: false,
            uiLibrary: 'bootstrap',
            notFoundText: 'Няма добавени записи'
        });

        self.grid.on('rowDataBound', function (e, $row, id, record) {
            $row.removeClass();
            $row.addClass(record.StageName);
        });
    },

    initEvents: function () {
        var self = ConsultationTableViewModel;

    },

    initHub: function () {
        var self = ConsultationTableViewModel,
            consultationHub = $.connection.consultationHub;

        consultationHub.client.refresh = function (consultationId, consultantId, isInsert) {
            var ids;

            if (isInsert && consultantId === $(self.currentSpecialistId).val()) {
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

    edit: function (e) {
        var url = $('#consultationUrl').val();

        location.href = $.format('{0}?id={1}', url, e.data.record.Id);
    },

    search: function () {
        var self = ConsultationTableViewModel,
            params;

        params = {
            IsConsultation: $('#IsConsultation').val()
        };

        self.grid.reload(params);
    },
}


$(document).ready(function () {
    ConsultationTableViewModel.init();
});
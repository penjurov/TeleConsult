var ConsultationTableViewModel = {
    grid: null,
    gridId: '#consultationsGrid',

    init: function () {
        var self = this;

        self.initEvents();
        self.initGrid();

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
    },

    initEvents: function () {
        var self = ConsultationTableViewModel;

    },

    edit: function () {

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
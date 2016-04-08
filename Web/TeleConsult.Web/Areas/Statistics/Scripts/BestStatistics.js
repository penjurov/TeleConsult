var BestStatisticsViewModel = {
    bestHospitalsGrid: null,
    bestHospitalsGridId: '#bestHospitalsGrid',

    bestDoctorsGrid: null,
    bestDoctorsGridId: '#bestDoctorsGrid',

    init: function () {
        var self = this;

        self.initGrids();
        self.initEvents();
    },

    initGrids: function () {
        var self = BestStatisticsViewModel;

        self.bestHospitalsGrid = $(self.bestHospitalsGridId).grid({
            primaryKey: 'ID',
            columns: [
                { title: 'Лечебно заведение', field: 'Name', align: 'center'},
                { title: 'Среден рейтинг', field: 'Rating', align: 'center'},
            ],
            autoLoad: true,
            uiLibrary: 'bootstrap',
            notFoundText: 'Няма добавени записи'
        });

        self.bestHospitalsGrid.on('cellDataBound', function (e, $wrapper, id, column, record) {
            if ('Rating' === column.field) {
                var span = $('<span />').addClass('stars').html($('<span />').width(record.Rating * 8));
                $wrapper.html(span);
            }
        });

        self.bestDoctorsGrid = $(self.bestDoctorsGridId).grid({
            primaryKey: 'ID',
            columns: [
                { title: 'Лекар', field: 'FullName', align: 'center'},
                { title: 'Лечебно заведение', field: 'HospitalName', align: 'center'},
                { title: 'Среден рейтинг', field: 'Rating', align: 'center'},
            ],
            autoLoad: true,
            uiLibrary: 'bootstrap',
            notFoundText: 'Няма добавени записи'
        });

        self.bestDoctorsGrid.on('cellDataBound', function (e, $wrapper, id, column, record) {
            if ('Rating' === column.field) {
                var span = $('<span />').addClass('stars').html($('<span />').width(record.Rating * 8));
                $wrapper.html(span);
            }
        });
    },

    initEvents: function () {
        var self = BestStatisticsViewModel;

    }
}

$(document).ready(function () {
    BestStatisticsViewModel.init();
});
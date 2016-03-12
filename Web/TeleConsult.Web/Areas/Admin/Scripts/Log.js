var LogViewModel = {
    grid: null,
    gridId: '#logsGrid',
    modal: null,

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();

        self.search();
    },

    initGrid: function() {
        var self = LogViewModel;

        self.grid = $(self.gridId).grid({
            dataKey: 'ID',
            params: { sortBy: '', direction: '' },
            columns: [
                { title: 'Дата', field: 'Date', width: 200, sortable: true },
                { title: 'Действие', field: 'ActionName', width: 400, sortable: true },
                { title: 'Потребител', field: 'UserName', width: 250, sortable: true },
                { title: 'Детайли', field: 'Details', sortable: true }
            ],
            pager: { enable: true, limit: 10, sizes: [10, 20, 50, 100] },
            autoLoad: false,
            uiLibrary: 'bootstrap',
            notFoundText: 'Няма намерени записи'
        });
    },

    initEvents: function () {
        var self = LogViewModel;

        $('#btnSearch').on('click', self.search);
        $('.date').datetimepicker();
    },

    search: function () {
        var self = LogViewModel,
            params;

        params = {
            Details: $('#searchDetails').val(),
            ActionType: parseInt($('#searchAction').val(), 10),
            UserId: $('#searchUser').val(),
            StartDate: $('#searchFrom').val(),
            EndDate: $('#searchTo').val()
        };

        self.grid.reload(params);
    }
}

$(document).ready(function () {
    LogViewModel.init();
});
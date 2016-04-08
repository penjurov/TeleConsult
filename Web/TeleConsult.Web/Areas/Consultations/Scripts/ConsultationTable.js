var ConsultationTableViewModel = {
    grid: null,
    gridId: '#consultationsGrid',
    currentSpecialistId: '#CurrentSpecialistId',

    evaluateDialog: '#evaluateDialog',
    evaluateForm: '#evaluateForm',
    modal: null,
    validator: null,

    init: function () {
        var self = this;

        self.initGrid();
        self.initEvents();
        self.initValidation();
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
                { title: '', field: 'Edit', width: 32, align: 'center', type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Редакция', events: { 'click': self.edit } },
                { title: '', field: 'Rating', width: 100, align: 'center', type: 'icon', icon: 'glyphicon-star', tooltip: 'Рейтинг', events: { 'click': self.openEvaluateDialog } }
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

        self.grid.on('cellDataBound', function (e, $wrapper, id, column, record) {
            if ('Rating' === column.field) {
                if (record.Rating !== null) {
                    $wrapper.empty().closest('td').off('click');
                    var span = $('<span />').addClass('stars').html($('<span />').width(record.Rating * 8));

                    $wrapper.html(span);
                } else {
                    if ($('#IsConsultation').val() === 'False' && record.StageName === 'finnished') {
                        $wrapper.css('display', '');
                    } else {
                        $wrapper.hide().closest('td').off('click');
                    }
                }
            }
        });
    },

    initValidation: function () {
        var self = ConsultationTableViewModel;

        self.validator = $(self.evaluateForm).validate({
            rules: {
                evaluationValue: {
                    required: true,
                    maxDecimalDigits: 1,
                    min: 0,
                    max: 10
                }
            },
            errorElement: 'div',
            errorClass: 'validation-error'
        });
    },

    initEvents: function () {
        var self = ConsultationTableViewModel;

        $('#btnEvaluate').on('click', self.evaluate);
        $('#btnCancel').on('click', function () {
            self.modal.modal('hide');
        });
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

    openEvaluateDialog: function (e) {
        var self = ConsultationTableViewModel;

        $('#consultationId').val(e.data.record.Id);
        $('#evaluationValue').val('');

        self.modal = $(self.evaluateDialog).modal();
        self.modal.off('hide.bs.modal')
            .on('hide.bs.modal', function (event) {
                self.validator.resetForm();
                $('input.validation-error').removeClass('validation-error');
            });
    },

    evaluate: function () {
        var self = ConsultationTableViewModel,
            url,
            params,
            onSuccess;

        url = $(this).data('url');

        params = {
            consultationId: parseInt($('#consultationId').val(), 10),
            rating: parseFloat($('#rating').val())
        }

        onSuccess = function () {
            self.modal.modal('hide');
            self.grid.reload();
        }

        ajaxModel.post(url, params, onSuccess);
    }
}


$(document).ready(function () {
    ConsultationTableViewModel.init();
});
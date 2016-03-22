var SchedulesViewModel = {
    calendarId: '#calendar',
    validator: null,
    dialog: '#scheduleDialog',
    form: '#scheduleForm',
    modal: null,

    init: function () {
        var self = this;

        self.initCalendar();
        self.initEvents();
        self.initValidation();
        $('.date').datetimepicker({
            format: 'DD/MM/YYYY h:mm A'
        });

        self.search();
    },

    initCalendar: function () {
        var self = SchedulesViewModel;

        $(self.calendarId).fullCalendar({
            lang: 'bg',
            header: {
                left: '',
                center: 'prev title next',
                right: 'today month,agendaWeek,agendaDay'
            },
            buttonText: {
                today:    'Днес',
                month:    'Месечен',
                week:     'Седмичен',
                day:      'Дневен'
            },
            editable: true,
            defaultView: 'agendaWeek',
            allDaySlot: true,
            scrollTime: '00:00:00',
            slotEventOverlap: false,
            eventDrop: function (event, revertFunc, delta, jsEvent, ui, view) {
                self.editOnDrag(event, revertFunc);
            },
            eventResize: function (event, revertFunc, delta, jsEvent, ui, view) {
                self.editOnDrag(event, revertFunc);
            },
            eventClick: function (event) {
                self.showDialog('Редактирай график', event);
            },
            dayClick: function (date, allDay, jsEvent, view) {
                if (date.format('HH:mm') === '00:00') {
                    self.asAvailable = "true";
                }
            },
            select: function (start, end) {
                self.showDialog('Добави график', null, start.format('DD/MM/YYYY h:mm A'), end.format('DD/MM/YYYY h:mm A'));
            },
            selectable: true,
            selectHelper: true,
            height: 720
        });
    },

    initEvents: function () {
        var self = SchedulesViewModel;

        $('#btnSaveSchedule').on('click', self.save);
        $('#btnCancel').on('click', function () {
            self.modal.modal('hide');
        });

        $('#btnSearch').on('click', self.search);

        $('.fc-button').on('click', function (e) {
            e.preventDefault();
            self.search();
        });
    },

    initValidation: function () {
        var self = SchedulesViewModel;

        self.validator = $(self.form).validate();
        self.validator.settings.errorElement = 'div';
        self.validator.settings.errorClass = 'validation-error';
    },

    showDialog: function (title, record, startDate, endDate) {
        var self = SchedulesViewModel;

        self.populateFields(record, startDate, endDate);
        self.modal = $(self.dialog).modal();
        self.modal.find('.modal-title').text(title);
        self.modal.off('hide.bs.modal')
            .on('hide.bs.modal', function (event) {
                self.validator.resetForm();
                $('input.validation-error').removeClass('validation-error');
            });
    },

    populateFields: function (record, startDate, endDate) {
        var self = SchedulesViewModel;

        if (record) {
            $('#Id').val(record.Id);
            $('#ViewModel_SpecialistId').val(record.SpecialistId);
            $('#ViewModel_StartDate').val(record.start.format('DD/MM/YYYY h:mm A'));
            $('#ViewModel_EndDate').val(record.end.format('DD/MM/YYYY h:mm A'));
            $('#ViewModel_IsAllDay').val(record.IsAllDay);
            $('#ViewModel_Description').val(record.Description);
        } else {
            $('#Id').val('');
            $(self.dialog).find(':text, select').val('');
            $('#ViewModel_StartDate').val(startDate);
            $('#ViewModel_EndDate').val(endDate);
        }
    },

    editOnDrag: function (event, revertDrag) {
        var self = SchedulesViewModel;

        if (!event.end) {
            revertDrag();
        } else {
            self.save(event);
        }
    },

    save: function (event) {
        var self = SchedulesViewModel,
            params, url, onSuccess;

        if ($(self.form).valid()) {
            url = $("#saveSchedule").val();

            onSuccess = function () {
                self.modal.modal('hide');
                self.search();
            }

            params = {
                Id: event.Id ? event.Id : $('#Id').val(),
                SpecialistId: event.SpecialistId ? event.SpecialistId : $('#ViewModel_SpecialistId').val(),
                StartDate: event.start ? event.start.format('DD/MM/YYYY h:mm A') : $('#ViewModel_StartDate').val(),
                EndDate: event.end ? event.end.format('DD/MM/YYYY h:mm A') : $('#ViewModel_EndDate').val(),
                IsAllDay: event.IsAllDay ?  event.IsAllDay : $('#ViewModel_IsAllDay').is(':selected'),
                Description: event.Description ? event.Description : $('#ViewModel_Description').val()
            }

            ajaxModel.post(url, params, onSuccess);
        }
    },

    search: function () {
        var self = SchedulesViewModel,
            startDate, endDate, onSucess, url, view;

        if (self.ajaxInProcess) {
            return;
        }

        self.ajaxInProcess = true;

        view = $(self.calendarId).fullCalendar('getView');

        if ((view.end !== undefined) && (view.start !== undefined)) {
            endDate = view.end.format('MM/DD/YYYY');
            startDate = view.start.format('MM/DD/YYYY');
        }

        params = {
            StartDate: startDate,
            EndDate: endDate,
            Description: $('#searchDescription').val(),
            SpecialistId: $('#searchSpecialist').val()
        }

        onSucess = function (response) {
            if (response) {
                self.fillCalendar(response);
            } else {
                self.fillCalendar([]);
            }

            self.ajaxInProcess = false;
        };

        url = $('#getSchedules').val();
        
        ajaxModel.get(url, params, onSucess);
    },

    fillCalendar: function (items) {
        var self = SchedulesViewModel,
            sourceArray = [],
            $calendar = $(self.calendarId);

        $.each(items, function (index, item) {

            if (item.IsAllDay) {
                item.allDay = true;
            }

            item.start = new Date(parseInt(item.StartDate.substr(6)));
            item.end = new Date(parseInt(item.EndDate.substr(6)));

            item.title = item.SpecialistName + '; ' + item.SpecialistSpeciality;

            sourceArray.push(item);
        });

        $calendar.fullCalendar('removeEvents');
        $calendar.fullCalendar('removeEventSources');
        $calendar.fullCalendar('addEventSource', sourceArray);
    }
}

$(document).ready(function () {
    SchedulesViewModel.init();
});
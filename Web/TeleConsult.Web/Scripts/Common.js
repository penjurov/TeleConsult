jQuery.validator.methods.date = function (value, element) {
    var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
    if (isChrome) {
        var dateParts = value.split('/');
        var dateStr = dateParts[2] + '-' + dateParts[1] + '-' + dateParts[0];
        return this.optional(element) || !/Invalid|NaN/.test(new Date(dateStr));
    } else {
        return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
    }
};

jQuery.extend(jQuery.validator.messages, {
    required: "Полето е задължително",
    max: jQuery.validator.format("Моля въведете число по-малко или равно на {0}."),
    min: jQuery.validator.format("Моля въведете число по-голямо или равно на {0}.")
});

jQuery.validator.addMethod("maxDecimalDigits", function (value, element, params) {
    var numberOfDecimalPlaces = (value.split('.')[1] || []).length;
    return this.optional(element) || numberOfDecimalPlaces <= params;
}, $.format("Оценката не може да има повече от {0} знака след десетичната запетая."));

var ajaxModel = {
    get: function (url, params, onSuccessFunc, onErrorFunc) {
        var settings = { url: url, data: params, type: 'GET', dataType: 'json' };
        $.ajax(settings).done(onSuccessFunc).fail(onErrorFunc || utils.onError);
    },

    post: function (url, params, onSuccessFunc, onErrorFunc, verificationToken) {
        var settings = {
            url: url,
            data: JSON.stringify(params),
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            headers: {
                'VerificationToken': verificationToken
            },
        };
        $.ajax(settings).done(onSuccessFunc).fail(onErrorFunc || utils.onError);
    },

    postFile: function (url, params, onSuccessFunc, onErrorFunc, customSettings) {
        var settings = { url: url, data: params, type: 'POST', processData: false, contentType: false };
        $.ajax(settings).done(onSuccessFunc).fail(onErrorFunc || utils.onError);
    }
};

function alert(text, okHandler) {
    //clean previous div for a dialog window alerts/dialogs are not stackable 
    var statusHandle = '';
    $('#alert-modal').remove();

    var $dialog = $('<div class="modal alert-modal" id="alert-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
        '<div class="modal-dialog modal-sm">' +
        '<div class="modal-content">' +

        '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                '<h4 class="modal-title">Грешка</h4>' +
            '</div>' +

        '<div class="modal-body  text-center">' + text + '</div>' +
        '<div class="modal-footer"> <button type="button" class="btn btn-default" data-dismiss="modal">OK</button> </div>' +
        '</div> </div> </div>');

    $('body').append($dialog);
    $dialog.modal();
};

function confirm (text, successHandler, failHandler, acceptButtonText, cancelButtonText, title) {
    var okText, cancelText, onDismiss;

    okText = acceptButtonText ? acceptButtonText : 'Потвърди';
    cancelText = cancelButtonText ? cancelButtonText : 'Отказ';

    onDismiss = function (event) {
        if (failHandler && $.isFunction(failHandler)) {
            failHandler(event);
        };
    };

    BootstrapDialog.show({
        title: title || 'Теле Консулт',
        message: text,
        onhide: onDismiss,
        buttons: [{
            label: okText,
            action: function (confirm) {
                if (successHandler && $.isFunction(successHandler)) {
                    successHandler();
                };
                confirm.close();
            }
        }, {
            label: cancelText,
            action: function (confirm) {
                onDismiss();
                confirm.close();
            }
        }]
    });
};

var utils = {
    onError: function (error) {
        message = JSON.parse(error.responseText).Message;
        alert(message);
    }
}
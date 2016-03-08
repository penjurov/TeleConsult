var ajaxModel = {
    get: function (url, params, onSuccessFunc, onErrorFunc) {
        var settings = { url: url, data: params, type: 'GET', dataType: 'json' };
        $.ajax(settings).done(onSuccessFunc).fail(onErrorFunc || utils.onError);
    },

    post: function (url, params, onSuccessFunc, onErrorFunc) {
        var settings = { url: url, data: JSON.stringify(params), type: 'POST', contentType: 'application/json', dataType: 'json' };
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
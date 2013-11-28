define(["jquery", "knockout", "kovalidation", "signalr.hubs", "bootstrap"], function($, ko) {
    var index = {};

    index.loading = ko.observable(true);
    index.error = ko.observable(false);

    index.id = ko.observable();

    index.author = ko.observable('').extend({
        required: {
            message: "'Author' should not be empty."
        }
    });

    index.content = ko.observable('').extend({
        required: {
            message: "'Content' should not be empty."
        }
    });

    index.create = function() {
        if (!ko.validation.isValid(index)) {
            return;
        }

        $.ajax({
            type: "POST",
            url: "api/posts/",
            data: { Author: index.author(), Content: index.content() }
        }).done(function(response) {
            index.formModal.modal('hide');
            $(document).trigger('postCreated', [response]);
        })
        .fail(function(request, status, error) {
            if (request.status == 400) {
                var validationResult = $.parseJSON(request.responseText);
                ko.validation.rebindValidations(index, validationResult.Results.Errors);
            }

            if (request.status == 500) {
                alert("internal server");
            }
        });
    };

    $(document).bind('loadPostCreation', function(e, id) {
        ko.validation.clearValidations(index);
        index.formModal.modal('show');
    });

    $(function() {
        index.formModal = $('#post-creation-modal').modal({ backdrop: true, closeOnEscape: true, modal: true, show: false });
    });

    return index;
});
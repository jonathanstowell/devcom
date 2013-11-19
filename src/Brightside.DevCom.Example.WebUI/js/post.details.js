require(["jquery", "knockout", "kovalidation", "signalr.hubs", "bootstrap"], function($, ko) {
    var index = {};

    index.loading = ko.observable(true);
    index.error = ko.observable(false);
    index.editing = ko.observable(false);

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

    index.comments = ko.observableArray([]);

    index.edit = function() {
        index.editing(!index.editing());
    };

    index.save = function() {
        if (!ko.validation.isValid(index)) {
            return;
        }

        $.ajax({
            type: "PUT",
            url: "api/posts/",
            data: { Id: index.id(), Author: index.author(), Content: index.content() }
        }).done(function(response) {
            index.editing(!index.editing());
            $(document).trigger('postUpdated', [response]);
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

    $(document).bind('loadPostDetails', function(e, id) {
        $.getJSON("api/posts/" + id, function(response) {
            index.id(response.id);
            index.author(response.author);
            index.content(response.content);
            index.comments(response.comments);

            index.editing(false);

            index.formModal.modal('show');
        });
    });

    $(function() {
        ko.applyBindings(index, $('#post-details')[0]);
        index.formModal = $('#post-details-modal').modal({ backdrop: true, closeOnEscape: true, modal: true, show: false });
    });

});
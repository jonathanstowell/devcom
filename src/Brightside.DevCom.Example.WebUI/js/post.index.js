requirejs(["jquery", "knockout", "komapping", "signalr.hubs"], function($, ko, mapping) {
    ko.mapping = mapping;

    var index = {};

    var hub = $.connection.postHub;

    index.loading = ko.observable(true);
    index.error = ko.observable(false);
    index.posts = ko.observableArray([]);

    index.selectedPost = ko.observable(null);

    index.details = function() {
        $(document).trigger('loadPostDetails', [this.id()]);
    };

    index.create = function() {
        $(document).trigger('loadPostCreation');
    };

    index.load = function() {

        index.loading(true);
        index.error(false);

        $.getJSON("api/posts", function(response) {
            index.loading(false);
            index.error(false);

            ko.mapping.fromJS(response, {}, index.posts);
        })
            .fail(function(request, status, error) {
                index.error(true);
            })
            .always(function() {
                index.loading(false);
            });
    };

    $(document).bind('postCreated', function(e, post) {
        var item = ko.utils.arrayFirst(index.posts(), function(item) {
            return item.id() == post.id;
        });

        if (item) {
            return;
        }

        index.posts.push(ko.mapping.fromJS(post));
    });

    $(document).bind('postUpdated', function(e, post) {
        var item = ko.utils.arrayFirst(index.posts(), function(item) {
            return item.id() == post.id;
        });

        item.author(post.author);
        item.content(post.content);
        item.lastModifiedDateTime(post.lastModifiedDateTime);
    });

    if (hub) {
        hub.client.createdPost = function(post) {
            var item = ko.utils.arrayFirst(index.posts(), function(item) {
                return item.id() == post.id;
            });

            if (item) {
                return;
            }

            index.posts.push(ko.mapping.fromJS(post));
        };
    } else {
        console.log("Cannot resolve post hub.");
    }

    $(function() {
        ko.applyBindings(index, jQuery('#posts')[0]);
        index.load();
    });
});
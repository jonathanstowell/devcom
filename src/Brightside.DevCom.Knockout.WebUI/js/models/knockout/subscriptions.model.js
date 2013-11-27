define(["jquery", "knockout"], function($, ko) {
    var model = {};

    model.include = ko.observable(false);
    model.content = ko.observable();

    model.include.subscribe(function (include) {
        if (include) {
            model.content("New Content");
        }
        else {
            model.content("");
        }
    });

    return model;
});
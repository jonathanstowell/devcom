define(["jquery", "knockout"], function($, ko) {
    var model = {};

    model.left = ko.observable(1);
    model.right = ko.observable(2);
    model.total = ko.computed(function() {
        return parseInt(model.left()) + parseInt(model.right());
    });

    return model;
});
define(["jquery", "knockout"], function($, ko) {
    var model = {};

    model.name = ko.observable("Gary");
    model.books = ko.observableArray(["The Hobbit", "Game of Thrones"]);
    model.book = ko.observable();
    model.addToBooks = function () {
        model.books.push(model.book());
    };
    model.removeFromBooks = function (i) {
        model.books.splice(i, 1);
    };

    return model;
});
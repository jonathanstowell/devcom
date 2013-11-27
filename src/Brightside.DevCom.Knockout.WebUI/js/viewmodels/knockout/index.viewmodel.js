requirejs(["jquery",
        "knockout",
        "mylibs/plugins/models/knockout/observable.model",
        "kovalidation",
        "signalr.hubs",
        "bootstrap"], function ($, ko, observableModel) {
            
    var model = {
        observable: observableModel
    };

    $(function () {
        ko.applyBindings(model, jQuery('#knockout-observables')[0]);
    });
});
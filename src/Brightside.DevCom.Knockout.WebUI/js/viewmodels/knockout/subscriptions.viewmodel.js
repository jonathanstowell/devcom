requirejs(["jquery",
        "knockout",
        "mylibs/plugins/models/knockout/subscriptions.model",
        "kovalidation",
        "signalr.hubs",
        "bootstrap"], function ($, ko, subscriptionsModel) {
            
    var model = {
        subscriptions: subscriptionsModel
    };

    $(function () {
        ko.applyBindings(model, jQuery('#knockout-subscriptions')[0]);
    });
});
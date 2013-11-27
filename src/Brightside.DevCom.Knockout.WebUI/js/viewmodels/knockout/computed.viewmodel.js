requirejs(["jquery",
        "knockout",
        "mylibs/plugins/models/knockout/computed.model",
        "kovalidation",
        "signalr.hubs",
        "bootstrap"], function ($, ko, computedModel) {
            
    var model = {
        computed: computedModel
    };

    $(function () {
        ko.applyBindings(model, jQuery('#knockout-computed')[0]);
    });
});
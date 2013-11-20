requirejs(["jquery",
        "knockout",
        "mylibs/plugins/models/posts/list.model",
        "mylibs/plugins/models/posts/create.model",
        "mylibs/plugins/models/posts/details.model",
        "kovalidation",
        "signalr.hubs",
        "bootstrap"], function ($, ko, listModel, createModel, detailsModel) {
            
    var model = {
        list: listModel,
        create: createModel,
        details: detailsModel
    };

    $(function () {
        ko.applyBindings(model, jQuery('#posts')[0]);
    });
});
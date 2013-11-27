require.config({
    // relative url from where modules will load
    baseUrl: "/js/",
    paths: {
        "jquery": "//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min",
        "knockout": "libs/knockout-2.2.0",
        "komapping": "libs/knockout.mapping-2.2.0",
        "kovalidation": "libs/knockout.validation",
        "koextensions": "mylibs/brightside.knockout.extensions",
        "kopaging": "mylibs/brightside.knockout.paging",
        "signalr": "libs/jquery.signalR-1.1.3",
        'signalr.hubs': '../signalr/hubs?',
        'bootstrap': 'libs/bootstrap',
        'flot': 'libs/flot/jquery.flot',
        'flotpie': 'libs/flot/jquery.flot.pie',
        'flotresize': 'libs/flot/jquery.flot.resize'
    },
    shim: {
        "jquery": {
            exports: '$'
        },
        "komapping": {
            deps: ['knockout'],
            exports: 'komapping',
        },
        "kopaging": {
            deps: ['jquery', 'knockout'],
            exports: 'kopaging',
        },
        "signalr": {
            deps: ['jquery']
        },
        "signalr.hubs": {
            deps: ['signalr']
        },
        "bootstrap": {
            deps: ["jquery"]
        },
        "purl": {
            deps: ["jquery"]
        },
        "flot": {
            deps: ["jquery"]
        },
        "flotpie": {
            deps: ["flot"]
        },
        "flotresize": {
            deps: ["flot"]
        }
    }
});
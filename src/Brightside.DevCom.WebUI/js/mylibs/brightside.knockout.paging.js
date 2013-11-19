(function(factory) {
    // Module systems magic dance.

    if (typeof require === "function" && typeof exports === "object" && typeof module === "object") {
        // CommonJS or Node: hard-coded dependency on "knockout"
        factory(require("knockout"), exports);
    } else if (typeof define === "function" && define["amd"]) {
        // AMD anonymous module with hard-coded dependency on "knockout"
        define(["jquery", "knockout", "komapping", "exports"], factory);
    } else {
        // <script> tag: use the global `ko` object, attaching a `mapping` property
        factory(ko, ko.grid = {});
    }
}(function($, ko, mapping, exports) {

    if (typeof(ko) === 'undefined') {
        throw 'Knockout is required, please ensure it is loaded before loading this validation plug-in';
    }

    ko.mapping = mapping;

    // Private function

    function getColumnsForScaffolding(data) {
        if (!data) {
            return [];
        }
        if ((typeof data.length !== 'number') || data.length === 0) {
            return [];
        }
        var columns = [];
        for (var propertyName in data[0]) {
            columns.push({ headerText: propertyName, rowText: propertyName });
        }
        return columns;
    }

    ko.grid = {
        // Defines a view model class you can use to populate a grid
        viewModel: function(configuration) {
            this.data = configuration.data;

            this.loading = ko.observable(true);
            this.error = ko.observable(false);
            this.initialized = ko.observable(false);

            this.previousRequestData = {};

            this.pageCount = ko.observable();
            this.totalItemCount = ko.observable();
            this.pageNumber = ko.observable(1);
            this.pageSize = ko.observable();
            this.hasPreviousPage = ko.observable();
            this.hasNextPage = ko.observable();
            this.isFirstPage = ko.observable();
            this.isLastPage = ko.observable();
            this.hasData = ko.computed(function() {
                return this.data().length > 0;
            }, this);
            this.pages = ko.computed(function() {
                var currentPage = this.pageNumber();
                var count = this.pageCount();

                var upper = count < 9 ? count : 9;
                var lower = 1;

                if (count > 9 && currentPage > 5) {

                    if (currentPage + 4 > count) {
                        upper = count;
                        lower = 9 - (count - currentPage);
                    } else {
                        upper = currentPage + 4;
                        lower = currentPage - 4;
                    }
                }

                return ko.utils.range(lower, upper);
            }, this);

            this.functions = configuration.functions;

            this.columns = ko.observableArray(configuration.columns || getColumnsForScaffolding(ko.utils.unwrapObservable(this.data)));

            this.navigate = function(page) {
                var loadPage = 1;

                if (page === 'next') {
                    if (this.pageNumber() < ko.utils.unwrapObservable(this.pageCount())) {
                        loadPage = this.pageNumber() + 1;
                    }
                } else if (page === 'prev') {
                    if (this.pageNumber() > 1) {
                        loadPage = this.pageNumber() - 1;
                    }
                } else if (page === 'first') {
                    loadPage = 1;
                } else if (page === 'last') {
                    loadPage = this.pageCount();
                } else if (this.pageNumber() == page) {
                    return;
                } else {
                    loadPage = page;
                }

                this.load(loadPage);
            };

            this.load = function(page, requestData) {
                this.loading(true);
                this.error(false);

                requestData = requestData || {};
                requestData.page = page;

                if (configuration.primeRequestData) {
                    configuration.primeRequestData(requestData);
                }

                $.getJSON(configuration.url, requestData, (function(model) {
                    return function(response) {
                        model.loading(false);
                        model.error(false);

                        ko.mapping.fromJS(response.items, {}, model.data);

                        model.pageCount(response.pageCount);
                        model.totalItemCount(response.totalItemCount);
                        model.pageNumber(response.pageNumber);
                        model.pageSize(response.pageSize);
                        model.hasPreviousPage(response.hasPreviousPage);
                        model.hasNextPage(response.hasNextPage);
                        model.isFirstPage(response.isFirstPage);
                        model.isLastPage(response.isLastPage);

                        model.initialized(true);

                        if (configuration.done) {
                            configuration.done(response);
                        }
                    };
                }(this)))
                    .fail((function(model) {
                        return function(request, status, error) {
                            if (configuration.fail) {
                                configuration.fail(request, status, error);
                            }

                            model.error(true);
                        };
                    }(this)))
                    .always((function(model) {
                        return function() {
                            if (configuration.always) {
                                configuration.always();
                            }

                            model.loading(false);
                        };
                    }(this)));
            };
        }
    };

    // Templates used to render the grid
    var templateEngine = new ko.nativeTemplateEngine();

    templateEngine.addTemplate = function(templateName, templateMarkup) {
        var script = document.createElement('script');

        script.type = 'text/html';
        script.id = templateName;
        script.innerHTML = templateMarkup;

        $("body").append(script);
    };

    templateEngine.addTemplate("ko_grid", "\
    <div class=\"widget stacked widget-table action-table\">\
        <div class=\"widget-header\">\
            <i class=\"icon-th-list\"></i>\
            <h3><i data-bind=\"visible: loading\" class=\"icon-spinner icon-spin icon-4x\"></i>CPR</h3>\
        </div>\
        <div class=\"widget-content hide\" data-bind=\"visible: initialized() && !error(), css: { 'hide': !initialized() || error() }\">\
            <table class=\"table table-striped table-bordered\">\
                <thead>\
                    <tr data-bind=\"foreach: columns\">\
                        <th data-bind=\"text: headerText\"></th>\
                    </tr>\
                </thead>\
                <tbody data-bind=\"foreach: data\">\
                    <tr data-bind=\"foreach: $parent.columns\">\
                        <td data-bind=\"text: typeof rowText == 'function' ? rowText($parent) : $parent[rowText] \"></td>\
                    </tr>\
                </tbody>\
            </table>\
        </div>\
        <ul class=\"pagination pull-right hide\" data-bind=\"visible: data().length > 0, css: { 'hide': data().length == 0 }\">\
            <li class=\"prev-container hand\" data-bind=\"css: { disabled: pageNumber() === 1 }, click: function (e) { navigate('prev') }\">\
                <a class=\"prev\">← Previous</a>\
            </li>\
            <li class=\"first-container hand\" data-bind=\"css: { disabled: pageNumber() === 1 }, click: function (e) { navigate('first') }\">\
                <a class=\"first\">First</a>\
            </li>\
            <!-- ko foreach: pages -->\
            <li class=\"hand\" data-bind=\"css: { active: $parent.pageNumber() == $data }, click: function (e) { $parent.navigate($data) }\">\
                <a title=\"View Page $data\">\
                    <span data-bind=\"text: $data\"></span>\
                </a>\
            </li>\
            <!-- /ko -->\
            <li class=\"last-container hand\" data-bind=\"css: { disabled: pageNumber() === pageCount() }, click: function (e) { navigate('last') }\">\
                <a class=\"last\">Last</a>\
            </li>\
            <li class=\"next-container hand\" data-bind=\"css: { disabled: pageNumber() === pageCount() }, click: function (e) { navigate('next') }\">\
                <a class=\"next\">Next →</a>\
            </li>\
        </ul>");

    // The "simpleGrid" binding
    ko.bindingHandlers.grid = {
        init: function() {
            return { 'controlsDescendantBindings': true };
        },
        // This method is called to initialize the node, and will also be called again if you change what the grid is bound to
        update: function(element, viewModelAccessor, allBindingsAccessor) {
            var viewModel = viewModelAccessor(), allBindings = allBindingsAccessor();

            // Empty the element
            while (element.firstChild)
                ko.removeNode(element.firstChild);

            // Allow the default templates to be overridden
            var gridTemplateName = allBindings.gridTemplate || "ko_grid";

            // Render the main grid
            var gridContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(gridTemplateName, viewModel, { templateEngine: templateEngine }, gridContainer, "replaceNode");
        }
    };
}));
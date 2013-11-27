requirejs(["jquery", "knockout", "kovalidation"], function ($, ko) {
    ko.bindingHandlers.inverseChecked = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var value = valueAccessor();
            var interceptor = ko.computed({
                read: function () {
                    return !value();
                },
                write: function (newValue) {
                    value(!newValue);
                },
                disposeWhenNodeIsRemoved: element
            });

            var newValueAccessor = function () { return interceptor; };


            //keep a reference, so we can use in update function
            ko.utils.domData.set(element, "newValueAccessor", newValueAccessor);
            //call the real checked binding's init with the interceptor instead of our real observable
            ko.bindingHandlers.checked.init(element, newValueAccessor, allBindings);
        }
    };
});
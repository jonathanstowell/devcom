requirejs(["jquery", "knockout", "kovalidation"], function($, ko) {
    ko.bindingHandlers.datetime = {
        convertDateString: function(date) {
            var jsDate = new Date(date);
            return jsDate.getDate() + '/' + (jsDate.getMonth() + 1) + '/' + jsDate.getFullYear();
        },
        convertDateTimeString: function(date) {
            var ampm = "AM";
            var jsDate = new Date(date);
            var result = jsDate.getDate() + '/' + (jsDate.getMonth() + 1) + '/' + jsDate.getFullYear();

            var hours = jsDate.getHours();
            var minutes = jsDate.getMinutes();
            if (hours === 0)
                hours = 12;
            else if (hours > 12) {
                ampm = "PM";
                hours = hours - 12;
            }

            //If it is midnight, just omit the time, since it is a default date
            if (hours !== 0) {
                result = result + ' ' + (hours) + ':' + (minutes < 10 ? '0' + minutes : minutes) + ' ' + ampm;
            }

            return result;
        },
        init: function(element, valueAccessor, allBindingsAccessor, viewModel) {
            var unwrap = ko.utils.unwrapObservable;
            var dataSource = valueAccessor();
            var binding = allBindingsAccessor();
            var opt = { showTime: true };
            var source;

            if (typeof(unwrap(dataSource)) === 'undefined') {
                $(element).text('');
                return;
            }

            if (binding.options) {
                opt = $.extend(opt, binding.options);
            }

            if (dataSource) {
                var stringResult;
                if (opt.showTime)
                    stringResult = ko.bindingHandlers.datetime.convertDateTimeString(unwrap(dataSource));
                else
                    stringResult = ko.bindingHandlers.datetime.convertDateString(unwrap(dataSource));

                $(element).text(stringResult);
            } else
                $(element).text('');
        },
        update: function(element, valueAccessor, allBindingsAccessor, viewModel) {
            //update value based on a model change
            var unwrap = ko.utils.unwrapObservable;
            var dataSource = valueAccessor();
            var binding = allBindingsAccessor();
            var opt = { showTime: true };

            if (binding.dateTimeViewOptions) {
                opt = $.extend(opt, binding.options);
            }

            if (dataSource) {
                var currentModelValue = unwrap(dataSource);
                if (dataSource) {
                    var stringResult;
                    if (opt.showTime)
                        stringResult = ko.bindingHandlers.datetime.convertDateTimeString(unwrap(dataSource));
                    else
                        stringResult = ko.bindingHandlers.datetime.convertDateString(unwrap(dataSource));

                    $(element).text(stringResult);
                } else
                    $(element).text('');
            }
        }
    };
});
// Allow only numeric input
// Usage: allow-numbers="some_numeric_value"
// Default value of input-max-length is 500.
// Use ng-trim="false" in view to pass leading & trailing whitespaces to the directive,
// otherwise input field will allow leading and trailing whitespaces.

// Note: Do not use substring/substr/slice to restrain length of string.
//       It allows for values to be edited in the middle and the values at the end get pushed off.
//       For eg. MaxLength = 4; Input value = 1234; Editing 5 in the middle 
//       would change Input value to 1253, pushing 4 out.
(function () {
    cobraApp.directive("allowNumbers", function () {
        return {
            restrict: "A",
            require: "ngModel",
            link: function (scope, element, attrs, controller) {
                var holdModelValue;
                controller.$parsers.push(function (modelValue) {
                    var inputLength = parseInt(attrs.allowNumbers, 10) || 500;
                    if (modelValue == undefined) {
                        return '';
                    }
                    var newModelValue = modelValue.replace(/[^\d]/g, '');
                    if (newModelValue.length == inputLength) {
                        holdModelValue = newModelValue;
                    }
                    if (newModelValue !== modelValue || modelValue.length > inputLength) {
                        if (modelValue.length >= inputLength) {
                            newModelValue = holdModelValue;
                        }
                        controller.$setViewValue(newModelValue);
                        controller.$render();
                    }
                    return newModelValue;
                });
            }
        }
    });
})();
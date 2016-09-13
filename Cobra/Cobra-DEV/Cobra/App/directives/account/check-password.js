
app.directive('checkPassword', function () {
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, element, attrs, controller) {

            // update validator: checkPassword{{WhiteSpace | UpperCase | LowerCase | Digit | Symbol}}
            // Regulation: Must NOT contains whitespace
            // MUST contains: digits, lower-case, upper-case, special-character
            controller.$validators.checkPasswordWhiteSpace = function (modelValue) {
                // when empty, just show the "required" msg
                if (modelValue.length == 0) {
                    return true;
                }
                if (!modelValue.match(/\s/)) {
                    return true;
                }
                else {
                    return false;
                }
            };
            controller.$validators.checkPasswordUpperCase = function (modelValue) {
                if (modelValue.length == 0) {
                    return true;
                }
                if (modelValue.match(/[A-Z]/)) {
                    return true;
                }
                else {
                    return false;
                }
            };
            controller.$validators.checkPasswordLowerCase = function (modelValue) {
                if (modelValue.length == 0) {
                    return true;
                }
                if (modelValue.match(/[a-z]/)) {
                    return true;
                }
                else {
                    return false;
                }
            };
            controller.$validators.checkPasswordDigit = function (modelValue) {
                if (modelValue.length == 0) {
                    return true;
                }
                if (modelValue.match(/\d/)) {
                    return true;
                }
                else {
                    return false;
                }
            };
            controller.$validators.checkPasswordSymbol = function (modelValue) {
                if (modelValue.length == 0) {
                    return true;
                }
                //@@ used to escape AtTheRate symbol in Razor
                if (modelValue.match(/[`~!@@#\$%\^&\*\(\)\-_=\+\[\]\{\}\\\|:;'"\,\./<>\?]/)) {
                    return true;
                }
                else {
                    return false;
                }
            };
        }
    };
});


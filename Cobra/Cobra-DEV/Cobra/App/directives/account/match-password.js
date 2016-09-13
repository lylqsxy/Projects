
app.directive('matchPassword', function ($http) {
    return {
        restrict: "A",
        require: "ngModel",
        scope: {
            passwordValue: "=matchPassword"
        },
        link: function (scope, element, attrs, controller) {
            controller.$validators.matchResult = function (modelValue) {
                if (modelValue.length == 0) {
                    return true;
                }
                return modelValue == scope.passwordValue;
            };
        }
    }
    debugger;
});

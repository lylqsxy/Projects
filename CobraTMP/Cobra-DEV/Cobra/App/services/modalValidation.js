(function () {
    function formValidation() {
        return {
            require: 'ngModel',
            link: function (scope, elm, attrs, ctrl) {

                ctrl.$validators.formValidation = function (modelValue, viewValue) {
                    //未设置validation的默认值
                    if (attrs.formValidation === 'form-validation') {
                        return true;
                    }
                    var valParam = angular.fromJson(attrs.formValidation);
                    var error = false;
                    if (!error && valParam.minLen) {
                        error = modelValue.length >= valParam.minLen ? false : true;
                    }

                    if (!error && valParam.maxLen) {
                        error = modelValue.length <= valParam.maxLen ? false : true;
                    }

                    return error ? false : true;
                };
            }
        }

    }

    angular.module('cobra-app').directive("formValidation", formValidation);

})();
(function () {
    function formValidation() {
        return {
            require: 'ngModel',
            link: function (scope, elm, attrs, ctrl) {

                ctrl.$validators.formValidation = function (modelValue, viewValue) {
                    // the defalut value is form-validation when the validation is not set
                    if (attrs.formValidation === 'form-validation') {
                        return true;
                    }
                    var valParam = angular.fromJson(attrs.formValidation);

                    var modalValueLen = modelValue === null ? 0 : modelValue.toString().length;
                    var error = false;
                    if (!error && valParam.required) {

                        error = modalValueLen > 0 ? false : true;
                    }

                    if (!error && valParam.minLen) {
                        error = modalValueLen >= valParam.minLen ? false : true;
                    }

                    if (!error && valParam.maxLen) {
                        error = modalValueLen <= valParam.maxLen ? false : true;
                    }

                    return error ? false : true;
                };
            }
        }

    }

    angular.module('cobra-app').directive("formValidation", formValidation);

})();
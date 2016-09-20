var app = angular.module('myApp', ['ngRoute', 'angularValidator']);

app.service('Test', function () {
    var test = {};
    var i = 1;
    test.add = function(num){
        i += num;
    }
    test.minus = function (num) {
        i -= num;
    }
    test.get = function () {
        return i;
    }
    return test;
});

app.directive('convertToNumber', function () {
    return {
        require: 'ngModel',

        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return parseInt(val, 10);
            });
            ngModel.$formatters.push(function (val) {
                return '' + val;
            });
        }
    };
});

app.directive('dynamicName', function ($compile, $parse) {
    return {
        restrict: 'A',
        terminal: true,
        //scope: {
        //    dynamicName: '=',
        //},
        link: function (scope, elem) {
            //var name = $parse(elem.attr('dynamic-name'))(scope);
            // $interpolate() will support things like 'skill'+skill.id where parse will not
            //elem.removeAttr('dynamic-name');
            //elem.attr('name', elem.attr('static-name') + "-" + name);
            //$compile(elem)(scope);

            scope.$watch('dynamicName', function (newValue, oldValue) {
                if (newValue) {
                    //name = $parse(elem.attr('dynamic-name'))(scope);
                    //console.log(scope.dynamicName);
                    //elem.attr('name', elem.attr('static-name') + "-" + name);
                    //$compile(elem)(scope);
                }
                    
                
                    
            })
        }
    };
});

app.controller('appCtrl',
    function ($scope, $http, Test) {

        ////////////////////////
        $scope.showModal = function (response) {
            $('#commonModal').modal('show');
            if (response.IfSuccess === false)
            {
                $scope.modalTpl = {
                    modalTitle: "Login Failed",
                    modalPath: 'modalTmp',
                    modalResponse: response
                };
            }
            else
            {
                $scope.modalTpl = {
                    modalTitle: "Login Succeeded",
                    modalPath: 'modalTmp',
                    modalResponse: response
                };
            }
            
        };

        $scope.loginFn = function (login) {

            $http.post("/Account/Login/", $scope.login).success(function (response) {
                console.log(response);
                $scope.showModal(response);
            });
        };

        $scope.close = function () {
            $('#commonModal').modal('hide');
        };


        //////////////////////////
        
        $scope.data = [];
        
        $scope.data[0] = {
            name: "Nicky",
            address: "4 Rd"
        }

        $scope.data[1] = {
            name: "May",
            address: "5 Rd"
        }

        $scope.data[2] = {
            name: "Eric",
            address: "6 Rd"
        }

        $scope.data[3] = {
            name: "Craig",
            address: "7 Rd"
        }

        $scope.edit = function(i)
        {
            $('#commonModal').modal('show');
            $scope.modalTpl = {
                modalTitle: "Edit",
                modalPath: 'modalEdit',
            };
           // $scope.modal = $scope.data[i];
            $scope.modal = JSON.parse(JSON.stringify($scope.data[i]));
            $scope.index = i;
        }

        $scope.save = function (modal, i) {
            $scope.data[i] = modal;
            $scope.close();
        }

        /////////////////////////

        $scope.t = Test.get();
        

        $scope.Add = function () {
            Test.add(parseInt($scope.num, 10));
            $scope.t = Test.get();
        }

        $scope.Minus = function () {
            Test.minus(parseInt($scope.num, 10));
            $scope.t = Test.get();
        }

        //////////////////////

        $scope.test = { Id: 2, value: 'bug' }

        $scope.typeOptions = [
        { Id: 1, value: 'feature' },
        { Id: 2, value: 'bug' },
        { Id: 3, value: 'enhancement' }
        ];

        $scope.filterCondition = {
            operator: 'neq'
        }

        $scope.operators = [{
            value: 'eq',
            displayName: 'equals'
        }, {
            value: 'neq',
            displayName: 'not equal'
        }]

        ////////////////


        $scope.inputName = 'dynamicName00';
        $scope.a = 123;
        


        $scope.doStuff = function (formName) {
            console.log($scope.inputName)
            console.log(formName);
            
        }

        $scope.dd = function () {
            $scope.inputName = $scope.inputName + 'Name';
        }

        $scope.test = function (form) {
            var a = eval('form.' + $scope.inputName).$error.required;
            console.log(a)
            return a;
        }


    });

app.controller('testCtrl',
    function ($scope, $http, Test) {
         
        $scope.t = Test.get();

        $scope.Add = function () {
            Test.add(1);
            $scope.t = Test.get();
        }

        $scope.Minus = function () {
            Test.minus(1);
            $scope.t = Test.get();
        }

    })
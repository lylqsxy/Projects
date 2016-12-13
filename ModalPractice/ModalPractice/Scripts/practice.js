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

app.controller('appCtrl',
    function ($scope, $http, Test) {

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
        console.log($scope.test)

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
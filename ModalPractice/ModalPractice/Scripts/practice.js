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
        //console.log($scope.test)

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


var firstMethod = function () {
    var promise = new Promise(function (resolve, reject) {
        console.log("first method started")
        setTimeout(function () {
            console.log('first method completed');
            resolve({ data: '123' });
        }, 2000);
    });
    return promise;
};


var secondMethod = function (someStuff) {
    var promise = new Promise(function (resolve, reject) {
        console.log("second method started")
        setTimeout(function () {
            console.log('second method completed');
            resolve({ newData: someStuff.data + ' some more data' });
        }, 2000);
    });
    return promise;
};

var thirdMethod = function (someStuff) {
    var promise = new Promise(function (resolve, reject) {
        console.log("third method started")
        setTimeout(function () {
            console.log('third method completed');
            resolve({ result: someStuff.newData });
        }, 3000);
    });
    return promise;
};


//var a = firstMethod();
//console.log(a)
//var b = a.then(secondMethod);
//console.log(b)
//var c = b.then(thirdMethod);
//console.log(c)

firstMethod()
   .then(secondMethod)
   .then(thirdMethod);

app.controller("MyController2", ["$scope", "$q", function ($scope, $q) {
    $scope.flag = true;
    $scope.handle = function () {
        var deferred = $q.defer();
        var promise = deferred.promise;

        promise.then(function (result) {
            alert("Success: " + result);
        }, function (error) {
            alert("Fail: " + error);
        });

        if ($scope.flag) {
            deferred.resolve("you are lucky!");
        } else {
            deferred.reject("sorry, it lost!");
        }
    }
}]);

app.controller("MyController", ["$scope", "$q", function ($scope, $q) {
    $scope.flag = true;
    $scope.handle = function () {
        var deferred = $q.defer();
        var promise = deferred.promise;

        promise.then(function (result) {
            result = result + "you have passed the first then()";
            return result;
        }, function (error) {
            error = error + "failed but you have passed the first then()";
            return error;
        }).then(function (result) {
            alert("Success: " + result);
        }, function (error) {
            alert("Fail: " + error);
        });

        if ($scope.flag) {
            deferred.resolve("you are lucky!");
        } else {
            deferred.reject("sorry, it lost!");
        }
    }
}]);
var Fence = function (n) {
    this.test = n;
}
var newttt = { id: 1 }
Fence.prototype.test3 = newttt;


var ttt = new Fence(newttt);

var ff = new Object();

console.log(ff)

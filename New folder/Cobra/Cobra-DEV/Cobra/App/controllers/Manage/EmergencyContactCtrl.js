/*

*/

cobraApp.directive('showFocus', function ($timeout) {
    return function (scope, element, attrs) {
        scope.$watch(attrs.showFocus,
          function (newValue) {
              $timeout(function () {
                  newValue && element[0].focus();
              });
          }, true);
    };
});


cobraApp.controller('EmergencyContactCtrl', function ($scope, $http, $anchorScroll, $location, $window) {

    $scope.EmergencyContactListFn = function () {
        $http.get("/Manage/GetEmergencyContacts/").then(function (response) {
            $scope.emergencyContactList = response.data;
            $scope.form = [];
            for (i = 0; i < $scope.emergencyContactList.length; i++) {
                $scope.form[i] = {
                    showLable: [],
                    showTextBox: [],
                    focusTextBox: [],
                    showList: false
                };
                for (j = 0; j < 8; j++) {
                    $scope.form[i].showLable[j] = true;
                    $scope.form[i].showTextBox[j] = false;
                    $scope.form[i].focusTextBox[j] = false;
                }
            }
        });
    };

    $scope.EmergencyContactEditFn = function (x) {
        $http.post("/Manage/EditEmergencyContact/", x)
    }

    $scope.EmergencyContactListFn();

    $scope.AddRow = function() {
        $scope.emergencyContactList.push({});
        var i = $scope.emergencyContactList.length - 1;
        $scope.form[i] = {
            showLable: [],
            showTextBox: [],
            focusTextBox: [],
            showList: false
        };
        $scope.EditRow(i);
    }
    
    $scope.EditRow = function (index) {
        for (j = 0; j < 8; j++) {
            $scope.form[index].showLable[j] = false;
        }
        for (j = 0; j < 8; j++) {
            $scope.form[index].showTextBox[j] = true;
        }
    }

    $scope.SaveRow = function (x, index) {
        for (j = 0; j < 8; j++) {
            $scope.form[index].showLable[j] = true;
        }
        for (j = 0; j < 8; j++) {
            $scope.form[index].showTextBox[j] = false;
        }
        $scope.EmergencyContactEditFn(x);
    }

    $scope.PhoneList = function (index) {
        var s = "collapseMain" + index;
        $location.hash(s);
        $anchorScroll();
        $scope.form[index].showList = !$scope.form[index].showList;

    }

    $scope.LableOnClick = function (i, index) {
        $scope.form[index].showLable[i] = false;
        $scope.form[index].showTextBox[i] = true;
        $scope.form[index].focusTextBox[i] = true;
        
    }    

    $scope.LableOnUnfocus = function (x, i, index) {
        $scope.form[index].showLable[i] = true;
        $scope.form[index].showTextBox[i] = false;
        $scope.form[index].focusTextBox[i] = false;
        $scope.EmergencyContactEditFn(x);
    }


})
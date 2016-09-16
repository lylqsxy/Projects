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

    $scope.emergencyContactList = [];
    $scope.form = [];
    var newRow = {
        Id: 0

    };
    var dataTMP = {};
    var editMode = false;

    $scope.EmergencyContactListFn = function () {
        $http.get("/Manage/GetEmergencyContacts/").then(function (response) {
            $scope.emergencyContactList = response.data;
            
            for (var i = 0; i < $scope.emergencyContactList.length; i++) {
                $scope.form[i] = {
                    showLable: new Array(6),
                    showTextBox: new Array(6),
                    focusTextBox: new Array(6),
                    showList: false,
                    phoneForm: new Array($scope.emergencyContactList[i].PhoneList.length)
                };
                for (var j = 0; j < $scope.emergencyContactList[i].PhoneList.length; j++) {
                    $scope.form[i].phoneForm[j] = {
                        showLable: new Array(5),
                        showTextBox: new Array(5),
                        focusTextBox: new Array(5)
                    };
                    ToggleShow(i, -1, j);
                }
                ToggleShow(i, -1);
            }

        });
    };

    $scope.EmergencyContactListFn();

    $scope.AddRow = function () {
        $scope.emergencyContactList.push({
            Id: 0, "Firstname": "Nickyqq", "Middlename": null, "Lastname": "Li", "RelationshipID": 1,
            "Priority": 1, "Reason": "222", "PhoneList": [{
                "Id": 1, "Number": "02193848384", "PhoneTypeID": 1,
                "IsMobile": true, "IsPrimary": true, "CountryID": 1
            }]
        });
        var i = $scope.emergencyContactList.length - 1;
        $scope.form[i] = {
            showLable: [],
            showTextBox: [],
            focusTextBox: [],
            showList: false
        };

    };

    $scope.EditRow = function (index, phoneIndex) {
        if (!editMode) {
            editMode = true;       
            if (arguments.length === 1) {
                dataTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[index]));
                ToggleEdit(index, -1);
            }
            else if (arguments.length === 2) {
                dataTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[index].PhoneList[phoneIndex]));
                ToggleEdit(index, -1, phoneIndex);
            }
        }
    };

    $scope.SaveRow = function (index, form, phoneIndex) {
        if (!form.$invalid) {
            var x = $scope.emergencyContactList[index];
            DataPost(x);
            if (arguments.length === 2) {
                ToggleShow(index, -1);
            }
            else if (arguments.length === 3) {
                ToggleShow(index, -1, phoneIndex);
            }
            editMode = false;
        }
    };

    $scope.CancelRow = function (index, phoneIndex) {
        $scope.emergencyContactList[index] = JSON.parse(JSON.stringify(dataTMP));
        if (arguments.length === 1) {
            ToggleShow(index, -1);
        }
        else if (arguments.length === 2) {
            ToggleShow(index, -1, phoneIndex);
        }
        editMode = false;
    };

    var DataPost = function (x) {

        if (x.Id > 0) {      // update
            $http({
                method: "Post",
                url: "/Manage/EditEmergencyContact",
                data: x,
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }).then(function SentOk(result) {
                console.log(result);
            }, function Error(result) {
                console.log(result);
            });
        }
        else {          // create
            $http({
                method: "Post",
                url: "/Manage/CreateEmergencyContact",
                data: x,
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }).then(function SentOk(result) {
                //$scope.data[index].Id = result.data.Id;
                console.log(result);
            }, function Error(result) {
                console.log(result);
            });
        }
    };

    $scope.PhoneList = function (index) {
        //var s = "collapseMain" + index;
        //$location.hash(s);
        //$anchorScroll();
        $scope.form[index].showList = !$scope.form[index].showList;

    };

    $scope.LableOnClick = function (i, index, phoneIndex) {
        if (!editMode) {
            if (arguments.length === 2) {
                ToggleEdit(index, i);
            }
            else if (arguments.length === 3) {
                ToggleEdit(index, i, phoneIndex);
            }
        }
    };

    $scope.LableOnUnfocus = function (i, index, form, phoneIndex) {
        if (!editMode) {
            if (!form.$invalid) {
                var x = $scope.emergencyContactList[index];
                DataPost(x);
                if (arguments.length === 3) {
                    ToggleShow(index, i);
                }
                else if (arguments.length === 4) {
                    ToggleShow(index, i, phoneIndex);
                }
            }
        }
    };

    var ToggleShow = function (index, i, phoneIndex) {

        if (arguments.length === 3) {
            if (i === -1) {
                for (var j = 0; j < $scope.form[index].phoneForm[phoneIndex].showLable.length; j++) {
                    $scope.form[index].phoneForm[phoneIndex].showLable[j] = true;
                    $scope.form[index].phoneForm[phoneIndex].showTextBox[j] = false;
                    $scope.form[index].phoneForm[phoneIndex].focusTextBox[j] = false;
                    $scope.form[index].phoneForm[phoneIndex].disableCancelBtn = false;
                }
            }
            else {
                $scope.form[index].phoneForm[phoneIndex].showLable[i] = true;
                $scope.form[index].phoneForm[phoneIndex].showTextBox[i] = false;
                $scope.form[index].phoneForm[phoneIndex].focusTextBox[i] = false;
                $scope.form[index].phoneForm[phoneIndex].disableCancelBtn = false;
            }
            $scope.form[index].phoneForm[phoneIndex].showSaveBtn = false;
            $scope.form[index].phoneForm[phoneIndex].showCancelBtn = false;
            $scope.form[index].phoneForm[phoneIndex].showEditBtn = true;
            $scope.form[index].phoneForm[phoneIndex].showDelBtn = true;
        }
        else if (arguments.length === 2) {
            if (i === -1) {
                for (var j = 0; j < $scope.form[index].showLable.length; j++) {
                    $scope.form[index].showLable[j] = true;
                    $scope.form[index].showTextBox[j] = false;
                    $scope.form[index].focusTextBox[j] = false;
                    $scope.form[index].disableCancelBtn = false;
                }
            }
            else {
                $scope.form[index].showLable[i] = true;
                $scope.form[index].showTextBox[i] = false;
                $scope.form[index].focusTextBox[i] = false;
                $scope.form[index].disableCancelBtn = false;
            }
            $scope.form[index].showSaveBtn = false;
            $scope.form[index].showCancelBtn = false;
            $scope.form[index].showEditBtn = true;
            $scope.form[index].showDelBtn = true;
        }
    };

    var ToggleEdit = function (index, i, phoneIndex) {
        if (arguments.length === 3) {
            if (i === -1) {
                for (var j = 0; j < $scope.form[index].phoneForm[phoneIndex].showLable.length; j++) {
                    $scope.form[index].phoneForm[phoneIndex].showLable[j] = false;
                    $scope.form[index].phoneForm[phoneIndex].showTextBox[j] = true;
                    $scope.form[index].phoneForm[phoneIndex].disableCancelBtn = false;
                }
            }
            else {
                $scope.form[index].phoneForm[phoneIndex].showLable[i] = false;
                $scope.form[index].phoneForm[phoneIndex].showTextBox[i] = true;
                $scope.form[index].phoneForm[phoneIndex].focusTextBox[i] = true;
                $scope.form[index].phoneForm[phoneIndex].disableCancelBtn = true;
            }
            $scope.form[index].phoneForm[phoneIndex].showSaveBtn = true;
            $scope.form[index].phoneForm[phoneIndex].showCancelBtn = true;
            $scope.form[index].phoneForm[phoneIndex].showEditBtn = false;
            $scope.form[index].phoneForm[phoneIndex].showDelBtn = false;
        }

        else if (arguments.length === 2) {
            if (i === -1) {
                for (var j = 0; j < $scope.form[index].showLable.length; j++) {
                    $scope.form[index].showLable[j] = false;
                    $scope.form[index].showTextBox[j] = true;
                    $scope.form[index].disableCancelBtn = false;
                }
            }
            else {
                $scope.form[index].showLable[i] = false;
                $scope.form[index].showTextBox[i] = true;
                $scope.form[index].focusTextBox[i] = true;
                $scope.form[index].disableCancelBtn = true;
            }
            $scope.form[index].showSaveBtn = true;
            $scope.form[index].showCancelBtn = true;
            $scope.form[index].showEditBtn = false;
            $scope.form[index].showDelBtn = false;
        }

    };
});
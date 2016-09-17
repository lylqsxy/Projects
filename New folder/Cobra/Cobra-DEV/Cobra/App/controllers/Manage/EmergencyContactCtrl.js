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
    var dataTMP = {};
    var dataPhoneTMP = {};
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
                if ($scope.emergencyContactList[i].PhoneList.length === 1) {
                    $scope.form[i].phoneForm[0].disableDelBtn = true;
                }
                else {
                    $scope.form[i].phoneForm[0].disableDelBtn = false;
                }
                ToggleShow(i, -1);
            }

        });
    };

    $scope.EmergencyContactListFn();

    $scope.AddRow = function (index) {
        if (!editMode) {
            editMode = true;
            var newIndex
            if (arguments.length === 0) {
                $scope.emergencyContactList.push({ Id: 0 });
                newIndex = $scope.emergencyContactList.length - 1;
                $scope.emergencyContactList[newIndex].PhoneList = [];
                $scope.form[newIndex] = {
                    showLable: new Array(6),
                    showTextBox: new Array(6),
                    focusTextBox: new Array(6),
                    showList: true,
                    phoneForm: new Array(1)
                };
                
                $scope.form[newIndex].phoneForm[0] = {
                    showLable: new Array(5),
                    showTextBox: new Array(5),
                    focusTextBox: new Array(5)
                };
                dataTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[newIndex]));
                ToggleEdit(newIndex, -1);
                $scope.form[newIndex].disableSaveBtn = true;
                $scope.form[newIndex].disableCancelBtn = true;
                $scope.form[newIndex].disableAddBtn = true;
                $scope.form[newIndex].disablePhoneBtn = true;
            }
            else if (arguments.length === 1) {
                newIndex = index;
                
            }

            $scope.emergencyContactList[newIndex].PhoneList.push({ Id: 0 });
            var newPhoneIndex = $scope.emergencyContactList[newIndex].PhoneList.length - 1;
            $scope.form[newIndex].phoneForm[newPhoneIndex] = {
                showLable: new Array(5),
                showTextBox: new Array(5),
                focusTextBox: new Array(5)
            };
            dataPhoneTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[newIndex].PhoneList[newPhoneIndex]));
            ToggleEdit(newIndex, -1, newPhoneIndex);            
        }
    };

    $scope.EditRow = function (index, phoneIndex) {
        if (!editMode) {
            editMode = true;       
            if (arguments.length === 1) {
                dataTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[index]));
                ToggleEdit(index, -1);
            }
            else if (arguments.length === 2) {
                dataPhoneTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[index].PhoneList[phoneIndex]));
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
                ToggleShow(index, -1);
                ToggleShow(index, -1, phoneIndex);
                $scope.form[index].disableSaveBtn = false;
                $scope.form[index].disableCancelBtn = false;
                $scope.form[index].disableAddBtn = false;
                $scope.form[index].disablePhoneBtn = false;
                if ($scope.emergencyContactList[index].PhoneList.length === 1) {
                    $scope.form[index].phoneForm[0].disableDelBtn = true;
                }
                else {
                    $scope.form[index].phoneForm[0].disableDelBtn = false;
                }
            }
            editMode = false;  
        }
    };

    $scope.DelRow = function (index, phoneIndex) {
        if (!editMode) {        
            if (arguments.length === 1) {
                if (confirm("All the phone data will be deleted!")) {
                    var x = $scope.emergencyContactList[index];
                    DataDelPost(x);
                    $scope.emergencyContactList.splice(index, 1);
                }    
            }
            else if (arguments.length === 2) {
                if (confirm("Are you sure?")) {
                    var x = {
                        id: $scope.emergencyContactList[index].PhoneList[phoneIndex].Id
                    }
                    PhoneDataDelPost(x);
                    $scope.emergencyContactList[index].PhoneList.splice(phoneIndex, 1);
                    if ($scope.emergencyContactList[index].PhoneList.length === 1) {
                        $scope.form[index].phoneForm[0].disableDelBtn = true;
                    }
                    else {
                        $scope.form[index].phoneForm[0].disableDelBtn = false;
                    }
                }
            }
        }
    };

    $scope.CancelRow = function (index, phoneIndex) {
        if (arguments.length === 1) {
            $scope.emergencyContactList[index] = JSON.parse(JSON.stringify(dataTMP));
            ToggleShow(index, -1);
        }
        else if (arguments.length === 2) {
            $scope.emergencyContactList[index].PhoneList[phoneIndex] = JSON.parse(JSON.stringify(dataPhoneTMP));
            if ($scope.emergencyContactList[index].PhoneList[phoneIndex].Id === 0) {
                $scope.emergencyContactList[index].PhoneList.pop();              
                if ($scope.emergencyContactList[index].Id === 0) {
                    $scope.emergencyContactList.pop();
                }
            }
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

    var DataDelPost = function (x) {
            // delete
        $http({
            method: "Post",
            url: "/Manage/DeleteEmergencyContact",
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

    var PhoneDataDelPost = function (x) {
        // delete
        $http({
            method: "Post",
            url: "/Manage/DeletePhone",
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
                }
                $scope.form[index].phoneForm[phoneIndex].disableCancelBtn = false;
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
                for (var k = 0; k < $scope.form[index].showLable.length; k++) {
                    $scope.form[index].showLable[k] = true;
                    $scope.form[index].showTextBox[k] = false;
                    $scope.form[index].focusTextBox[k] = false;
                }
                $scope.form[index].disableCancelBtn = false;
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
                }
                $scope.form[index].phoneForm[phoneIndex].disableCancelBtn = false;
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
                for (var k = 0; k < $scope.form[index].showLable.length; k++) {
                    $scope.form[index].showLable[k] = false;
                    $scope.form[index].showTextBox[k] = true;
                }
                $scope.form[index].disableCancelBtn = false;
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
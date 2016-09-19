/* Author: Nicky

*/

cobraApp.service('arrayService', function ($filter) {
    var arrayService = {};
    arrayService.PriorityListGenerator = function (inputArray) {
        var max = Math.max.apply(Math, inputArray);
        var array = [];
        for (var i = 1; i < max+5; i++) {
            if ($filter('filter')(inputArray, i).length < 1) {
                array.push(i);
            }
        }
        return array;
    };

    arrayService.ArrayAdd = function (array, i) {
        var index = array.indexOf(i);
        if (index <= -1) {
            array.push(i);
        }
        array = array.sort(function (a, b) { return a - b; });
    };

    arrayService.ArrayRemove = function (array, i) {
        var index = array.indexOf(i);
        if (index > -1) {
            array.splice(index, 1);
        }
        array = array.sort(function (a, b) { return a - b; });
    };
    return arrayService;
});

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

cobraApp.directive('convertToNumber', function () {
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

cobraApp.controller('EmergencyContactCtrl', function ($scope, $http, $filter, $anchorScroll, $location, $window, arrayService) {

    var dataTMP;
    var dataPhoneTMP;
    var editMode;
    var cellLock;

    var init = function () {
        $scope.emergencyContactList = [];
        $scope.form = [];
        $scope.priorityList = [];
        dataTMP = {};
        dataPhoneTMP = {};
        editMode = false;
        cellLock = false;
    };
    
    var EmergencyContactListFn = function () {
        init();
        $http.get("/Manage/GetEmergencyContacts/").then(function (response) {
            $scope.emergencyContactList = response.data;
            var priorityArray = [];
            for (var i = 0; i < $scope.emergencyContactList.length; i++) {
                $scope.form[i] = {
                    showLable: new Array(6),
                    showTextBox: new Array(6),
                    focusTextBox: new Array(6),
                    showList: false,
                    phoneBtn: "Phone >>",
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
                else if ($scope.emergencyContactList[i].PhoneList.length > 1) {
                    $scope.form[i].phoneForm[0].disableDelBtn = false;
                }
                ToggleShow(i, -1);
                priorityArray.push($scope.emergencyContactList[i].Priority);
            }
            $scope.priorityList = arrayService.PriorityListGenerator(priorityArray);
        });
    };

    EmergencyContactListFn();

    var EmergencyContactAttributesListFn = function () {
        $http.get("/Manage/GetEmergencyContactAttributes/").then(function (response) {
            $scope.ecaList = response.data;

        });
    };

    EmergencyContactAttributesListFn();

    $scope.AddRow = function (index) {
        if (!editMode) {
            editMode = true;
            var newIndex;
            if (arguments.length === 0) {
                $scope.emergencyContactList.push({
                    Id: "New",
                    RelationshipID: "",
                    Priority: $scope.priorityList[0]
                });
                newIndex = $scope.emergencyContactList.length - 1;
                $scope.emergencyContactList[newIndex].PhoneList = [];
                $scope.form[newIndex] = {
                    showLable: new Array(6),
                    showTextBox: new Array(6),
                    focusTextBox: new Array(6),
                    showList: true,
                    phoneBtn: "Phone <<",
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

            $scope.emergencyContactList[newIndex].PhoneList.push({
                Id: "New",
                PhoneTypeID: "",
                IsMobile: false,
                IsPrimary: false,
                CountryID: "a"
            });
            var newPhoneIndex = $scope.emergencyContactList[newIndex].PhoneList.length - 1;
            $scope.form[newIndex].phoneForm[newPhoneIndex] = {
                showLable: new Array(5),
                showTextBox: new Array(5),
                focusTextBox: new Array(5)
            };
            dataPhoneTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[newIndex].PhoneList[newPhoneIndex]));
            ToggleEdit(newIndex, -1, newPhoneIndex);

            var s = "form" + newIndex + "phoneForm" + newPhoneIndex;
            $location.hash(s);
            $anchorScroll();
            
        }
    };

    $scope.EditRow = function (index, phoneIndex) {
        if (!editMode) {
            editMode = true;
            dataTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[index]));
            if (arguments.length === 1) {
                ToggleEdit(index, -1);
            }
            else if (arguments.length === 2) {
                dataPhoneTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[index].PhoneList[phoneIndex]));
                ToggleEdit(index, -1, phoneIndex);
            }
        }
    };

    $scope.SaveRow = function (index, form, phoneIndex) {
        setFormDirty(index, form, phoneIndex);
        if (!form.$invalid) {
            var x = $scope.emergencyContactList[index];
            DataPost(x, index, phoneIndex);
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
            cellLock = false;
        }
    };

    $scope.DelRow = function (index, phoneIndex) {
        if (!editMode) {
            if (arguments.length === 1) {
                if (confirm("All the phone data will be deleted!")) {
                    var x = $scope.emergencyContactList[index];
                    DataDelPost(x, index, phoneIndex);
                    $scope.emergencyContactList.splice(index, 1);
                }
            }
            else if (arguments.length === 2) {
                if (confirm("Are you sure?")) {
                    var y = {
                        id: $scope.emergencyContactList[index].PhoneList[phoneIndex].Id
                    };
                    PhoneDataDelPost(y, index, phoneIndex);
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
            if ($scope.emergencyContactList[index].PhoneList[phoneIndex].Id === "New") {
                $scope.emergencyContactList[index].PhoneList.pop();
                if ($scope.emergencyContactList[index].Id === "New") {
                    $scope.emergencyContactList.pop();
                }
                var s = "collapseMain" + index;
                $location.hash(s);
                $anchorScroll();
            }
            ToggleShow(index, -1, phoneIndex);
        }
        editMode = false;

    };

    var DataPost = function (x, index, phoneIndex) {
        if (x.Id !== "New") {      // update
            $http({
                method: "Post",
                url: "/Manage/EditEmergencyContact",
                data: x,
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }).then(function SentOk(result) {
                updateId(result.data.contactToUpdate, index, phoneIndex);
                console.log(result);
            }, function Error(result) {
                if (result.status !== 200) {
                    window.alert(result.status + " Error");
                    EmergencyContactListFn();
                }
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
                updateId(result.data.newEmergencyContact, index, phoneIndex);
                console.log(result);
            }, function Error(result) {
                if (result.status !== 200) {
                    window.alert(result.status + " Error");
                    EmergencyContactListFn();
                }
                console.log(result);
            });
        }
    };

    var DataDelPost = function (x,index, phoneIndex) {
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
            if (result.status !== 200) {
                window.alert(result.status + " Error");
                EmergencyContactListFn();
            }
            console.log(result);
        });
    };

    var PhoneDataDelPost = function (x, index, phoneIndex) {
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
            if (result.status !== 200) {
                window.alert(result.status + " Error");
                EmergencyContactListFn();
            }
            console.log(result);
        });
    };

    var updateId = function (resultData, index, phoneIndex) {
        $scope.emergencyContactList[index].Id = resultData.Id;
        if (phoneIndex !== undefined) {
            $scope.emergencyContactList[index].PhoneList[phoneIndex].Id = resultData.PhoneList[phoneIndex].Id;
        }    
    };

    $scope.PhoneList = function (index) {
        if (!$scope.form[index].showList) {
            var s = "collapseMain" + index;
            $location.hash(s);
            $anchorScroll();
            $scope.form[index].showList = true;
            $scope.form[index].phoneBtn = "Phone <<";
        }
        else {
            var ss = "collapseMain" + index;
            $location.hash(ss);
            $anchorScroll();
            $scope.form[index].showList = false;
            $scope.form[index].phoneBtn = "Phone >>";
        }
        

    };

    $scope.LableOnClick = function (i, index, phoneIndex) {
        if (!editMode && !cellLock) {
            dataTMP = JSON.parse(JSON.stringify($scope.emergencyContactList[index]));
            if (arguments.length === 2) {
                ToggleEdit(index, i);
            }
            else if (arguments.length === 3) {
                ToggleEdit(index, i, phoneIndex);
            }
            cellLock = true;
        }
    };

    $scope.LableOnUnfocus = function (i, index, form, phoneIndex) {
        if (!editMode) {
            if (!form.$invalid) {
                var x = $scope.emergencyContactList[index];
                DataPost(x, index, phoneIndex);
                if (arguments.length === 3) {
                    ToggleShow(index, i);
                }
                else if (arguments.length === 4) {
                    ToggleShow(index, i, phoneIndex);
                }
                cellLock = false;
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
            arrayService.ArrayRemove($scope.priorityList, $scope.emergencyContactList[index].Priority);
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
            arrayService.ArrayAdd($scope.priorityList, $scope.emergencyContactList[index].Priority);
        }
    };

    var setFormDirty = function (index, form, phoneIndex) {
        for (var k = 0; k < 6; k++) {
            eval("form.textBox" + k.toString()).$setDirty();
        }
        if (phoneIndex !== undefined) {
            for (var j = 6; j < 11; j++) {
                eval("form.textBox" + j.toString() + phoneIndex.toString()).$setDirty();
            }
        }
        
    };

});


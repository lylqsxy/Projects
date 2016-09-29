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

cobraApp.controller('EmergencyContactCtrl', function ($scope, $http, $filter, $anchorScroll, $location, $window, arrayService) {

    var dataTMP;
    var dataPhoneTMP;
    var editMode;
    var cellLock;
    var relationshipList = [];
    var phoneTypesList = [];
    var phoneCodeList = [];
    var newContact = {
        "Id": "New",
        "Firstname": null,
        "Middlename": null,
        "Lastname": null,
        "RelationshipID": null,
        "Priority": null,
        "Reason": null,
        "PhoneList": null
    };
    var newPhone = {
        "Id": "New",
        "Number": null,
        "PhoneTypeID": null,
        "IsMobile": false,
        "IsPrimary": false,
        "CountryID": null
    };
    var editedIndex;
    var editedPhoneIndex;
    var editingPhone;
    var newIndex;
    var newPhoneIndex;

    var init = function () {
        $scope.emergencyContactList = [];
        $scope.form = [];
        dataTMP = {};
        dataPhoneTMP = {};
        editMode = false;
        cellLock = false;
    };
    
    var EmergencyContactListFn = function () {
        init();
        $http.get("/Manage/GetEmergencyContacts/").then(function (response) {
            $scope.emergencyContactList = response.data;
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
            }
        });
    };

    EmergencyContactListFn();

    var EmergencyContactAttributesListFn = function () {
        $http.get("/Manage/GetEmergencyContactAttributes/").then(function (response) {
            $scope.ecaList = response.data;
            
            for (var item in $scope.ecaList.Relationships) {
                var relationship = {
                    value: $scope.ecaList.Relationships[item].ID,
                    text: $scope.ecaList.Relationships[item].Relationship
                };
                relationshipList.push(relationship);    
            }

            for (var item2 in $scope.ecaList.PhoneTypes) {
                var phoneTypes = {
                    value: $scope.ecaList.PhoneTypes[item2].ID,
                    text: $scope.ecaList.PhoneTypes[item2].PhoneType
                };
                phoneTypesList.push(phoneTypes);
            }

            for (var item3 in $scope.ecaList.PhoneCountries) {
                var phoneCode = {
                    value: $scope.ecaList.PhoneCountries[item3].ID,
                    text: $scope.ecaList.PhoneCountries[item3].Name + " | " + $scope.ecaList.PhoneCountries[item3].PhoneCode
                };
                phoneCodeList.push(phoneCode);
            }
        });
    };

    EmergencyContactAttributesListFn();

    $scope.AddRow = function () {
        newIndex = true;
        newPhoneIndex = false;
        editedIndex = 0;
        editingPhone = false;
        showModal(newContact);
    };

    $scope.AddPhoneRow = function (index) {
        newIndex = false;
        newPhoneIndex = true;
        editedIndex = index;
        editingPhone = true;
        showPhoneModal(newPhone);
    };

    $scope.EditRow = function (index) {
        newIndex = false;
        newPhoneIndex = false;
        editedIndex = index;
        editingPhone = false;
        showModal($scope.emergencyContactList[index]);        
    };

    $scope.EditPhoneRow = function (index, phoneIndex) {
        newIndex = false;
        newPhoneIndex = false;
        editedIndex = index;
        editedPhoneIndex = phoneIndex;
        editingPhone = true;
        showPhoneModal($scope.emergencyContactList[index].PhoneList[phoneIndex]);
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
        var msgboxData = {
            title: 'Delete Confirmation',
            data: {
                index: index,
                phoneIndex: phoneIndex
            }
        };
        if (phoneIndex === undefined) {
            msgboxData.msg = 'All the phone data will be deleted! Id: ' + $scope.emergencyContactList[index].Id;
        }
        else {
            msgboxData.msg = 'One phone data will be deleted! Id: ' + $scope.emergencyContactList[index].Id +
                ', PhoneId: ' + $scope.emergencyContactList[index].PhoneList[phoneIndex].Id;
        } 
        $scope.$broadcast('showMsgBox', msgboxData);
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
                updateRow(result.data.contactToUpdate, index);
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
                updateRow(result.data.newEmergencyContact, -1);
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

    var updateRow = function (resultData, index) {
        if (index !== -1) {
            $scope.emergencyContactList[index] = resultData;
        }
        else {
            $scope.emergencyContactList.push(resultData);
            $scope.form.push({
                showList: false,
                phoneBtn: "Phone >>"
            });
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

    $scope.phoneListVal = function (phoneIndex, form, textBoxIndex, attr) {
        if (attr === "required") {
            return a = eval("form.textBox" + textBoxIndex + "At" + phoneIndex).$error.required &&
                    eval("form.textBox" + textBoxIndex + "At" + phoneIndex).$dirty;
        }
        else if (attr === "pattern") {
            return a = eval("form.textBox" + textBoxIndex + "At" + phoneIndex).$error.pattern;
        }
    };
            
    var showModal = function (x) {
        var dataToModal = [
            {
                title: 'First Name',
                variableName: 'Firstname',
                value: x.Firstname ? x.Firstname : "",
                type: 'text',
                validation: {
                    required: true,
                    errorText: '* Required'
                },
                regExpVaid: {
                    text: 'Incorrect Format',
                    reg: /^[a-zA-Z]*$/
                }
            },

            {
                title: 'Middle Name',
                variableName: 'Middlename',
                value: x.Middlename ? x.Middlename : "",
                type: 'text',
                regExpVaid: {
                    text: 'Incorrect Format',
                    reg: /^[a-zA-Z]*$/
                }
            },

            {
                title: 'Last Name',
                variableName: 'Lastname',
                value: x.Lastname ? x.Lastname : "",
                type: 'text',
                validation: {
                    required: true,
                    errorText: '* Required'
                },
                regExpVaid: {
                    text: 'Incorrect Format',
                    reg: /^[a-zA-Z]*$/
                }
            },

            {
                title: 'Relationship',
                variableName: 'RelationshipID',
                value: x.RelationshipID ? x.RelationshipID : relationshipList[0].value,
                type: 'select',
                selectEnum: relationshipList
            },

            {
                title: 'Priority',
                variableName: 'Priority',
                value: x.Priority ? x.Priority : arrayListGenerator(x)[0].value,
                type: 'select',
                selectEnum: arrayListGenerator(x)
            },

            {
                title: 'Reason',
                variableName: 'Reason',
                value: x.Reason ? x.Reason : "",
                type: 'text'
            }

            //title: Title for input, variableName: dababase filed name  , type: input type 
            //value: setting the initial value if you edit a entity by modal
            //validation: minLen:2,maxLen:20,required: true
            //regulation expression vaidation, regExpVaid: { text: 'Email format is not correct', reg: /^\s*\w*\s*$/ }, see the example
        ];
        
        var modalOption = {
            modalTitle: newIndex === true? 'Add Contact' : 'Edit Contact',  // Modal tilte
            controller: 'Manage', //Contorll name 
            action: 'EditEmergencyContact', //Action Name (Post)
            idVariable: 'Id', // ID of a table
            idvalue: x.Id, //nuallable, Route domain/controller/action/idValue
            httpPostConfig: {
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }
        };

        $scope.$broadcast('showModelEvent', [dataToModal, modalOption]);
    };
    
    var showPhoneModal = function (x) {
        
        var dataToModal = [
            {
                title: 'Number',
                variableName: 'Number',
                value: x.Number ? x.Number : "",
                type: 'text',
                validation: {
                    required: true,
                    errorText: '* Required'
                },
                regExpVaid: {
                    text: 'Incorrect Format',
                    reg: /^[0-9()-+]+$/
                }
            },

            {
                title: 'Phone Type',
                variableName: 'PhoneTypeID',
                value: x.PhoneTypeID ? x.PhoneTypeID : phoneTypesList[0].value,
                type: 'select',
                selectEnum: phoneTypesList
            },

            {
                title: 'IsMobile',
                variableName: 'IsMobile',
                value: x.IsMobile ? x.IsMobile : false,
                type: 'checkbox',
                etitle: 'Mobile Phone'
            },

            {
                title: 'IsPrimary',
                variableName: 'IsPrimary',
                value: x.IsPrimary ? x.IsPrimary : false,
                type: 'checkbox',
                etitle: 'Primary Phone'
            },

            {
                title: 'Country',
                variableName: 'CountryID',
                value: x.CountryID ? x.CountryID : "",
                type: 'select',
                selectEnum: phoneCodeList
            }

            //title: Title for input, variableName: dababase filed name  , type: input type 
            //value: setting the initial value if you edit a entity by modal
            //validation: minLen:2,maxLen:20,required: true
            //regulation expression vaidation, regExpVaid: { text: 'Email format is not correct', reg: /^\s*\w*\s*$/ }, see the example
        ];

        var modalOption = {
            modalTitle: newPhoneIndex === true ? 'Add Phone' : 'Edit Phone',  // Modal tilte
            controller: '', //Contorll name 
            action: '', //Action Name (Post)
            idVariable: '', // ID of a table
            idvalue: '' //nuallable, Route domain/controller/action/idValue
        };

        $scope.$broadcast('showModelEvent', [dataToModal, modalOption]);
    };

    $scope.$on('modelDone', function (event, data) {
        if (data) {
            if (editingPhone === false) {
                if (!newIndex) {
                    data.PhoneList = $scope.emergencyContactList[editedIndex].PhoneList;
                    data.Id = $scope.emergencyContactList[editedIndex].Id;
                }
                else {
                    data.Id = "New";
                }
                
                DataPost(data, editedIndex);     
            }
            else if (editingPhone === true) {
                var tmp = $scope.emergencyContactList[editedIndex];
                if (!newPhoneIndex) {
                    data.Id = $scope.emergencyContactList[editedIndex].PhoneList[editedPhoneIndex].Id;
                    tmp.PhoneList[editedPhoneIndex] = data;
                }
                else {
                    data.Id = "New";
                    tmp.PhoneList.push(data);
                }              
                DataPost(tmp, editedIndex);
            }
        }
        else {
            console.log(data);
        }
    });

    var arrayListGenerator = function (x) {
        var priorityArray = [];
        for (var i = 0; i < $scope.emergencyContactList.length; i++) {
            priorityArray.push($scope.emergencyContactList[i].Priority);
        }
        var priorityList = arrayService.PriorityListGenerator(priorityArray);
        if (!newIndex) {
            arrayService.ArrayAdd(priorityList, x.Priority);
        }
        var pList = [];
        for (var item in priorityList) {
            var p = {
                value: priorityList[item],
                text: priorityList[item]
            };
            pList.push(p);
        }
        return pList;
    };

    $scope.$on('msgboxDone', function (event, data) {
        if (data[1]) {
            if (data[0].data.phoneIndex === undefined) {
                var x = $scope.emergencyContactList[data[0].data.index];
                DataDelPost(x, data[0].data.index, data[0].data.phoneIndex);
                $scope.emergencyContactList.splice(data[0].data.index, 1);
            }
            else {
                var y = {
                    id: $scope.emergencyContactList[data[0].data.index].PhoneList[data[0].data.phoneIndex].Id
                };
                PhoneDataDelPost(y, data[0].data.index, data[0].data.phoneIndex);
                $scope.emergencyContactList[data[0].data.index].PhoneList.splice(data[0].data.phoneIndex, 1);
            }
        }
        $('#msgbox').modal('hide');
    });

});




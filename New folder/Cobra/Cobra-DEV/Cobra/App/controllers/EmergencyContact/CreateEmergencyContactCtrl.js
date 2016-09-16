(function (angular) {
    'use strict';

    cobraApp.controller('CreateECCtrl', function ($scope, $http, $window) {

        initialise();

        $scope.relationships = []; 
        $scope.selectedRelationship = {};

        //predefined options 
        $scope.primaryOptions = [{IsPrimary: true, Text:"Yes"}, {IsPrimary: false, Text:"No"}]; 
        $scope.isMobileOptions = [{ IsMobile: true, Text: "Yes" }, { IsMobile: false, Text: "No" }];
        $scope.phoneTypeOptions = [];
        $scope.phoneCountryOptions = [];

        //selector models
        $scope.selectedPhoneType = {};
        $scope.selectedIsPrimary = {};
        $scope.selectedIsMobile = {};

        //phone section 
        $scope.phoneViewModels = [];
      
        function initialise() {
            //Get predefine attributes 
            $http({
                method: "GET",
                url: "/Manage/GetEmergencyContactAttributes"
            }).then(function success(response) {
                $scope.phoneTypeOptions = response.data.PhoneTypes;
                $scope.relationships = response.data.Relationships;
                $scope.phoneCountryOptions = response.data.PhoneCountries;
                console.log(response.data);
            }, function error(response) {
        });

        $scope.newContact = {
            Priority: 1,
            Firstname: "", 
            Middlename: "",
            Lastname: "",
            RelationshipID: 0, //default 2
            Reason: "accident",
            //PhoneList: [{ Number: "09 210222new", PhoneTypeID: 3, IsPrimary: false, IsMobile: false },
            //          { Number: "0210222new", PhoneTypeID: 3, IsPrimary: false, IsMobile: true }]
        };

         
        }
        
        $scope.addPhone = function () {
            //alert('addPhone called');
            var newPhone = { Number: "", PhoneTypeID: 0, CountryID: 0, IsMobile: false, IsPrimary: false };

            if ($scope.phoneViewModels.length === 0) {
                //default at least one primary phone number 
                newPhone.IsPrimary = true;
            }
            $scope.phoneViewModels.push(newPhone);
            console.log($scope.phoneViewModels);      
        };
        
        //for toogle IsPrimary phone option 
        var checkedPrimaryPhones = []; 
        $scope.tooglePrimaryPhone = function (checkedPhone) {
            if (checkedPrimaryPhones.length < 2) {
                //nothing else is being checked as the moment, so this is primary
                checkedPhone.IsPrimary = true;
            }
            
            checkedPhone.IsPrimary = true;
            
        };

        //Send data to server
        $scope.add = function () {
            $scope.newContact.RelationshipID = $scope.selectedRelationship.ID;
            $scope.newContact.PhoneList = $scope.phoneViewModels;

            $http.post('/Manage/CreateEmergencyContact', $scope.newContact)
            .then(function (response) {
                console.log("success");
                $window.location.href = "/Manage/EmergencyContact";
            });           

        };

        $scope.cancel = function () {
            $window.location.href = "/Manage/EmergencyContact";
        };

   
    });
    
    
    
        
})(angular);

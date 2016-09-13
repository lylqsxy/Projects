(function (angular) {
    app.controller('ECCtrl', function ($scope, $http, $window, $mdDialog) {
        //initialise profile if not yet been created
        
        $http({
            method: "GET",
            url: "/Manage/CreateProfile"
        }).then(function success(response) {
            alert("got profile");
        }, function error(response) {
            alert("no profile");
        });

       $scope.phoneTypeOptions = [];
       $scope.relationships = [];
       $scope.phoneCountryOptions = [];
       initialise(); 

        $scope.relationships = ['Mother', "Father", "Brother", "Sister"];
        $scope.ecContacts = [{
            Priority: 1,
            Firstname: "contact2",
            Middlename: "del",
            Lastname: "camp",
            RelationshipID: 2,
            Reason: "accident",
            PhoneList: [{ Number: "09 210222new", PhoneTypeID: 3, CountryID: 1, IsPrimary: false, IsMobile: false },
                        { Number: "0210222new", PhoneTypeID: 3, CountryID: 1, IsPrimary: false, IsMobile: true }]
        }];
          
        function initialise() {
          $http.get("/Manage/GetEmergencyContacts").then(function (response) {
              $scope.ecContacts = response.data;
              console.log('get emergency contact');
           });
         
          //Get predefine attributes 
          $http({
              method: "GET",
              url: "/Manage/GetEmergencyContactAttributes"
          }).then(function success(response) {
              $scope.phoneTypeOptions = response.data.PhoneTypes;
              $scope.relationships = response.data.Relationships;
              $scope.phoneCountryOptions = response.data.PhoneCountries;
              console.log("countryoptions phones ", $scope.phoneCountryOptions);
          }, function error(response) {
          });

        }

        $scope.dragControlListeners = {
            containment: '#board',//optional param.
            dragEnd: function (event) { console.log($scope.ecContacts); },
            allowDuplicates: false //optional param allows duplicates to be dropped.
        };

        function getDestIndx(event) {
            var destIndx = event.dest.index;
            console.log('destIndx: ', destIndx);
        }

        var contact = {
            Priority: 1,
            Firstname: "contact3",
            Middlename: "del",
            Lastname: "camp",
            RelationshipID: 2, 
            Reason: "accident",
            PhoneList: [{ Number: "09 210222new", PhoneTypeID: 3, CountryID: 1, IsPrimary: false, IsMobile: false },
                        { Number: "0210222new", PhoneTypeID: 3, CountryID: 1, IsPrimary: false, IsMobile: true }]
        };
        

        //Invoke when creating a new contact 
        $scope.addEC = function ($event) {
            $window.location.href = "/Manage/CreateEmergencyContact";

        };

        $scope.dragMode = false;
        $scope.dragDisabled = true;
        $scope.dragModeStyle = '';
        $scope.cursorClass = "noDragCursor";
        $scope.toogleDragMode = function () {
            $scope.dragDisabled = $scope.dragMode == true ? false : true;

            $scope.cursorClass = $scope.dragMode == true ? 'dragCursor' : 'noDragCursor';
        };
        
        //the layout for showing emergency contact details
        $scope.isDetailsEnabled = false;
        $scope.contactDetail = {}; 
        $scope.viewContactDetails = function (contact) {
            $scope.isDetailsEnabled = true;
            $scope.contactDetail = contact;
            console.log($scope.contactDetail);
        };

        //edit contact
        $scope.edit = function (event) {
            var updateContact = {
                Id: 1004,
                Firstname: "helen",
                Lastname: "del",
                Middlename: "camp",
                RelationshipID: 1, //change to mother
                Priority: 2, 
                Reason: "greeting",
                PhoneList: [{ Number: "09 210222edit", PhoneTypeID: 3, CountryID: 1, IsPrimary: false, IsMobile: false },
                        { Number: "0210222edit", PhoneTypeID: 3, CountryID: 1, IsPrimary: false, IsMobile: true }]

            }

           // $window.location.href = "/Manage/UpdateEmergencyContact";

            $http({
                method: "POST",
                data: $scope.contactDetail,
                url: "/Manage/EditEmergencyContact"
            }).then(function success(response) {
                 
                console.log("edited on server"); 
            }, function failed(response) {
                console.log("failed edit on server");
            });
         }

        $scope.delete = function (event, contactToDelete) {
            var confirm = $mdDialog.confirm()
                              .title('Would you like to delete your debt?')
                              .textContent('All of the banks have agreed to forgive you your debts.')
                              .ariaLabel('Lucky day')
                              .targetEvent(event)
                              .ok('OK')
                              .cancel('CANCEL');
            $mdDialog.show(confirm).then(function () {
                $http({
                    method: "POST",
                    data: contactToDelete,
                    url: "/Manage/DeleteEmergencyContact"
                }).then(function success(response) {
                    //update UI
                    var indxToDelete = $scope.ecContacts.indexOf(contactToDelete); 
                    $scope.ecContacts.splice(indxToDelete, 1); 
                }, function fail(response) {
                });
            }, function () {
            });
         
        }

        $scope.addPhone = function () {
            //alert('addPhone called');
            var newPhone = { Number: "", PhoneTypeID: 0, CountryID: 0, IsMobile: false, IsPrimary: false };

            $scope.contactDetail.PhoneList.push(newPhone);
        };
    });
})(window.angular);
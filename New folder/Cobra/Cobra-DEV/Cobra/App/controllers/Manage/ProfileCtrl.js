
/*! 
 Name        : ProfileCtrl.js 
 Version     : 
 Author      : Riyas and Eric
 Date        : 10-08-2016 
 Description : This Angular JS controller file hold functions for user interface UserProfile.cshtml file
               interect with manage contoller's methods.
               Using app.js module file 
 */

cobraApp.controller('manage/ProfileCtrl', ['$scope', '$http', '$window', '$uibModal', 'myservice', function ($scope, $http, $window, $uibModal, myservice) { // mdDialog not injected


    "use strict";
    $scope.btnStatusss = false;
    //$scope.showStates = true;
    //$document.callAFunc = function(){
    //    $scope.btnStatus = true;
    //    var inputsss = document.getElementById('input123456');
    //    //autoPlaces = new google.maps.places.Autocomplete(input, { types: ['geocode'] });
    //    var autoPlacessss = new google.maps.places.Autocomplete(inputsss);
    //};
    // this function run when page loading
    $window.onload = function () {
        // alert("Called on page load..");
    };

    // not used 
    var vm = this;
    $scope.title = "Profile Page";
    this.initializeController = function () {
    }


    $scope.states = ('AL AK AZ AR CA CO CT DE FL GA HI ID IL IN IA KS KY LA ME MD MA MI MN MS ' +
    'MO MT NE NV NH NJ NM NY NC ND OH OK OR PA RI SC SD TN TX UT VT VA WA WV WI ' +
    'WY').split(' ').map(function (state) {
        return { abbrev: state };
    });
    $scope.gendertypes = ('Male Female').split(' ').map(function (state) {
        return { abbrev: state };
    })

    //$scope.countrycodes = ('01 64 63').split(' ').map(function (state) {
    //    return { abbrev: state };
    //})

    //$scope.phonecodes = ('01 02 03 021 027').split(' ').map(function (state) {
    //    return { abbrev: state };
    //})


    // Initializing scope

    // response data assigning to data object
    $scope.data = {};
    // response data assigning to modalData object when modal trigger for editing purpose (modalData used ONLY in the modal opened when edit any data)
    $scope.modalData = {};
    $scope.emails = [];
    $scope.phoneNumbers = [];
    $scope.addresses = [];
    $scope.profile = [];



    // array for address types from db table address type
    $scope.addresstypes = [];
    $http.get('/Manage/GetAddressType').then(
            function (response) {
                $scope.addresstypes = response.data;
            }
        );

    // array for country types from db table country type
    $scope.countrytypes = [];
    $http.get('/Manage/GetCountry').then(
            function (response) {
                $scope.countrytypes = response.data;
            }
        );
    // array for phone types from db table phone type
    $scope.phonetypes = [];
    $http.get('/Manage/GetPhoneType').then(
            function (response) {
                $scope.phonetypes = response.data;

                // function to get phonetype name
                Object.keys($scope.phonetypes).forEach(function (key) {

                    // console.log(key, $scope.phonetypes[key].Name);

                });
            }
        );





    $(document).ready(function () {
        // console.log("ready!");
        //  console.log($scope.phoneNumbers);
        // logArrayElements();
        var obj = {
            first: "John",
            last: "Doe"
        };

        //
        //  Visit non-inherited enumerable keys
        //
        Object.keys(obj).forEach(function (key) {

            // console.log(key, obj[key]);

        });

    });

    // array for emails types from db table emails type
    $scope.emailtypes = [];
    $http.get('/Manage/GetEmailType').then(
            function (response) {
                $scope.emailtypes = response.data;

            }
        );

    // function for send data from UI to database update, send data to controller Manage to UpdateProfile method
    $scope.postdata = function () {
        // $scope.addNewAddress();
        $http.post('/Manage/UpdateProfile', $scope.modalData).then(
           function (response) {
               $scope.getProfileData();
           }
       );
    }

    //console.log(tmp)

    //Author Aakash
    // to disable the socialmedia button after register

    //$scope.isDisabled = false;
    //$scope.DisableButton = function () {
    //    $scope.isDisabled = true;
    //}
    $scope.socialMediaFa = [];
    $scope.socialMediaGo = [];
    var request = {
        method: 'get',
        url: '/manage/SocialMediaProvider'
    };
    $http(request).then(function (response) {
        console.log(response.data.length);
        if (response.data.length === 0) {
            //console.log("nothing to show");
        } else {
            if (response.data['0'].SocialMediaTypeId !== null && response.data['0'].SocialMediaTypeId === 1) {
                $scope.socialMediaGo = response.data['0'].SocialMediaTypeId;
                if (response.data.length === 2) {
                    $scope.socialMediaFa = response.data['1'].SocialMediaTypeId;
                };
                //console.log(response.data['0'].SocialMediaTypeId);

            } else if (response.data['0'].SocialMediaTypeId !== null && response.data['0'].SocialMediaTypeId === 2) {
                $scope.socialMediaFa = response.data['0'].SocialMediaTypeId;
                if (response.data.length === 2) {
                    $scope.socialMediaGo = response.data['1'].SocialMediaTypeId;
                };
            };
        };
        //console.log(response.data['0'].SocialMediaTypeId);
        //console.log(response.data['1'].SocialMediaTypeId);
        //valid respone.data


        //point to index of response.data to the social media data



    });







    // get all profile data from database to UI for a current user, Using method CreateProfile in Manage controller
    // 
    $scope.getProfileData = function () {

        $http.get('/Manage/CreateProfile').then(
           function (response) {
               $scope.data = response.data;
               if ($scope.data.PersonModel.DoB != null) {
                   $scope.profile = response.data.ProfileModel;
                   $scope.data.PersonModel.DoB = new Date(ToJavaScriptDate(response.data.PersonModel.DoB));
               }
               $scope.emails = response.data.EmailModel;
               $scope.phoneNumbers = response.data.PhoneModel;
               $scope.addresses = response.data.AddressModel;
               $scope.deActivePhoneDeleteButton();
               $scope.deActiveEmailDeleteButton();
               $scope.deActiveAddressDeleteButton();
           }
       );
    }


    // date picker ========= Start >>>

    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: new Date(),
        showWeeks: true
    };

    $scope.dateOptions = {
        dateDisabled: disabled,
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    // Disable weekend selection
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }

    $scope.toggleMin = function () {
        $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
        $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
    };

    $scope.toggleMin();

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };

    $scope.setDate = function (year, month, day) {
        $scope.dt = new Date(year, month, day);
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };

    $scope.popup2 = {
        opened: false
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.events = [
      {
          date: tomorrow,
          status: 'full'
      },
      {
          date: afterTomorrow,
          status: 'partially'
      }
    ];

    function getDayClass(data) {
        var date = data.date,
          mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    }

    // date picker ============= End <<<<<



    // function to change json date format to regular date format, using this method on UI to display the date
    function ToJavaScriptDate(value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
    }

    //**************** EMAIL TAB *********************//


    // add new Email address into the emails array
    $scope.addNewEmail = function () {
        var newItemNo = $scope.emails.length + 1;
        $scope.emails.push({ Id: 0, EmailTypeId: null, Email: null, IsPrimary: false });
    };

    // Checkbox update function for email primary in email tab
    $scope.updateSelection = function (position, emails) {
        angular.forEach(emails, function (email, index) {
            if (position != index)
                email.IsPrimary = false;
        });
        angular.forEach(emails, function (email, index) {
            if (position == index)
                email.IsPrimary = false;

        });
    }

    // send current email id to controller and delete UI after server confirmation 
    // using confirmDeleteEmail()
    $scope.removeEmail = function (index) {
        var id = $scope.emails[index].Id;
        $http.post('/Manage/DeleteEmail', { 'id': id }).then(
           function (response) {
               $scope.getProfileData();
           }
       );
    };


    // confirm with dialoge deleting email 
    $scope.confirmDeleteEmail = function (ev, index) {
        var arrayItemIndex = index;
        var id = $scope.emails[index].Id;
        if (arrayItemIndex == 0 && $scope.emails[index + 1].Id == 0) {
            $mdDialog.show(
               $mdDialog.alert()
                 // .parent(angular.element(document.querySelector('#dialogContainer'))) //  id dialogContainer  can be used inside div
                  .clickOutsideToClose(true)
                 // .title('Do you want to delete?')
                  .textContent('Sorry! Record can not be deleted')
                  .ariaLabel('Sorry! Record can not be deleted')
                  .ok('Ok')
                  .targetEvent(ev)
            );
        } else {
            if (!id == 0) {
                var confirm = $mdDialog.confirm()
                         .title('Would you like to delete email?')
                         //.textContent('All you want to keep emails.')
                         .ariaLabel('Lucky day')
                         .targetEvent(ev)
                         .ok('Delete')
                         .cancel('No');
                $mdDialog.show(confirm).then(function () {
                    $scope.status = 'You decided Delete email.';
                    $scope.removeEmail(index);
                }, function () {
                    $scope.status = 'You decided to keep your email.';
                });
            } else {
                $scope.emails.splice(index);
                $scope.deActiveEmailDeleteButton();
            }
        }
    };

    // deActivate email delete button
    $scope.deActiveEmailDeleteButton = function () {
        if ($scope.emails.length > 1) {
            $scope.emaildissable = false;
        } else {
            $scope.emaildissable = true;
        }
    }

    //************ PHONE TAB *********//

    // add new Phone number into phoneNumbers array
    $scope.addNewPhoneNumber = function () {
        var newItemNo = $scope.emails.length + 1;
        $scope.phoneNumbers.push({ Id: 0, PhoneTypeId: null, Number: null, IsMobile: false, CountryId: null, IsPrimary: false });
    };

    // send current phone id to controller and delete UI after server confirmation 
    // using confirmDeletePhone()
    $scope.removePhone = function (index) {
        var id = $scope.phoneNumbers[index].Id;
        $http.post('/Manage/DeletePhone', { 'id': id }).then(
           function (response) {
               $scope.getProfileData();
           }
       );
    };

    // confirm with dialoge and delete Phone number 
    $scope.confirmDeletePhone = function (ev, index) {
        var arrayItemIndex = index;
        var id = $scope.phoneNumbers[index].Id;
        if (arrayItemIndex == 0 && $scope.phoneNumbers[index + 1].Id == 0) {
            $mdDialog.show(
               $mdDialog.alert()
                 // .parent(angular.element(document.querySelector('#dialogContainer'))) //  id dialogContainer  can be used inside div
                  .clickOutsideToClose(true)
                 // .title('Do you want to delete?')
                  .textContent('Sorry! Record can not be deleted')
                  .ariaLabel('Sorry! Record can not be deleted')
                  .ok('Ok!')
                  .targetEvent(ev)
            );
        } else {
            if (!id == 0) {
                var confirm = $mdDialog.confirm()
                         .title('Would you like to delete phone number?')
                         //.textContent('All you want to keep phone.')
                         .ariaLabel('Lucky day')
                         .targetEvent(ev)
                         .ok('Delete')
                         .cancel('No');
                $mdDialog.show(confirm).then(function () {
                    $scope.status = 'You decided Delete phone.';
                    $scope.removePhone(index);
                }, function () {
                    $scope.status = 'You decided to keep your phone.';
                });
            } else {
                $scope.phoneNumbers.splice(index);
                $scope.deActivePhoneDeleteButton();
            }
        }

    };

    // Checkbox update function for phone primary tab
    $scope.phonePrimarySelection = function (position, phoneNumbers) {
        angular.forEach(phoneNumbers, function (phoneNumber, index) {
            if (position != index)
                phoneNumber.IsPrimary = false;
        });
        angular.forEach(phoneNumbers, function (phoneNumber, index) {
            if (position == index)
                phoneNumber.IsPrimary = false;
        });
    }



    // Checkbox update function for phone mobile tab
    //$scope.phoneMobileSelection = function (position, phoneNumbers) {
    //    angular.forEach(phoneNumbers, function (phoneNumber, index) {
    //        if (position != index)
    //            phoneNumber.IsMobile = false;
    //    });
    //}

    // deActivate phonedelete button
    $scope.deActivePhoneDeleteButton = function () {
        if ($scope.phoneNumbers.length > 1) {
            $scope.dissable = false;
        } else {
            $scope.dissable = true;
        }
    }

    // deActivate phone Add button
    $scope.deActivePhoneAddButton = function () {
        var lastItem = $scope.phoneNumbers.length - 1;
        if ($scope.phoneNumbers[lastItem].Number == null) {
            $scope.phoneAddButton = true;
        } else {
            $scope.phoneAddButton = false;
        }
    }

    // Select primary if only one record in the db
    $scope.selectPrimary = function () {
        if ($scope.phoneNumbers.length == 1 && $scope.phoneNumbers[0].IsPrimary != true) {
            $scope.phoneNumbers[0].IsPrimary = true;
            $scope.postdata();
        } else if ($scope.emails.length == 1 && $scope.emails[0].IsPrimary != true) {
            $scope.emails[0].IsPrimary = true;
            $scope.postdata();
        } else if ($scope.addresses.length == 1 && $scope.addresses[0].IsPrimary != true) {
            $scope.addresses[0].IsPrimary = true;
            $scope.postdata();
        }
    }


    //************** ADDRESS TAB *************************//


    // Checkbox primary update function for address tab
    $scope.addresPrimarySelection = function (position, addresses) {
        angular.forEach(addresses, function (address, index) {
            if (position != index)
                address.IsPrimary = false;
        });
        angular.forEach(addresses, function (address, index) {
            if (position == index)
                address.IsPrimary = false;
        });

    }

    // edit address function


    // object for new address for UI
    $scope.addAddress = {
        Id: 0, AddressTypeId: " ", StreetNumber: " ", StreetName: " ", Suburb: " ", City: " ", State: " ", Country: " ", ZipCode: " ", IsPrimary: false
    };

    // function add new address to array addresses
    $scope.addNewAddress = function () {
        if ($scope.userFormAddaddress.$valid) {
            var sNumber = document.getElementById("street_number").value;
            var sName = document.getElementById("route").value;
            var suburb = document.getElementById("administrative_area_level_2").value;
            var city = document.getElementById("locality").value;
            var state = document.getElementById("administrative_area_level_1").value;
            var country = document.getElementById("country").value;
            var zipCode = document.getElementById("postal_code").value;

            $scope.addresses.push({
                Id: 0, AddressTypeId: $scope.addAddress.AddressTypeId, StreetNumber: sNumber, StreetName: sName, Suburb: suburb,
                City: city, State: $scope.addAddress.State, Country: country, ZipCode: zipCode, IsPrimary: false
            });
            $scope.resetform();
        }
    };

    // reset function for add new address form
    $scope.resetform = function () {
        document.getElementById("userFormAddaddressId").reset();
        $scope.forceUnknownOption();
    }

    // reset function for add new address form dropdown box
    $scope.forceUnknownOption = function () {
        $scope.addAddress.AddressTypeId = 'nonsense';
        $scope.addAddress.State = 'nonsense';
    };

    // reset function method for add new address checkbox
    $scope.toggleSync = function () {
        $scope.resetform();
    };

    // send current address id to controller and delete UI after server confirmation 
    // using confirmDeleteAddress()
    $scope.removeAddress = function () {
        var index = document.getElementById("idLabelIndex").value;
        var id = $scope.addresses[index].Id;
        $http.post('/Manage/DeleteAddress', { 'id': id }).then(
           function (response) {
               $scope.getProfileData();
           }
       );
    };

    // confirm with dialoge delete Address 
    $scope.confirmDeleteAddress = function (ev, index) {
        var arrayItemIndex = index;
        var id = $scope.addresses[index].Id;
        if (arrayItemIndex == 0 && $scope.addresses[index].Id == 0) {
            $mdDialog.show(
               $mdDialog.alert()
                 // .parent(angular.element(document.querySelector('#dialogContainer'))) //  id dialogContainer  can be used inside div
                  .clickOutsideToClose(true)
                 // .title('Do you want to delete?')
                  .textContent('Sorry! Record can not be deleted')
                  .ariaLabel('Sorry! Record can not be deleted')
                  .ok('Ok!')
                  .targetEvent(ev)
            );
        } else {
            if (!id == 0) {
                var confirm = $mdDialog.confirm()
                         .title('Would you like to delete Address?')
                         //.textContent('All you want to keep phone.')
                         .ariaLabel('Lucky day')
                         .targetEvent(ev)
                         .ok('Delete')
                         .cancel('No');
                $mdDialog.show(confirm).then(function () {
                    $scope.status = 'You decided Delete Address.';
                    $scope.removeAddress(index);
                }, function () {
                    $scope.status = 'You decided to keep your adress.';
                });
            } else {
                $scope.addresses.splice(index);
                $scope.deActiveAddressDeleteButton();
            }
        }
    };

    // address delete button deActivate function
    $scope.deActiveAddressDeleteButton = function () {
        if ($scope.addresses.length > 1) {
            $scope.addressdissable = false;
        } else {
            $scope.addressdissable = true;
        }
    }

    // ****** MODEL ****** START >>>>>>

    // navigate to previous page
    $scope.go_back = function () {
        $window.history.back();
    };

    $(document).ready(function () {
        //button edit details function, show modal and change modal title
        $("#btnDetails").click(function () {
            $("#common-modal").modal("show");
            $('#common-modal').modal({
                backdrop: 'static'
            }).on('shown.bs.modal', function () {
                //  alert(data);
                $('#common-modal .modal-title').text('Edit Details ')
                //$("#date_timestamp").val('riyas');
            });
        });

        // button function for edit address load address on the edit model

        $scope.editAddress = function (addressId, isPrimary) {
            $("#common-modal").modal("show");
            $('#common-modal').modal({
                backdrop: 'static'
            })
            // $scope.selectedUserId = addressId;  // passing the addressId to the view
            $("#common-modal").on('shown.bs.modal', function () {
                $('#common-modal .modal-title').text('Edit Address ')
                document.getElementById("idPrimaryAddress").checked = isPrimary;

                angular.forEach($scope.addresses, function (address, key) {
                    if (address.Id === addressId) {
                        angular.forEach($scope.addresstypes, function (addresstype, key) {
                            if (address.AddressTypeId === addresstype.Id) {
                                $("#idAddressType").val(addresstype.Name);
                            }
                        });
                        $("#idAddress").val(address.Id);
                        $("#idStNumber").val(address.StreetNumber);
                        $("#idStName").val(address.StreetName);
                        $("#IdSuburb").val(address.Suburb);
                        $("#IdCity").val(address.City);
                        $("#IdState").val(address.State);
                        $("#IdCountry").val(address.Country);
                        $("#IdZipcode").val(address.ZipCode);
                    }
                });
            });
        };

        $("#edit").click(function () {
            $("#common-modal").modal("show");
        });
    });


    cobraApp.directive('modalDialog', function () {
        return {
            restrict: 'E',
            replace: true,
            transclude: true,
            link: function (scope) {
                scope.cancel = function () {
                    scope.$dismiss('cancel');
                };
            },

        };
    });

    $scope.myservice1 = myservice;
    $scope.name2 = myservice;

    // open modal to edit details when edit button trigger for details
    $scope.openEditDetail = function (size, template) {
        $scope.modalData = JSON.parse(JSON.stringify($scope.data));
        $scope.modalData.PersonModel.DoB = $scope.data.PersonModel.DoB;
        var modalInstance = $uibModal.open({
            templateUrl: template || 'Template',
            controller: 'ModalController',
            scope: $scope,
            size: size
        });
    };

    // passing all response data to modalDataAddress object when function openEditAddress() 
    $scope.modalDataAddress = {};
    // save $index value from view when edit button trigger
    $scope.modelIndex;
    // save AddressTypeId value to string
    $scope.stringAddressTypeId;

    // open modal to edit address when edit button trigger in address pannel
    $scope.openEditAddress = function (index) {
        $scope.modelIndex = index;
        // parse $scope.data to modalData object to edit purpose when modal opened
        $scope.modalData = JSON.parse(JSON.stringify($scope.data));
        // $scope.modalDataAddress = JSON.parse(JSON.stringify($scope.addresses[index]));
        $scope.modalDataAddress = JSON.parse(JSON.stringify($scope.modalData.AddressModel[$scope.modelIndex]));
        // $scope.modalData = $scope.addresses[index];
        $scope.stringAddressTypeId = $scope.modalDataAddress.AddressTypeId.toString();
        var modalInstance = $uibModal.open({
            templateUrl: 'idEditAddressTemplate',
            controller: 'ModalController',
            scope: $scope
        });
    };

    // save edited Address data when save button triggered from modal
    $scope.saveEditAddress = function () {
        console.log("primary: " + $scope.modalDataAddress.IsPrimary);
        // get addresstypeid value from select element for Address type passing to variable
        var ngmodelStringAddressTypeId = document.getElementById("idAddressType").value;
        // change ngmodelStringAddressTypeId value int for saving purpose into database
        $scope.modalDataAddress.AddressTypeId = parseInt(ngmodelStringAddressTypeId);
        // after edit address on the current modal passing edited modalDataAddress to modalData.AddressModel for saving into database
        $scope.modalData.AddressModel[$scope.modelIndex] = $scope.modalDataAddress;

        // in current modal primary selected, then modalDataAddress.IsPrimary == true. then change other addresses primary to false
        if ($scope.modalDataAddress.IsPrimary) {
            angular.forEach($scope.modalData.AddressModel, function (address, index) {
                if ($scope.modelIndex != index)
                    address.IsPrimary = false;
            });
        }
        $scope.postdata()
    };

    // open modal to Delete address when delete button trigger in address pannel

    $scope.openDeletAddress = function (index) {
        $scope.recordIndex = index;
        var modalInstance = $uibModal.open({
            templateUrl: 'idDeleteAddressTemplate',
            controller: 'ModalController',
            scope: $scope
        });
    };

    $scope.openAddAddress = function (index) {
        $scope.recordIndex = index;
        var modalInstance = $uibModal.open({
            templateUrl: 'idAddNewAddressTemplate',
            controller: 'ModalController',
            scope: $scope
        });
    };

    // ****** MODEL ****** END <<<<<<<

    $scope.getProfileData();


}]);



cobraApp.run(['$http', function ($http) {
    $http.defaults.headers.common['X-XSRF-Token'] =
        angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value');
}]);


cobraApp.controller('ModalController', ['$scope', '$http', '$uibModalInstance', 'myservice', function ($scope, $http, $uibModalInstance, myservice) {
    $scope.dialogTitle = 'Your title';
    $scope.myservice2 = myservice;
    $scope.name1 = myservice;
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}]);

cobraApp
    .service('myservice', function () {
        this.xxx = "yyy";
        this.name = "riyas"
    });
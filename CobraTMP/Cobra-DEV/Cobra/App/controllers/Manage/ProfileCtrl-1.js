
/*! 
 Name        : ProfileCtrl.js 
 Version     : 
 Author      : Riyas 
 Date        : 10-08-2016 
 Description : This Angular JS controller file hold functions for user interface UserProfile.cshtml file
               interect with manage contoller's methods.
               Using app.js module file 
 */

app.controller('manage/ProfileCtrl', function ($scope, $http, $mdDialog, $window) {

    "use strict";
    //$scope.showStates = true;

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
    $scope.data = {};
    $scope.emails = [];
    $scope.phoneNumbers = [];
    $scope.address = [];
    $http.get('/Manage/CreateProfile').then(
            function (response) {
                $scope.data = response.data;
                if ($scope.data.PersonModel.DoB != null) {
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

            }
        );

    // array for emails types from db table emails type
    $scope.emailtypes = [];
    $http.get('/Manage/GetEmailType').then(
            function (response) {
                $scope.emailtypes = response.data;

            }
        );

    // function for send data from UI to database update, send data to controller Manage to UpdateProfile method
    $scope.postdata = function () {
        $scope.addNewAddress();
        $http.post('/Manage/UpdateProfile', $scope.data).then(
           function (response) {
               $scope.getProfileData();
           }
       );
    }

    // get all profile data from database to UI for a current user, Using method CreateProfile in Manage controller
    // 
    $scope.getProfileData = function () {

        $http.get('/Manage/CreateProfile').then(
           function (response) {
               $scope.data = response.data;
               if ($scope.data.PersonModel.DoB != null) {
                   $scope.data.PersonModel.DoB = new Date(ToJavaScriptDate(response.data.PersonModel.DoB));
               }
               $scope.emails = response.data.EmailModel;
               $scope.phoneNumbers = response.data.PhoneModel;
               $scope.addresses = response.data.AddressModel;
               $scope.deActivePhoneDeleteButton();
               $scope.deActiveEmailDeleteButton();
               $scope.deActiveAddressDeleteButton();
               $scope.selectPrimary();

           }
       );
    }

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
    $scope.removeAddress = function (index) {
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
        if (arrayItemIndex == 0 && $scope.addresses[index + 1].Id == 0) {
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


});
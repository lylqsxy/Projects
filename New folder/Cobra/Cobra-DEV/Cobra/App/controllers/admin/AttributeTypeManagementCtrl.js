(function () {
    // Controller: ManageAttributesType
    cobraApp.controller('admin/ManageAttributesTypeCtrl', ['$scope', '$http', '$window','utils', function ($scope, $http, $window, utils) {
        $scope.AddressTypes = [];
        $scope.EmailTypes = [];
        $scope.CountryTypes = [];
        $scope.PhoneTypes = [];
        $scope.SocialMediaTypes = [];
        $scope.RelationshipTypes = [];
        $scope.EventTypes = [];
        $scope.AlertTypes = [];
        $scope.ResourceTypes = [];

        //GetAddressTypeList();
        //GetEmailTypeList();
        //GetCountryTypeList();
        //GetPhoneTypeList();
        //GetSocialMediaTypeList();
        //GetRelationshipTypeList();
        //GetEventTypeList();
        //GetAlertTypeList();
        //GetResourceTypeList();
        GetAllTypeList();

        function GetAllTypeList() {
            $http({
                method: 'GET',
                url: '/Admin/GetAllType',
                cache: false
            }).then(function successCallback(response) {
                console.log(response);
                $scope.AddressTypes = response.data.allAddressType;
                $scope.EmailTypes = response.data.allEmailType;
                $scope.CountryTypes = response.data.allCountryType;
                $scope.PhoneTypes = response.data.allPhoneType;
                $scope.SocialMediaTypes = response.data.allSocialMediaType;
                $scope.RelationshipTypes = response.data.allRelationshipType;
                $scope.EventTypes = response.data.allEventType;
                $scope.AlertTypes = response.data.allAlertType;
                $scope.ResourceTypes = response.data.allResourceType;
            }, function errorCallback(response) {
                console.log('error');
            });
        }
        

        //function GetAddressTypeList() {
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllAddressType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.AddressTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //function GetEmailTypeList() {
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllEmailType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.EmailTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //function GetCountryTypeList() {
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllCountryType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.CountryTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //function GetPhoneTypeList() {
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllPhoneType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.PhoneTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //function GetSocialMediaTypeList() {
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllSocialMediaType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.SocialMediaTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //function GetRelationshipTypeList(){
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllRelationshipType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.RelationshipTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //function GetEventTypeList() {
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllEventType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.EventTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //function GetAlertTypeList() {
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllAlertType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.AlertTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //function GetResourceTypeList(){
        //    $http({
        //        method: 'GET',
        //        url: '/Admin/GetAllResourceType',
        //    }).then(function successCallback(response) {
        //        console.log(response);
        //        $scope.ResourceTypes = response.data;
        //    }, function errorCallback(response) {
        //        console.log('error');
        //    });
        //}

        //Prepare one single order value to Modal
        //Address
        $scope.toAddressModalObject = function (Address) {
            var AddressToModal = [
                    { title: 'Address Type Name', variableName: 'Name', value: (Address ? Address.Name : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },
            ];

            return AddressToModal;
        };

        //Email
        $scope.toEmailModalObject = function (Email) {
            var EmailToModal = [
                    { title: 'Email Type Name', variableName: 'Name', value: (Email ? Email.Name : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },
            ];

            return EmailToModal;
        };

        //Country
        $scope.toCountryModalObject = function (Country) {
            var CountryToModal = [
                    { title: 'Country Name', variableName: 'Name', value: (Country ? Country.Name : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },
                    { title: 'Country Code', variableName: 'CountryCode', value: (Country ? Country.CountryCode : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },
                    { title: 'Phone Code', variableName: 'PhoneCode', value: (Country ? Country.PhoneCode : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },
            ];

            return CountryToModal;
        };

        //Phone
        $scope.toPhoneModalObject = function (Phone) {
            var PhoneToModal = [
                    { title: 'Phone Type', variableName: 'Name', value: (Phone ? Phone.Name : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },

            ];

            return PhoneToModal;
        };

        //Social Media
        $scope.toSocialMediaModalObject = function (SocialMedia) {
            var SocialMediaToModal = [
                    { title: 'SocialMedia Name', variableName: 'Name', value: (SocialMedia ? SocialMedia.Name : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },

            ];

            return SocialMediaToModal;
        };

        //Relationship
        $scope.toRelationshipModalObject = function (RelationShip) {
            var RelationshipToModal = [
                    { title: 'Relationship To You', variableName: 'RelationshipToYou', value: (RelationShip ? RelationShip.RelationshipToYou : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },

            ];

            return RelationshipToModal;
        };

        //Event
        $scope.toEventModalObject = function (Event) {
            var EventToModal = [
                    { title: 'Event Name', variableName: 'Name', value: (Event ? Event.Name : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },
                    { title: 'Event Description', variableName: 'Description', value: (Event ? Event.Description : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },

            ];

            return EventToModal;
        };

        //Alert
        $scope.toAlertModalObject = function (Alert) {
            var AlertToModal = [
                    { title: 'Alert Name', variableName: 'Name', value: (Alert ? Alert.Name : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },

            ];

            return AlertToModal;
        };

        //Resource
        $scope.toResourceModalObject = function (Resource) {
            var ResourceToModal = [
                    { title: 'Resource Name', variableName: 'Name', value: (Resource ? Resource.Name : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },

            ];

            return ResourceToModal;
        };

        //prepare the modal options, including the back-end controller name, action name, for each attribute
        //showAddressModal is a on-click function for a button in view, using showAddressModal() means create new
        //while using showAddressModal('Id') means edit a existing item

        //Address
        $scope.showAddressModal = function (Id) {

            var modalOption = {
                modalTitle: Id ? 'Edit Address Type' : 'Create New Address Type',
                controller: 'Admin', // corrsponding to .net controller
                action: Id ? 'UpdateAddressType' : 'CreateAddressType', // index, edit and create, corrsponding to .net backend action
                idVariable: 'Id', // Id variale 
                idValue: Id ? Id : ''  // the id of the entity, when create new, keep empty
            };
            if (Id) {
                var index = utils.findObjectIndexInArray($scope.AddressTypes, 'Id', Id);
                $scope.$broadcast('showModelEvent', [$scope.toAddressModalObject($scope.AddressTypes[index]), modalOption]);
            }
            else
            {
                $scope.$broadcast('showModelEvent', [$scope.toAddressModalObject(), modalOption]);
            }
        };

        //Email
        $scope.showEmailModal = function (Id) {

            var modalOption = {
                modalTitle: Id ? 'Edit Email Type': 'Create New Email Type',
                controller: 'Admin', // corrsponding to .net controller
                action: Id ? 'UpdateEmailType' : 'CreateEmailType', // index, edit and create, corrsponding to .net backend action
                idVariable: 'Id', // Id variale 
                idValue: Id ? Id:'' // the id of the entity, when create new, keep empty
            };
            if (Id) {
                var index = utils.findObjectIndexInArray($scope.EmailTypes, 'Id', Id);
                $scope.$broadcast('showModelEvent', [$scope.toEmailModalObject($scope.EmailTypes[index]), modalOption]);
            }
            else {
                $scope.$broadcast('showModelEvent', [$scope.toEmailModalObject(), modalOption]);
            }
        };

        //Country
        $scope.showCountryModal = function (Id) {

        var modalOption = {
            modalTitle: Id ? 'Edit Country Type': 'Create New Country Type',
            controller: 'Admin', // corrsponding to .net controller
            action: Id ? 'UpdateCountryType' : 'CreateCountryType', // index, edit and create, corrsponding to .net backend action
            idVariable: 'Id', // Id variale 
            idValue: Id ? Id : ''  // the id of the entity, when create new, keep empty
        };
        if (Id) {
            var index = utils.findObjectIndexInArray($scope.CountryTypes, 'Id', Id);
            $scope.$broadcast('showModelEvent', [$scope.toCountryModalObject($scope.CountryTypes[index]), modalOption]);
        }
        else {
            $scope.$broadcast('showModelEvent', [$scope.toCountryModalObject(), modalOption]);
        }
    };

        //Phone
        $scope.showPhoneModal = function (Id) {

        var modalOption = {
            modalTitle: Id ? 'Edit Phone Type' : 'Create New Phone Type',
            controller: 'Admin', // corrsponding to .net controller
            action: Id ? 'UpdatePhoneType' : 'CreatePhoneType', // index, edit and create, corrsponding to .net backend action
            idVariable: 'Id', // Id variale 
            idValue: Id ? Id : ''  // the id of the entity, when create new, keep empty
        };
        if (Id) {
            var index = utils.findObjectIndexInArray($scope.PhoneTypes, 'Id', Id);
            $scope.$broadcast('showModelEvent', [$scope.toPhoneModalObject($scope.PhoneTypes[index]), modalOption]);
        }
        else {
            $scope.$broadcast('showModelEvent', [$scope.toPhoneModalObject(), modalOption]);
        }
    };

        //Social Media
        $scope.showSocialMediaModal = function (Id) {

        var modalOption = {
            modalTitle: Id ? 'Edit SocialMedia Type': 'Create New SocialMedia Type',
            controller: 'Admin', // corrsponding to .net controller
            action: Id ? 'UpdateSocialMediaType' : 'CreateSocialMediaType', // index, edit and create, corrsponding to .net backend action
            idVariable: 'Id', // Id variale 
            idValue: Id ? Id : ''  // the id of the entity, when create new, keep empty
        };
        if (Id) {
            var index = utils.findObjectIndexInArray($scope.SocialMediaTypes, 'Id', Id);
            $scope.$broadcast('showModelEvent', [$scope.toSocialMediaModalObject($scope.SocialMediaTypes[index]), modalOption]);
        }
        else {
            $scope.$broadcast('showModelEvent', [$scope.toSocialMediaModalObject(), modalOption]);
        }
        };

        //Relationship
        $scope.showRelationshipModal = function (Id) {

        var modalOption = {
            modalTitle: Id ? 'Edit Relationship Type' : 'Create New Relationship Type', 
            controller: 'Admin', // corrsponding to .net controller
            action: Id ? 'UpdateRelationshipType' : 'CreateRelationshipType', // index, edit and create, corrsponding to .net backend action
            idVariable: 'Id', // Id variale 
            idValue: Id ? Id : ''  // the id of the entity, when create new, keep empty
        };
        if (Id) {
            var index = utils.findObjectIndexInArray($scope.RelationshipTypes, 'Id', Id);
            $scope.$broadcast('showModelEvent', [$scope.toRelationshipModalObject($scope.RelationshipTypes[index]), modalOption]);
        }
        else {
            $scope.$broadcast('showModelEvent', [$scope.toRelationshipModalObject(), modalOption]);
        }
    };

        //Event
        $scope.showEventModal = function (Id) {

        var modalOption = {
            modalTitle: Id ? 'Edit Event Type': 'Create New Event Type',
            controller: 'Admin', // corrsponding to .net controller
            action: Id ? 'UpdateEventType' : 'CreateEventType', // index, edit and create, corrsponding to .net backend action
            idVariable: 'Id', // Id variale 
            idValue: Id ? Id : '' // the id of the entity, when create new, keep empty
        };
        if (Id) {
            var index = utils.findObjectIndexInArray($scope.EventTypes, 'Id', Id);
            $scope.$broadcast('showModelEvent', [$scope.toEventModalObject($scope.EventTypes[index]), modalOption]);
        }
        else {
            $scope.$broadcast('showModelEvent', [$scope.toEventModalObject(), modalOption]);
        }
    };

        //Alert
        $scope.showAlertModal = function (Id) {

        var modalOption = {
            modalTitle: Id ? 'Edit Alert Type': 'Create New Alert Type',
            controller: 'Admin', // corrsponding to .net controller
            action: Id ? 'UpdateAlertType' : 'CreateAlertType', // index, edit and create, corrsponding to .net backend action
            idVariable: 'Id', // Id variale 
            idValue: Id ? Id : ''  // the id of the entity, when create new, keep empty
        };
        if (Id) {
            var index = utils.findObjectIndexInArray($scope.AlertTypes, 'Id', Id);
            $scope.$broadcast('showModelEvent', [$scope.toAlertModalObject($scope.AlertTypes[index]), modalOption]);
        }
        else {
            $scope.$broadcast('showModelEvent', [$scope.toAlertModalObject(), modalOption]);
        }
    };

        //Resource
        $scope.showResourceModal = function (Id) {

        var modalOption = {
            modalTitle: Id ? 'Edit Resource Type': 'Create New Resource Type',
            controller: 'Admin', // corrsponding to .net controller
            action: Id ? 'UpdateResourceType' : 'CreateResourceType', // index, edit and create, corrsponding to .net backend action
            idVariable: 'Id', // Id variale 
            idValue: Id ? Id : ''  // the id of the entity, when create new, keep empty
        };
        if (Id) {
            var index = utils.findObjectIndexInArray($scope.ResourceTypes, 'Id', Id);
            $scope.$broadcast('showModelEvent', [$scope.toResourceModalObject($scope.ResourceTypes[index]), modalOption]);
        }
        else {
            $scope.$broadcast('showModelEvent', [$scope.toResourceModalObject(), modalOption]);
        }
        };

        //This function is used to update the display list
        $scope.FreshList = function (data)
        {
            //Address Type
            if (data.addresstype != null)
            {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.AddressTypes, 'Id', data.addresstype.Id);
                if (index != -1)
                {
                    //for edit case
                    $scope.AddressTypes[index] = data.addresstype;
                }
                else
                {
                    //can't find the index, for create new case
                    $scope.AddressTypes.push(data.addresstype);
                }
            }

            //Email Type
            if (data.emailtype != null) {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.EmailTypes, 'Id', data.emailtype.Id);
                if (index != -1) {
                    //for edit case
                    $scope.EmailTypes[index] = data.emailtype;
                }
                else {
                    //can't find the index, for create new case
                    $scope.EmailTypes.push(data.emailtype);
                }
            }

            //Country Type
            if (data.countrytype != null) {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.CountryTypes, 'Id', data.countrytype.Id);
                if (index != -1) {
                    //for edit case
                    $scope.CountryTypes[index] = data.countrytype;
                }
                else {
                    //can't find the index, for create new case
                    $scope.CountryTypes.push(data.countrytype);
                }
            }

            //Phone Type
            if (data.phonetype != null) {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.PhoneTypes, 'Id', data.phonetype.Id);
                if (index != -1) {
                    //for edit case
                    $scope.PhoneTypes[index] = data.phonetype;
                }
                else {
                    //can't find the index, for create new case
                    $scope.PhoneTypes.push(data.phonetype);
                }
            }


            //SocialMedia Type
            if (data.socialmediatype != null) {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.SocialMediaTypes, 'Id', data.socialmediatype.Id);
                if (index != -1) {
                    //for edit case
                    $scope.SocialMediaTypes[index] = data.socialmediatype;
                }
                else {
                    //can't find the index, for create new case
                    $scope.SocialMediaTypes.push(data.socialmediatype);
                }
            }


            //Relationship Type
            if (data.relationshiptype != null) {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.RelationshipTypes, 'Id', data.relationshiptype.Id);
                if (index != -1) {
                    //for edit case
                    $scope.RelationshipTypes[index] = data.relationshiptype;
                }
                else {
                    //can't find the index, for create new case
                    $scope.RelationshipTypes.push(data.relationshiptype);
                }
            }

            //Event Type
            if (data.eventtype != null) {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.EventTypes, 'Id', data.eventtype.Id);
                if (index != -1) {
                    //for edit case
                    $scope.EventTypes[index] = data.eventtype;
                }
                else {
                    //can't find the index, for create new case
                    $scope.EventTypes.push(data.eventtype);
                }
            }

            //Alert Type
            if (data.alerttype != null) {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.AlertTypes, 'Id', data.alerttype.Id);
                if (index != -1) {
                    //for edit case
                    $scope.AlertTypes[index] = data.alerttype;
                }
                else {
                    //can't find the index, for create new case
                    $scope.AlertTypes.push(data.alerttype);
                }
            }

            //Resource Type
            if (data.resourcetype != null) {
                //find the index of the returned object in the list
                var index = utils.findObjectIndexInArray($scope.ResourceTypes, 'Id', data.resourcetype.Id);
                if (index != -1) {
                    //for edit case
                    $scope.ResourceTypes[index] = data.resourcetype;
                }
                else {
                    //can't find the index, for create new case
                    $scope.ResourceTypes.push(data.resourcetype);
                }
            }
        };

        $scope.$on('modelDone', function (event, data) {
            if (data) {
                //fresh the diaplay list
                $scope.FreshList(data[0].data);
                console.log('Success');
            } else {
                console.log('error');
            }
        });

}]);


    cobraApp.run(['$http', function ($http) {
        $http.defaults.headers.common['X-XSRF-Token'] =
            angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value');
    }]);
})();
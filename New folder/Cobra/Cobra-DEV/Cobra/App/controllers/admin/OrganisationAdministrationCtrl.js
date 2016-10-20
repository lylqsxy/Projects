//Author: Craig Rabbitt
'use strict';
(function () {

    // Service for pagination by Tom
    cobraApp.service('orgServiceGet', ['$http', function ($http) {
        return {
            getData: function (val) {
                var req = {
                    method: 'Get',
                    url: '/Admin/OrganisationAdministrationData',
                    params: val
                };
                return $http(req);

            }
        };
    }]);

    cobraApp.controller('OrganisationAdministrationCtrl', ['$scope', '$http', '$window', 'orgServiceGet', '$rootScope', function ($scope, $http, $window, orgServiceGet, $rootScope) {
        // Initializing scope
        cobraApp.run(['$http', function ($http) {
            $http.defaults.headers.common['X-XSRF-Token'] =
                angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value');
        }]);

        $scope.data = [];   // contains page data

        $scope.rowSelected;   // contains current row being edited/created
        $scope.editOrg = { Id: 0, OrgName: "", WebsiteUrl: "", isActive: "false" };  // used to post row to controller
        //$scope.org = { Id: 0, orgName: '', WebsiteUrl: '', isActive: true, lastUpdate: '' };

        //pagination By Tom
        //Load item per page request
        $scope.pageSizePicker = 25; // items per page

        $scope.disableButtons = true;
        $scope.activePages = [];
        //click function on pagination
        $scope.PageSelected = function (index) {
            //console.log("clicked index", index);
            $scope.pageSkip = index * $scope.pageSizePicker;
            //param to server get method
            $scope.params = {

                displaySize: $scope.pageSizePicker,
                startRec: $scope.pageSkip
            };
            // pull data from service
            orgServiceGet.getData($scope.params).then(function (response) {
                //console.log(response);

                //calculate total page
                $scope.totalOrgs = response.data.totalOrgs;
                $scope.totalPage = Math.floor($scope.totalOrgs / $scope.pageSizePicker);

                //create page navigator
                $scope.startPage = $scope.totalPage - $scope.totalPage + 1;
                $scope.endPage = $scope.totalPage;
                $scope.activePages.length = 0;
                if (index > 4) {
                    var pageid = index - 4;
                } else {
                    pageid = 1;
                }
                if (pageid === 0) {
                    pageid = 1;
                }
                for (var p = 0; p <= 8; p++) {
                    if (pageid <= $scope.totalPage) {
                        $scope.activePages[p] = pageid;
                    }
                    pageid++;
                }

                $scope.data = response.data.Orgs;
                //console.log(response.data.Orgs);

                //dots ... and steps  system
                $scope.dotsPre = false;
                if (index >= 6) {
                    $scope.dotsPre = true;
                }
                $scope.dotsAft = false;
                if (index < $scope.totalPage - 5) {
                    $scope.dotsAft = true;
                }
                $scope.curPage = index;
                $scope.previous = false;
                if (index > 0) {
                    $scope.previous = true;
                    $scope.prePage = $scope.curPage - 1;
                }
                $scope.next = false;
                if (index < $scope.endPage - 1) {
                    $scope.next = true;
                    $scope.nextPage = $scope.curPage + 1;
                }

            }).catch(function (response, status) {
                //   console.log(response.status);
            });

        };
        $scope.PageSelected(0);// init default page

        $scope.CreateOrganisation = function () {
            // create object required for modal component to create input fields in modal
            var orgRowData = {
                orgName: '',
                webSite: '',
                isActive: true,
                lastUpdate: ''

            }
            // define options for modal component
            var modalOption = {
                modalTitle: 'Create Organisation',  // Modal tilte
                controller: 'admin', //Controller name 
                action: 'CreateOrganisation', //Action Name (Post)
                idVariable: 'Id', // ID of a table
                idValue: '', //nuallable, Route domain/controller/action/idValue
                httpPostConfig: {
                    headers: {
                        'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                    }
                }
            };

            $scope.$broadcast('showModelEvent', [$scope.toModalObject(orgRowData), modalOption]);

        };

        $scope.OrgActive = function (result) {
            // activate or activate org as required and update sql database
            if ($scope.data[result].isActive) {
                $scope.data[result].isActive = false;
            }
            else {
                $scope.data[result].isActive = true;
            }

            $scope.editOrg.WebsiteUrl = $scope.data[result].WebsiteUrl;
            $scope.editOrg.OrgName = $scope.data[result].OrgName;
            $scope.editOrg.Id = $scope.data[result].Id;
            $scope.editOrg.isActive = $scope.data[result].isActive;
            $http({
                method: "Post",
                url: "/Admin/EditOrganisation",
                data: $scope.editOrg,
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }).then(function SentOk(result) {

                console.log(result);
            }, function Error(result) {
                console.log(result);
            });
        };

        $scope.toModalObject = function (table) {
            var orderToModal = [
                    { title: 'Organisation', variableName: 'OrgName', value: table.orgName, type: 'text', validation: { required: true } },
                    { title: 'Website', variableName: 'WebsiteUrl', value: table.webSite, type: 'text', validation: { required: true }, regExpVaid: { text: 'Url Must contain http or https', reg: /http/ } },
                    { title: 'Active', variableName: 'isActive', value: table.isActive, type: 'text', hide: true}]; // this is hidden as it is just needed to valid model sent to controller
            return orderToModal;
        };

        $scope.showModal = function (index) {

            // create object for modal component
            var orgRowData = {
                orgName: $scope.data[index].OrgName,
                webSite: $scope.data[index].WebsiteUrl,
                isActive: $scope.data[index].isActive
            }
            // define options for modal component
            var modalOption = {
                modalTitle: 'Edit Organisation',  // Modal tilte
                controller: 'admin', //Controller name 
                action: 'EditOrganisation', //Action Name (Post)
                idVariable: 'Id', // ID of a table
                idValue: $scope.data[index].Id, //nuallable, Route domain/controller/action/idValue
                httpPostConfig: {
                    headers: {
                        'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                    }
                }
            };
            $scope.$broadcast('showModelEvent', [$scope.toModalObject(orgRowData), modalOption]);
        };

        $scope.$on('modelDone', function (event, data) {
            if (data[1]) {
                // if CreateOrg property set to create then new org was created so add to angularjs modal
                if (data[0].data.CreateOrg) {
                    var newRow = {
                        Id: data[0].data.Id,
                        OrgName: data[0].data.ModalData.OrgName,
                        WebsiteUrl: data[0].data.ModalData.WebsiteUrl,
                        lastUpdate: data[0].data.ModalData.lastUpdate,
                        isActive: true
                    }
                    $scope.data.push(newRow);  // add org to angular modal array
                } else {            // CreateOrg property is set to false so modal was used to edit org so update angular modal with changes
                    for (var i = 0; i < $scope.data.length; i++) {
                        if (data[0].data.ModalData.Id == $scope.data[i].Id) {

                            $scope.data[i].OrgName = data[0].data.ModalData.OrgName;
                            $scope.data[i].WebsiteUrl = data[0].data.ModalData.WebsiteUrl;
                            $scope.data[i].lastUpdate = data[0].data.ModalData.lastUpdate
                           
                        }

                    }
                }

            } else {
                console.log('error');
            }
        });
    }]);
})(angular);
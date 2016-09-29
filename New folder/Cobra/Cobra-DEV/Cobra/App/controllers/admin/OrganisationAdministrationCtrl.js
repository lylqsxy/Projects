﻿//Author: Craig Rabbitt
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

    cobraApp.controller('OrganisationAdministrationCtrl', ['$scope', '$http', '$window', 'orgServiceGet', function ($scope, $http, $window, orgServiceGet) {
        // Initializing scope
        cobraApp.run(['$http', function ($http) {
            $http.defaults.headers.common['X-XSRF-Token'] =
                angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value');
        }]);

        $scope.data = [];   // contains page data

        $scope.rowSelected;   // contains current row being edited/created
        $scope.editOrg = { Id: 0, OrgName: "", WebsiteUrl: "", isActive: "false" };  // used to post row to controller
        //$scope.org = { Id: 0, orgName: '', WebsiteUrl: '', isActive: true, lastUpdate: '' };


        $scope.editing = false;    // set to true when editing a row and prevents user editing another row before closing or saving current row


        //pagination By Tom
        //Load item per page request
        $scope.pageSizePicker = 25; // items per page

        $scope.disableButtons = true;
        $scope.activePages = [];
        //click function on pagination
        $scope.PageSelected = function (index) {
            console.log("clicked index", index);
            $scope.pageSkip = index * $scope.pageSizePicker;
            //param to server get method
            $scope.params = {

                displaySize: $scope.pageSizePicker,
                startRec: $scope.pageSkip
            };
            // pull data from service
            orgServiceGet.getData($scope.params).then(function (response) {
                console.log(response);

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
                console.log(response.status);
            });

        };
        $scope.PageSelected(0);// init default page

        $scope.CreateOrganisation = function () {
            // make a copy of first row to use as a template as the new row, then reset properties as required
            var temp = angular.copy($scope.data[0]);
            temp.OrgName = "New";
            temp.Id = -1;
            temp.WebsiteUrl = "";
            temp.lastUpdate = "";
            // add new row
            $scope.data.push(temp);
            $scope.editing = true;
            var result = $scope.data.length - 1;
            console.log(result);
            $scope.data[result].showEditBox = true;         // shows editing boxs on true
            $scope.data[result].showTextOnly = false;       // shows textonly on true
            $scope.data[result].showEditButton = false;     // shows edit button on true
            $scope.data[result].showSaveButton = true;      // shows save button on true
            $scope.data[result].closeEditButton = false;    // shows close button on true
            $scope.data[result].showCancelButton = true;    // shows cancel button on true
            $scope.rowSelected = result;

        };

        $scope.EditOrg = function (result) {
            // if no row being edeited already show input boxs and buttons to allow editing
            if ($scope.editing === false) {

                $scope.data[result].showEditBox = true;
                $scope.data[result].showTextOnly = false;
                $scope.data[result].showEditButton = false;
                $scope.data[result].showSaveButton = true;
                $scope.data[result].closeEditButton = true;
                $scope.rowSelected = result;
                // set to true so no other row can now be edited until editing is completed
                $scope.editing = true;

            }
        };

        $scope.CloseEdit = function () {
            // if close edit button is pressed hide and show elements as required. note nothinig is updated in sql database
            var result = $scope.rowSelected;
            $scope.data[result].showEditBox = false;
            $scope.data[result].showTextOnly = true;
            $scope.data[result].showEditButton = true;
            $scope.data[result].showSaveButton = false;
            $scope.data[result].closeEditButton = false;
            $scope.data[result].showCancelButton = false;
            $scope.editing = false;
        };

        $scope.CancelEdit = function () {
            // user canceled row add so remove row and hide elements as required
            var result = $scope.rowSelected;
            $scope.data[result].showEditBox = false;
            $scope.data[result].showTextOnly = true;
            $scope.data[result].showEditButton = true;
            $scope.data[result].showSaveButton = false;
            $scope.data[result].closeEditButton = false;
            $scope.data[result].showCancelButton = false;
            $scope.editing = false;
            $scope.data.splice($scope.rowSelected, 1);  // remove from array
            console.log($scope.data);

        };
        $scope.EditOkButton = function () {
            // save button has been pressed, update sql database and hide and show elements as required
            $scope.editOrg.WebsiteUrl = $scope.data[$scope.rowSelected].WebsiteUrl;
            $scope.editOrg.OrgName = $scope.data[$scope.rowSelected].OrgName;
            $scope.editOrg.Id = $scope.data[$scope.rowSelected].Id;
            $scope.editOrg.isActive = $scope.data[$scope.rowSelected].isActive;
            // if id is -1 the a new row is being added else update row as per id
            if ($scope.editOrg.Id !== -1) {      // update
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
            } // create
            else {
                $http({
                    method: "Post",
                    url: "/Admin/CreateOrganisation",
                    data: $scope.editOrg,
                    headers: {
                        'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                    }
                }).then(function SentOk(result) {
                    $scope.data[index].Id = result.data.Id;

                }, function Error(result) {
                    console.log(result);
                });
            }

            var index = $scope.rowSelected;
            $scope.data[index].showEditBox = false;
            $scope.data[index].showTextOnly = true;
            $scope.data[index].showEditButton = true;
            $scope.data[index].showSaveButton = false;
            $scope.data[result].closeEditButton = false;
            $scope.data[result].showCancelButton = false;
            $scope.editing = false;
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

    }]);
})(angular);
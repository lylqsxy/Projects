//helen and now Lavesh and now Craig
// 
//angular.module('angularTable', ['angularUtils.directives.dirPagination']);

(function () {
    //var app = angular.module('app-usermanagement', []);
    cobraApp.controller('admin/UserManagementCtrl', function ($scope, $http) {
        $scope.users = [];
        $scope.curPage = 1;
        $scope.pages = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        $scope.displaySize = 10;
        $scope.userCount = 0;
        $scope.User = "";
        $scope.totalUsers = 0;

        $scope.strict = false;

        $scope.sort = {
            field: "ID",
            reverseSort: false
        };
        $scope.filter = {
            field: "ALL",
            searchValue: ""
        };
       // $scope.DisplaySize = 25;
        $scope.maxPages = 0;

        //Lavesh 
        
        $scope.PopulateDB = function () {
            $http({
                method: 'POST',
                url: '/Admin/PopulateDB',

            }).then(function successCallback(response) {
                console.log(response)
            }, function errorCallback(response) {
                console.log('error')
            })
        }
        $scope.ToggleActive = function (user) {
            var message = (!user.IsActive ? " Activate User?" : " Deactivate User?");
            var proceed = confirm("Please Confirm. " + message)
            if (proceed == true) {
                user.IsActive = !user.IsActive
                $http({
                    method: 'POST',
                    url: '/Admin/ToggleActive',
                    data: user
                }).then(function successCallback(response) {
                    console.log(response)
                }, function errorCallback(response) {
                    console.log('error')
                })

            }
        }

        $scope.applySort = function (sort, filter) {
            // var displaySize=20;
            var startRec = (($scope.curPage * $scope.displaySize) - $scope.displaySize);
            var sf = $scope.sort.field;
            var rs = $scope.sort.reverseSort;
            console.log("FromSort");
            console.log(startRec, $scope.curPage, $scope.displaySize);
            var sFilter = JSON.stringify(filter);
            var sSort = JSON.stringify(sort);
            console.log(sFilter, sSort);
            $http({
                method: 'GET',
                url: '/Admin/GetUserManagement',
                params: {

                    startRec: startRec, displaySize: $scope.displaySize, sortBy: sort.field, reverseSort: $scope.sort.reverseSort, sort: sSort, filterBy: sFilter
                }

            }).then(function successCallback(response) {
                console.log("FromSort");
                console.log(response);
                console.log("hi");
                console.log(response.data);
                $scope.users = response.data;

            }, function errorCallback(response) {
                console.log('error');
            });
        }
        
        $scope.ToggleLockout = function (user) {
            //alert("ToggleLockout Called");
            //console.log(user.IsActive)
            user.LockoutEnabled = !user.LockoutEnabled
            $http({
                method: 'POST',
                url: '/Admin/ToggleUserLockout',
                data: user
            }).then(function successCallback(response) {
                console.log(response)
            }, function errorCallback(response) {
                console.log('error')
            })
        }
        $scope.ToggleEmailConfirmed = function (user) {

            user.EmailConfirmed = !user.EmailConfirmed;
            $http({
                method: 'POST',
                url: '/Admin/ToggleEmailConfirmed',
                data: user
            }).then(function successCallback(response) {
                console.log(response)
            }, function errorCallback(response) {
                console.log('error')
            })
        }
        $scope.TogglePhoneNumberConfirmed = function (user) {
            user.PhoneNumberConfirmed = !user.PhoneNumberConfirmed;
            $http({
                method: 'POST',
                url: '/Admin/TogglePhoneNumberConfirmed',
                data: user
            }).then(function successCallback(response) {
                console.log(response)
            }, function errorCallback(response) {
                console.log('error')
            })
        }
        $scope.UpdateEmail = function (user) {
            $http({
                method: 'POST',
                url: '/Admin/UpdateEmail',
                data: user
            }).then(function successCallback(response) {
                console.log(response)
            }, function errorCallback(response) {
                console.log('error')
            })
        }
        $scope.UpdatePhone = function (user) {
            $http({
                method: 'POST',
                url: '/Admin/UpdatePhone',
                data: user
            }).then(function successCallback(response) {
                console.log(response)
            }, function errorCallback(response) {
                console.log('error')
            })
        }
        $scope.UpdateFailedAccessCount = function (user) {
            $http({
                method: 'POST',
                url: '/Admin/UpdateFailedAccessCount',
                data: user
            }).then(function successCallback(response) {
                console.log(response)
            }, function errorCallback(response) {
                console.log('error')
            })
        }
        $scope.SortView = function () {
            function compare(a, b) {
                if (a.Username < b.Username)
                    return -1;
                if (a.Username > b.Username)
                    return 1;
                return 0;
            }
            $scope.users.sort(compare);
        }

        // Craig
        $scope.PageSelected = function (page, search, sort) {
            sort = sort || "";
            
            var recordOffSet = 0;

            if ( page > 1 )
            {
                recordOffSet = page * $scope.displaySize;
            }
             
            $scope.curPage = page;
            
            $http({
                method: 'GET',
                url: '/Admin/GetUserManagement',
                params: { startRec: recordOffSet, displaySize: $scope.displaySize, sortBy: sort.field, reverseSort: sort.reverseSort, sort: "", filterBy: search }
            }).then(function successCallback(response) {
                
                $scope.users = response.data.users;
                $scope.userCount = $scope.users.length; // number of records returned from server
                $scope.totalUsers = response.data.totalUsers;
                $scope.maxPages = $scope.totalUsers / $scope.displaySize;

                $scope.maxPages = 27;
                $scope.pages.length = 0;
                    if (page > 4) {
                        var pageid = page - 4;
                    }
                    else {
                        var pageid = 1;
                    };

                    if (pageid == 0) {
                        pageid = 1;
                    }

                    for (var p = 0; p <= 9 ; p++) {
                        if (pageid <= $scope.maxPages) {
                             $scope.pages[p] = pageid;
                        }                      
                        pageid++;
                    }
                
                
            }, function errorCallback(response) {
                console.log('error');
                console.log(response);
            });
        };

        // get page data when view first shown
        $scope.PageSelected(1, '');
    });


})(angular);

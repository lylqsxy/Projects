'use strict';

(function () {
    //cobraApp.run([
    //    '$rootScope', '$window', function($rootScope, $window) {
    //        //$rootScope.status = false;
    //        $window.fbAsyncInit = function() {
    //            FB.init({
    //                appId: '1154958857883093', // Set YOUR APP ID
    //                status: true, // check login status
    //                cookie: true, // enable cookies to allow the server to access the session
    //                xfbml: true // parse XFBML
    //            });

    //            FB.Event.subscribe('auth.authResponseChange', function(response) {
    //                if (response.status === 'connected') {
    //                    $rootScope.status = true;
    //                } else if (response.status === 'not_authorized') {
    //                    $rootScope.status = false;
    //                } else {
    //                    $rootScope.status = false;
    //                }
    //            });

    //        };
    //        (function(d) {
    //            var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
    //            if (d.getElementById(id)) {
    //                return;
    //            }
    //            js = d.createElement('script');
    //            js.id = id;
    //            js.async = true;
    //            js.src = "//connect.facebook.net/en_US/all.js";
    //            ref.parentNode.insertBefore(js, ref);
    //        }(document));
    //    }
    //]);

    cobraApp.controller('SocialMedaiCtrl', function ($scope, $timeout, $http) {
        $scope.status = false;
        $scope.Login = function () {
            $http({
                method: 'POST',
                url: '/Account/ExternalLogin',
                params: {
                    provider: 'Facebook',
                    returnUrl: null
                }
            }).then(function (response) {
                var result = response.data;
                if (result.Success === true) {
                    window.location.href = result.RedirectUrl;
                } else { // login is fail

                }
            }, function (response) { // when server is offline or not response
                alert("Error. \nPlease contact customer service for further assistnces")
            });

            //FB.login(function (response) {
            //    if (response.authResponse) {                    
            //        FB.api('/me', { fields: 'id, name, email, picture,first_name,last_name,gender,birthday' }, function (response) {
            //            console.log(JSON.stringify(response));

            //        });
            //        $scope.status = true;

            //        //$http({
            //        //    method: 'POST',
            //        //    url: '/Account/ExternalLogin',
            //        //    params: {
            //        //        provider: 'Facebook',
            //        //        returnUrl: null
            //        //    }
            //        //}).then(function (response) {
            //        //    var result = response.data;
            //        //    if (result.Success === true) {
            //        //        window.location.href = result.RedirectUrl;
            //        //    } else { // login is fail

            //        //    }
            //        //}, function (response) { // when server is offline or not response
            //        //    alert("Error. \nPlease contact customer service for further assistnces")
            //        //});
            //    } else {
            //        $scope.status = false;
            //        console.log('User cancelled login or did not fully authorize.');
            //    }
            //});
        };

        $scope.getPhoto = function () {
            FB.api('/me/picture?type=normal', function (response) {
                var str = "<br/><b>Pic</b> : <img src='" + response.data.url + "'/>";
                document.getElementById("status").innerHTML += str;
            });
        };

        $scope.Logout = function () {
            FB.logout(function () { document.location.reload(); });
            $scope.status = false;
            console.log($scope.status);
        }
    });

    cobraApp.run(['$http', function ($http) {
        $http.defaults.headers.common['X-XSRF-Token'] = angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value');
    }]);
})();

//function getUserInfo() {
//    var resp;
//    FB.api('/me', { fields: 'id, name, email, picture,first_name,last_name,gender,birthday' }, function (response) {
//        //console.log(JSON.stringify(response));
//        //var str = "<b>Name</b> : " + response.first_name + " " + response.last_name + "<br>";
//        //str += "<b>Link: </b>" + response.link + "<br>";
//        //str += "<b>Username:</b> " + response.name + "<br>";
//        //str += "<b>Id: </b>" + response.id + "<br>";
//        //str += "<b>Email:</b> " + response.email + "<br>";
//        //str += "<b>Gender:</b> " + response.gender + "<br>";
//        //document.getElementById("status").innerHTML = str;
//        resp = response;
//    });
//    return resp;
//}



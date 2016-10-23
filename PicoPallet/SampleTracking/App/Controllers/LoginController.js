(function () {
    'use strict';

    PecoApp.controller('LoginController', function ($scope, $location, authService) {
        $scope.loginData = {
            USER_Name: "",
            USER_Password: ""
        };

        $scope.message = "";
        $scope.login = function () {

            authService.login($scope.loginData).then(function (response) {
                if (response.IsAuthorized && response.IsAdmin) {
                    $location.path('/');
                }
                else {
                    $scope.message = response.Message;
                }
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
        //$scope.login = function (account) {
        //    Account.getUserValidate({ username: account.username, password: account.password })
        //    .then(function (response) {
        //        console.log('ss');
        //        SessionService.token = response.access_token;
        //        $location.path('/');
        //    }, function (response) {
        //        $scope.loginForm.errorMessage = response.error_description;
        //    });
        //}
    });

    PecoApp.controller('RegisterController', function ($scope, LoginFactory, RegisterFactory, SessionService) {
        $scope.registerForm = {
            username: undefined,
            password: undefined,
            confirmPassword: undefined,
            errorMessage: undefined
        };

        $scope.register = function () {
            RegisterFactory($scope.registerForm.username, $scope.registerForm.password, $scope.registerForm.confirmPassword)
            .then(function () {
                LoginFactory($scope.registerForm.username, $scope.registerForm.password)
                .then(function (response) {
                    SessionService.token = response.access_token;
                }, function (response) {
                    $scope.registerForm.errorMessage = response;
                });
            }, function (response) {
                $scope.registerForm.errorMessage = response;
            });
        }
    });
})();
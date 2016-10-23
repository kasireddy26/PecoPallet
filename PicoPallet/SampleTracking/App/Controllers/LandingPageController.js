'use strict';
PecoApp.controller('LandingPageController123', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    console.log('I am  LandingPageController');
    $scope.main = {
        brand: ''
    };
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
    }

    $scope.authentication = authService.authentication;

}]);

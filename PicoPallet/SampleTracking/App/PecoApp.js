'use strict';
var httpHeaders;
var serviceBase = 'http://localhost:13061/';
var gPaperPageTitle = "HOME";

var PecoApp = angular.module('PecoApp', ['ngResource', 'ngRoute', 'ui.bootstrap']);

PecoApp
    .config(function ($routeProvider, $httpProvider) {
        $routeProvider
        .when('/', {
            templateUrl: '/App/Dashboard/dashboard.html',
            controller: 'DashboardController'
        })
        .when('/home2', {
            templateUrl: '/App/testhome2.html',
            controller: 'testhome2Controller'
        })
        .when('/charts', {
            templateUrl: '/App/Charts/charts.html',
            controller: 'ChartsController'
        })
        .when('/devicemngr', {
            templateUrl: '/App/DeviceManager/deviceManager.html',
            controller: 'DeviceMngrController'
        })
        .when('/notifications', {
            templateUrl: '/App/Maps/notification.html',
            controller: 'NotificationController'
        })
        .when('/routemap', {
            templateUrl: '/App/Maps/routemap.html',
            controller: 'RouteMapController'
        })
        .when('/pinbyzip', {
            templateUrl: '/App/Maps/pushpinbyZip.html',
            controller: 'PushpinbyZipController'
        })

        .when('/home1', {
            templateUrl: 'Templates/home1.html',
            controller: 'LandingPageController123'
        })
        .when('/geofence', {
            templateUrl: 'App/Maps/GeoFence.tmpl.html',
            controller: 'GeoFenceController'
        })
        .when('/login', {
            templateUrl: 'Templates/login.html',
            controller: 'LoginController'
        })
        .when('/register', {
            templateUrl: 'Templates/register.html',
            controller: 'RegisterController'
        });
        httpHeaders = $httpProvider.defaults.headers;
    });

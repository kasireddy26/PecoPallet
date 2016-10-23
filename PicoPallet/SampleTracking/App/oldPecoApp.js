'use strict';
var httpHeaders;
var serviceBase = 'http://localhost:13061/';
var gPaperPageTitle = "HOME";

var PecoApp = angular.module('PecoApp',
    ['tmh.dynamicLocale', 'ngResource', 'ngRoute', 'ngCookies', 'LocalStorageModule',
        'angular-loading-bar', 'pascalprecht.translate', 'ui.bootstrap']);

PecoApp
    .config(function ($routeProvider, $httpProvider, $translateProvider, tmhDynamicLocaleProvider) {
        $routeProvider
        .when('/', {
            templateUrl: 'Templates/home.html',
            controller: 'LandingPageController'
            //templateUrl: 'Templates/maps.html',
            //controller: 'MapsController'
        })
        .when('/home1', {
            templateUrl: 'Templates/home1.html',
            controller: 'LandingPageController123'
        })
        .when('/notifications', {
            templateUrl: 'Templates/maps.html',
            controller: 'MapsController'
        })
        .when('/charts', {
            templateUrl: 'Templates/charts.html',
            controller: 'ChartsController'
        })
        .when('/routemap', {
            templateUrl: 'Templates/routemap.html',
            controller: 'RouteMapsController'
        })
        .when('/devicemngr', {
            templateUrl: 'Templates/deviceManager.html',
            controller: 'DeviceMngrController'
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

PecoApp.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});
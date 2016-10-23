'use strict';
PecoApp.factory('authService', ['$location', '$http', '$q', 'localStorageService', 'ngAuthSettings', function ($location, $http, $q, localStorageService, ngAuthSettings, $httpProvider) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        isAdmin:false,
        userName: "",
        DispName: "",
        useRefreshTokens: false
    };

    var _externalAuthData = {
        provider: "",
        userName: "",
        externalAccessToken: ""
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post( '/Account/Register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = "grant_type=USER_Password&username=" + loginData.USER_Name + "&password=" + loginData.USER_Password;

        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + ngAuthSettings.clientId;
        }

        var deferred = $q.defer();
      
        
        $http.post('/Account/Authenticate', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }, { isArray: true })
            .success(function (response, status, headers,config) {
                
                var results = [];
                results.data = response;
                results.headers = headers();
                results.status = status;
                results.config = config;
                var AuthToken = results.headers["auth-token"];
            if (loginData.useRefreshTokens) {
                localStorageService.set('authorizationData', { token: AuthToken, userName: loginData.userName, refreshToken: AuthToken, useRefreshTokens: true });
            }
            else {
                localStorageService.set('authorizationData', { token: AuthToken, userName: loginData.userName, refreshToken: "", useRefreshTokens: false });
            }
           
            _authentication.DispName = results.data.DisplayName;
            //_authentication.useRefreshTokens = loginData.useRefreshTokens;
            _authentication.isAuth = true;

           // if (_authentication.isAuth != true) { _logOut(); }
           // if (_authentication.isAdmin != true) { }
                //deferred.resolve(response);

            $location.path('/');

        }).error(function (err, status) {
            _authentication.isAuth = false;
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.DispName = "";
        _authentication.useRefreshTokens = false;

    };
    
    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.authentication = _authentication;
    return authServiceFactory;
}]);



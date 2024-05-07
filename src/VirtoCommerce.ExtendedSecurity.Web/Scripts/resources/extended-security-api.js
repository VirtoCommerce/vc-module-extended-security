angular.module('ExtendedSecurity')
    .factory('ExtendedSecurity.webApi', ['$resource', function ($resource) {
        return $resource('api/extended-security');
    }]);

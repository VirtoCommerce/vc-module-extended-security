angular.module('VirtoCommerce.ExtendedSecurity')
    .factory('VirtoCommerce.ExtendedSecurity.webApi', ['$resource', function ($resource) {
        return $resource('api/extended-security');
    }]);

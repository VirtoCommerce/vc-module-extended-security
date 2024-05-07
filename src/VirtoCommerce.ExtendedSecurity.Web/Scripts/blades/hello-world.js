angular.module('ExtendedSecurity')
    .controller('ExtendedSecurity.helloWorldController', ['$scope', 'ExtendedSecurity.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'ExtendedSecurity';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'ExtendedSecurity.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);

// Call this to register your module to main application
var moduleName = 'ExtendedSecurity';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.ExtendedSecurityState', {
                    url: '/ExtendedSecurity',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'ExtendedSecurity.helloWorldController',
                                template: 'Modules/$(VirtoCommerce.ExtendedSecurity)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/ExtendedSecurity',
                icon: 'fa fa-cube',
                title: 'ExtendedSecurity',
                priority: 100,
                action: function () { $state.go('workspace.ExtendedSecurityState'); },
                permission: 'ExtendedSecurity:access',
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);

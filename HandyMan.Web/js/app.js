angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:60406/api/');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');
    $urlRouterProvider.otherwise('home');
    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('register', { url: '/register', tmeplateUrl: '/templates/register/register.html', controller: 'RegisterController' })
        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});
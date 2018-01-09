/// <reference path="../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('cdynews', ['cdynews.common', 'cdynews.post_categories']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('home', {
                url: "/home",
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/home');
    }
})();
(function () {
    angular.module('cdynews.post_categories', ['cdynews.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('post_categories', {
            url: "/post_categories",
            templateUrl: "/app/components/post_categories/postCategoryListView.html",
            controller: "postCategoryListController"
        }).state('add_post_categories', {
            url: "/add_post_categories",
            templateUrl: "/app/components/post_categories/postCategoryAddView.html",
            controller: "postCategoryAddController"
        }).state('edit_post_categories', {
            url: "/edit_post_categories/:id",
            templateUrl: "/app/components/post_categories/postCategoryEditView.html",
            controller: "postCategoryEditController"
        });
    }
})();
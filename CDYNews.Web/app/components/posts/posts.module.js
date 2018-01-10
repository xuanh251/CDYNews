(function () {
    angular.module('cdynews.posts', ['cdynews.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('posts', {
                url: "/posts",
                templateUrl: "/app/components/posts/postListView.html",
                controller: "postListController"
            }).state('add_posts', {
                url: "/add_posts",
                templateUrl: "/app/components/posts/postAddView.html",
                controller: "postAddController"
            }).state('edit_post', {
                url: "/edit_post/:id",
                templateUrl: "/app/components/posts/postEditView.html",
                controller: "postEditController"
            });
    }
})();
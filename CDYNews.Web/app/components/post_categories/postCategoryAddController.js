/// <reference path="../../shared/services/notificationservice.js" />
/// <reference path="../../../assets/admin/libs/moment/moment.js" />
/// <reference path="../../shared/services/apiservice.js" />
(function (app) {
    app.controller('postCategoryAddController', postCategoryAddController);
    postCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']
    function postCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.postCategory = {
            Status: true,
            HomeFlag: true,
        }
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name);
        }
        $scope.AddPostCategory = AddPostCategory;
        function AddPostCategory() {
            apiService.post('/api/postcategory/create', $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm vào!');
                $state.go('post_categories');
            }, function (error) {
                notificationService.displayError('Có lỗi xảy ra, chưa thêm được!');
            });
        }
        function loadParentCategory() {
            apiService.get('/api/postcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Can't load parentCategory!")
            })
        }
        loadParentCategory();
    }
})(angular.module('cdynews.post_categories'))
/// <reference path="../../shared/services/notificationservice.js" />
/// <reference path="../../../assets/admin/libs/moment/moment.js" />
/// <reference path="../../shared/services/apiservice.js" />
(function (app) {
    app.controller('postCategoryEditController', postCategoryEditController);
    postCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService']
    function postCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name);
        }
        $scope.UpdatePostCategory = UpdatePostCategory;
        function loadPostCategoryDetail() {
            apiService.get('/api/postcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.postCategory = result.data;
            }, function (error) {
                notificationService.displayError(error);
            });
        }
        function UpdatePostCategory() {
            apiService.put('/api/postcategory/update', $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công!');
                $state.go('post_categories');
            }, function (error) {
                notificationService.displayError('Có lỗi xảy ra, cập nhật không thành công!');
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
        loadPostCategoryDetail();
    }
})(angular.module('cdynews.post_categories'))
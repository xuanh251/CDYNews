/// <reference path="../../shared/services/notificationservice.js" />
/// <reference path="../../../assets/admin/libs/moment/moment.js" />
/// <reference path="../../shared/services/apiservice.js" />
(function (app) {
    app.controller('postEditController', postEditController);
    postEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService']
    function postEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
        }
        $scope.UpdatePost = UpdatePost;
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.post.Image = fileUrl;
                })
            }
            finder.popup();
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px',
            uiColor: '#ffffff',
            toolbar: 'Full'
        }
        function loadPostDetail() {
            apiService.get('/api/post/getbyid/' + $stateParams.id, null, function (result) {
                $scope.post = result.data;
            }, function (error) {
                notificationService.displayError(error);
            });
        }
        function UpdatePost() {
            apiService.put('/api/post/update', $scope.post, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công!');
                $state.go('posts');
            }, function (error) {
                notificationService.displayError('Có lỗi xảy ra, cập nhật không thành công!');
            });
        }
        function loadParent() {
            apiService.get('/api/post/getallparents', null, function (result) {
                $scope.parents = result.data;
            }, function () {
                console.log("Can't load parent!")
            })
        }
        loadParent();
        loadPostDetail();
    }
})(angular.module('cdynews.posts'))
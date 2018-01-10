/// <reference path="../../shared/services/notificationservice.js" />
/// <reference path="../../../assets/admin/libs/moment/moment.js" />
/// <reference path="../../shared/services/apiservice.js" />

(function (app) {
    app.controller('postAddController', postAddController);
    postAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']
    function postAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.post = {
            Status: true,
            HomeFlag: true,
        }
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
            toolbar:'Full'
        }
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
        }
        $scope.AddPost = AddPost;
        function AddPost() {
            $scope.post.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/post/create', $scope.post, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm vào!');
                $state.go('posts');
            }, function (error) {
                notificationService.displayError('Có lỗi xảy ra, chưa thêm được!');
            });
        }
        function loadParent() {
            apiService.get('/api/postcategory/getallparents', null, function (result) {
                $scope.postCategories = result.data;
            }, function () {
                console.log("Can't load parent!")
            })
        }
        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }
        loadParent();
    }
})(angular.module('cdynews.posts'))
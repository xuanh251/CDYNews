(function (app) {
    app.controller('postListController', postListController);
    postListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function postListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        $scope.posts = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getListPosts = getListPosts;
        $scope.keyword = '';
        $scope.CategoryID = 0;
        $scope.search = search;
        $scope.deletePost = deletePost;
        $scope.isAll = false;
        $scope.selectAll = selectAll;
        $scope.deleteMulti = deleteMulti;
        function deleteMulti() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            })
            var config = {
                params: {
                    listId: JSON.stringify(listId),
                }
            }
            apiService.del('/api/post/deletemulti', config, function (result) {
                notificationService.displaySuccess('Đã xoá thành công ' + result.data + ' bản ghi.');
                getListPosts();
            }, function (error) {
                notificationService.displayError('Xảy ra lỗi, chưa xoá được.');
            })
        }
        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.posts, function (item) {
                    item.checked = true;
                })
                $scope.isAll = true;
            } else {
                angular.forEach($scope.posts, function (item) {
                    item.checked = false;
                })
                $scope.isAll = false;
            }
        }
        function loadParent() {
            apiService.get('/api/postcategory/getallparents', null, function (result) {
                $scope.postCategories = result.data;
            }, function () {
                console.log("Can't load parent!")
            })
        }
        $scope.$watch('posts', function (newVal, oldVal) {
            var checked = $filter('filter')(newVal, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deletePost(id) {
            $ngBootbox.confirm('Bạn chắc chắn muốn xoá?').then(function () {
                var config = {
                    params: {
                        id: id,
                    }
                }
                apiService.del('/api/post/delete', config, function () {
                    notificationService.displaySuccess('Xoá thành công!');
                    getListPosts();
                }, function () {
                    notificationService.displayError('Xoá không thành công!');
                })
            })
        }
        function search() {
            getListPosts();
        }

        function getListPosts(page) {
            page = page || 0; //nếu bằng null thì thay bằng 0
            $scope.CategoryID = $scope.CategoryID || 0;
            var config = {
                params: {
                    postCategoryID: $scope.CategoryID,
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }

            apiService.get('/api/post/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy bản ghi nào!')
                }
                else {
                    notificationService.displaySuccess('Tìm thấy ' + result.data.TotalCount + ' bản ghi.')
                }
                $scope.posts = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;

            }, function () {
                console.log("Load post failed!");
            });
        }
        $scope.getListPosts();
        loadParent();
    }

})(angular.module('cdynews.posts'));

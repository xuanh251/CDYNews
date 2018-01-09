(function (app) {
    app.controller('postCategoryListController', postCategoryListController);
    postCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function postCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        $scope.postCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getListPostCategories = getListPostCategories;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deletePostCategory = deletePostCategory;
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
            apiService.del('/api/postcategory/deletemulti', config, function (result) {
                notificationService.displaySuccess('Đã xoá thành công ' + result.data + ' bản ghi.');
                getListPostCategories();
            }, function (error) {
                notificationService.displayError('Xảy ra lỗi, chưa xoá được.');
            })
        }
        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.postCategories, function (item) {
                    item.checked = true;
                })
                $scope.isAll = true;
            } else {
                angular.forEach($scope.postCategories, function (item) {
                    item.checked = false;
                })
                $scope.isAll = false;
            }
        }
        $scope.$watch('postCategories', function (newVal, oldVal) {
            var checked = $filter('filter')(newVal, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deletePostCategory(id) {
            $ngBootbox.confirm('Bạn chắc chắn muốn xoá?').then(function () {
                var config = {
                    params: {
                        id: id,
                    }
                }
                apiService.del('/api/postcategory/delete', config, function () {
                    notificationService.displaySuccess('Xoá thành công!');
                    getListPostCategories();
                }, function () {
                    notificationService.displayError('Xoá không thành công!');
                })
            })
        }
        function search() {
            getListPostCategories();
        }

        function getListPostCategories(page) {
            page = page || 0; //nếu bằng null thì thay bằng 0
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }

            apiService.get('/api/postcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy bản ghi nào!')
                }
                else {
                    notificationService.displaySuccess('Tìm thấy ' + result.data.TotalCount + ' bản ghi.')
                }
                $scope.postCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;

            }, function () {
                console.log("Load post category failed!");
            });
        }
        $scope.getListPostCategories();
    }

})(angular.module('cdynews.post_categories'));

/// <reference path="notificationservice.js" />
(function (app) {
    app.factory('apiService', apiService);
    apiService.$inject = ['$http', 'notificationService']
    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }
        function del(url, data, success, failure) {
            $http.delete(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Yêu cầu đăng nhập!');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }
        function post(url, data, success, failure) {
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Yêu cầu đăng nhập!');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }
        function put(url, data, success, failure) {
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Yêu cầu đăng nhập!');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }
        function get(url, para, success, failure) {
            $http.get(url, para).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }
    };
    app.controller('datCtrl', function ($scope) {
        $scope.today = new Date();
    });
})(angular.module('cdynews.common'))
(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            return input == true ? 'Kích hoạt' : 'Khoá';
        }
    });
})(angular.module('cdynews.common'));
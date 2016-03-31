angular.module('app').controller('AppController', function ($scope, $location, $anchorScroll) {
    $scope.scrollTo = function (anchor) {
        $location.hash(anchor);

        $anchorScroll();
    }
});
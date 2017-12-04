'use strict';
angular.module('tuatara', [
        'xeditable',
        'tuatara.core',
        'projects',
        'workforce',
        'playbook',
        'ngRoute'])
        .config(['$locationProvider', '$routeProvider', function config($locationProvider, $routeProvider) {
           $locationProvider.hashPrefix('');

            $routeProvider
              .when('/projects', {
                  template: '<projects-list></projects-list>'
              })
              .when('/playbook/:weekShift', {
                  template: '<pb-list></pb-list>'
              })
              .when('/resources', {
                  template: '<workforce-list></workforce-list>'
              })
              .otherwise({
                  template: '<p>choose something!</p>'
              })
            ;
        }])
//.run(function (editableOptions) {
//    editableOptions.theme = "bs3"; // for xeditable http://vitalets.github.io/angular-xeditable/
//});



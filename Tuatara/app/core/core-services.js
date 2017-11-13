/*
 * Core services, shared by all modules. Mostly resources (REST) 
 */

(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    angular.module('tuatara.core', ['ngResource'])
       .factory('core.workItems', ['$resource', function ($resource) {
           return $resource('/api/projects/:id',
               { id: '@id' },               
               {
                   'update': {  // angularjs will add $ to update
                       method: 'PUT', // put not supported by default actions in ngresource
                   }
               }
           );
       }])        
       .factory('core.workResources', ['$resource', function ($resource) {
           return $resource('/api/resources/:id',
               { id: '@id' },
               {
                   'update': {  // angularjs will add $ to update
                       method: 'PUT', // put not supported by default actions in ngresource
                   }
               }
           );
       }])
    ;

})();
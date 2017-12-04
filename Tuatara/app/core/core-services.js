/*
 * Core services, shared by all modules. Mostly resources (REST) 
 */

(function () {
    'use strict';
    let Tuatara = (window.Tuatara || (window.Tuatara = {}));

    angular.module('tuatara.core', ['ngResource'])
        // projects CRUD REST, just a thin wrapper around resources
       .factory('projectsService', ['$resource', function ($resource) { return $resource('/api/projects/:id', { id: '@id' }); }])
        // workforce CRUD REST, just a thin wrapper around resources
       .factory('workforceService', ['$resource', function ($resource) { return $resource('/api/workforce/:id', { id: '@id' }); }])
        // userConfirmation asks user before calling ACTION
       .factory('userConfirmation', function () {
           return function (action, question) {
               if (confirm(question || "Are you sure")) {
                   action();
               }
           }
       })
        // standard service error parsing
        .factory('errorParser', ['$log', function () {
            function parsedError(error) {
                let _this = this;
                this.source = error;
                this.message = error.message;
                this.exceptionType = error.exceptionType;
                this.exceptionMessage = error.exceptionMessage;
                this.innerMessage = error.data ? error.data.message : undefined;

                this.getAll = function () {
                    let text = [];
                    if (_this.message) text.push(_this.message);
                    if (_this.exceptionType) text.push(_this.exceptionType);
                    if (_this.exceptionMessage) text.push(_this.exceptionMessage);
                    if (_this.innerMessage) text.push(_this.innerMessage);
                    var result = text.join("\n");
                    return result;
                }

                this.getMessage = function (defaultMessage) {
                    var message = defaultMessage ? defaultMessage : _this.source.toString();
                    return _this.message ? _this.message :
                        (_this.innerMessage ? _this.innerMessage : (
                            _this.exceptionMessage ? _this.exceptionMessage : message));
                }
            };

            return function (errorObject) {
                return new parsedError(errorObject);
            }
        }])
        // default resource method names
       .config(['$resourceProvider', function ($resourceProvider) {
           $resourceProvider.defaults.actions = { // angularjs will add $ to all actions in all resources
               create: { method: 'POST' },
               get: { method: 'GET' },
               query: { method: 'GET', isArray: true },
               update: { method: 'PUT' },
               remove: { method: 'DELETE' }
           };
       }]);
})();
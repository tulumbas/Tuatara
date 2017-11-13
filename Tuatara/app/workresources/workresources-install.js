(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    angular.module('workresources', ['ui.bootstrap','tuatara.core'])
        .component('workResources', {
            controller: ['core.workresources', '$uibModal', '$log', Tuatara.workresourcesController],
            controllerAs: 'v',
            templateUrl: '/app/workresources/workresources.html'
        });

})();
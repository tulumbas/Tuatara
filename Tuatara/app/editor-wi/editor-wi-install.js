(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    angular.module('editor-wi', ['tuatara.core', 'ui.bootstrap'])
        .component('editorWi', {
            controller: ['core.workItems', '$uibModal', '$log', Tuatara.editorwiController],
            controllerAs: 'v',
            templateUrl: '/app/editor-wi/editor-wi.html'
        });
})();
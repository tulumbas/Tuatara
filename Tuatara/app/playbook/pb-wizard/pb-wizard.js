(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.pbWizardController = function(playbookService, userConfirmation, $log, $scope, $uibModalInstance) {

    }

})();


angular.module('playbook')
    .component('pbWizard', {
        controller: ['playbookService', 'userConfirmation', '$log', '$scope', Tuatara.pbWizardController],
        templateUrl: '/app/playbook/pb-wizard/pb-wizard.html'
    });
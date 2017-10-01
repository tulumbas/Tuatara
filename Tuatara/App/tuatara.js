angular.module("tuatara", ["ui.bootstrap", "xeditable"])
    .service("playBookService", ["$http", "$log", "$uibModal", Tuatara.playbookService])
    .component("playbook", {
        controller: ["playBookService", "$log", "$scope", Tuatara.playbookController],
        controllerAs: "v",
        templateUrl: "/app/playbook/root.html"
    })
    .directive("playbookRow", function() {
        return {
            restrict: "A",
            controller: ["playBookService", "$log", "$scope", Tuatara.playbookRowController],
            controllerAs: "r",
            templateUrl: "/app/playbook/row/row.html"
        }
    })
    .component("playbookRowEditor", {
        controller: ["playBookService", "$log", "$scope", Tuatara.playbookRowEditorController],
        controllerAs: "editor",
        templateUrl: "/app/playbook/row/roweditor.html",
        bindings: {
            resolve: '<',
            close: '&',
            dismiss: '&'
        }
    })
    .run(function(editableOptions) {
        editableOptions.theme = "bs3"; // for xeditable http://vitalets.github.io/angular-xeditable/
    })
;

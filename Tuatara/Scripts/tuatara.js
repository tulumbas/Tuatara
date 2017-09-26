angular.module("tuatara", ["ui.bootstrap", "xeditable"])
    .service("playBookService", ["$http", "$log", "$uibModal", Tuatara.playbookService])
    .component("playbook", {
        controller: ["playBookService", "$log", "$scope", Tuatara.playbookController],
        controllerAs: "v",
        templateUrl: "/content/templates/playbook/root.html"
    })
    .directive("playbookRow", function() {
        return {
            restrict: "A",
            controller: ["playBookService", "$log", "$scope", Tuatara.playbookRowController],
            controllerAs: "r",
            templateUrl: "/content/templates/playbook/row.html"
        }
    })
    .component("playbookRowEditor", {
        controller: ["playBookService", "$log", Tuatara.playbookRowEditorController],
        controllerAs: "editor",
        templateUrl: "/content/templates/playbook/roweditor.html",
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

angular.module("tuatara", ["xeditable"])
    .service("playBookService", ["$http", Tuatara.playbookService])
    .component("playbook", {
        controller: ["playBookService", "$log", Tuatara.playbookController],
        controllerAs: "v",
        templateUrl: "/content/templates/playbook/root.html",
    })
    .directive("playbookRow", function() {
        return {
            restrict: "A",
            controller: ["playBookService", "$log", "$scope", Tuatara.playbookRowController],
            controllerAs: "r",
            templateUrl: "/content/templates/playbook/row.html",
            // property rowSource is already bound by a parent ng-repeat
            bindings: {
                rowData: '<pbRowSource'
            }
        }
    })
    .run(function(editableOptions) {
        editableOptions.theme = "bs3"; // for xeditable http://vitalets.github.io/angular-xeditable/
    })
;
    


(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));
    (function () { let mocks = Tuatara.mocks || (Tuatara.mocks = {}) })();

    Tuatara.mocks.uibModal = function (jasmine, $injector) {
        var _this = this;
        var $q = $injector.get("$q");
        var $rootScope = $injector.get("$rootScope");
        var uibModalResult = $q.defer();
        this.openSpy = jasmine.createSpy('uibmodal_open').and.returnValue({ result: uibModalResult.promise });

        this.uib = {
            open: this.openSpy
        };

        this.resolve = function (result) {
            uibModalResult.resolve({ status: { $value: { data: result } } });
            // make angular resolve the promise
            $rootScope.$apply();
        }

        this.reject = function () {
            uibModalResult.reject();
            // make angular resolve the promise
            $rootScope.$apply();
        }

        this.getOpenArgs = function () {
            return _this.openSpy.calls.mostRecent().args;
        }
    }
})();
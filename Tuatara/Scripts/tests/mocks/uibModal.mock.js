(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));
    (function () { let mocks = Tuatara.mocks || (Tuatara.mocks = {}) })();

    Tuatara.mocks.uibModal = function (jasmine, $injector) {
        let _this = this;
        let $q = $injector.get("$q");
        let $rootScope = $injector.get("$rootScope");
        let uibModalResult = $q.defer();

        // 
        // definition of mock
        //
        this.open = jasmine.createSpy('uibmodal_open').and.returnValue({ result: uibModalResult.promise });

        //
        // helper funcs
        this.__resolve = function (result) {
            uibModalResult.resolve({ status: { $value: { data: result } } });            
            $rootScope.$apply(); // make angular resolve the promise
        }

        this.__reject = function () {
            uibModalResult.reject();            
            $rootScope.$apply(); // make angular resolve the promise
        }

        this.__getOpenArgs = function () {
            return _this.open.calls.mostRecent().args;
        }
    }
})();
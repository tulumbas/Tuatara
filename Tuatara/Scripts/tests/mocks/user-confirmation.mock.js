
(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));
    (function () { let mocks = Tuatara.mocks || (Tuatara.mocks = {}) })();
    

    Tuatara.mocks.userConfirmation = function (initialAnswer) {
        let _that = this;
        this.answer = !!initialAnswer;        
        this.service = function (action) {
            if (_that.answer) {
                action();
            }
        }
    }
})();
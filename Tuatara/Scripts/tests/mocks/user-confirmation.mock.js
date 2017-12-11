
(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));
    (function () { let mocks = Tuatara.mocks || (Tuatara.mocks = {}) })(); 

    Tuatara.mocks.userConfirmation = function (initialAnswer) {
        let _that = this;
        this.answer = !!initialAnswer;
        this.question = 'Are you sure?';
        this.service = function (action, question) {
            if (question) {
                _that.question = question;
            }

            if (_that.answer) {
                action();
            }
        }

        this.__getQuestion = function() {
            return _that.question;
        }
    }
})();
﻿/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />
var RegisterBookViewModel = (function () {
    function RegisterBookViewModel(bookHub) {
        var _this = this;
        this.bookHub = bookHub;
        this.title = ko.observable('');
        this.errorMessages = ko.observableArray([]);

        bookHub.on('bookError', function (errorMessages) {
            _this.errorMessages(errorMessages);
        });

        bookHub.on('bookRegistered', function () {
            _this.title('');
            _this.errorMessages([]);
        });
    }
    RegisterBookViewModel.prototype.registerBook = function () {
        this.bookHub.invoke('registerBook', this.title());
    };
    return RegisterBookViewModel;
})();
//# sourceMappingURL=RegisterBookViewModel.js.map

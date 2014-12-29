/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />

class RegisterBookViewModel {
    bookHub : any;
    title: KnockoutObservable<string>;

    constructor(bookHub: any) {
        this.bookHub = bookHub;
        this.title = ko.observable('');
    }

    registerBook() {
        this.bookHub.server.registerBook(this.title());

        this.title('');
    }
}
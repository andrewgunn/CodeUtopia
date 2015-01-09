/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />
class RegisterBookViewModel {
    bookHub: any;
    hasErrors: KnockoutComputed<boolean>;
    title: KnockoutObservable<string>;
    errorMessages: KnockoutObservableArray<string>;

    constructor(bookHub: any) {
        this.bookHub = bookHub;
        this.title = ko.observable('');
        this.errorMessages = ko.observableArray([]);

        bookHub.on('bookError', (errorMessages: string[]) => {
            this.errorMessages(errorMessages);
        });

        bookHub.on('bookRegistered', () => {
            this.title('');
            this.errorMessages([]);
        });
    }

    registerBook() {
        this.bookHub.invoke('registerBook', this.title());
    }
}
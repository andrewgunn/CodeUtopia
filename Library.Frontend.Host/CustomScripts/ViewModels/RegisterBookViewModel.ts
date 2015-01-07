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

        this.hasErrors = ko.computed({
            owner: this,
            read: () => {
                return this.errorMessages().length > 0;
            }
        });

        bookHub.client.validationFailed = (errorMessages: string[]) => {
            this.errorMessages(errorMessages);
        };
    }

    registerBook() {
        this.bookHub.server.registerBook(this.title());
    }
}
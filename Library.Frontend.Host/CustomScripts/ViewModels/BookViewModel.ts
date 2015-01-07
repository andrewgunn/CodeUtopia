/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />
class BookViewModel {
    bookHub: any;
    bookId: KnockoutObservable<string>;
    canBorrow: KnockoutComputed<boolean>;
    canReturn: KnockoutComputed<boolean>;
    _isBorrowed: KnockoutObservable<boolean>;
    title: KnockoutObservable<string>;

    constructor(bookHub: any, bookId: string, title: string, isBorrowed: boolean) {
        this.bookHub = bookHub;
        this.bookId = ko.observable(bookId);
        this.title = ko.observable(title);
        this._isBorrowed = ko.observable(isBorrowed);

        this.canBorrow = ko.computed({
            owner: this,
            read: () => {
                return !this._isBorrowed();
            }
        });

        this.canReturn = ko.computed({
            owner: this,
            read: () => {
                return this._isBorrowed();
            }
        });

    }

    borrowBook() {
        return this.bookHub.invoke('borrowBook', this.bookId());
    }

    returnBook() {
        this.bookHub.invoke('returnBook', this.bookId());
    }
}
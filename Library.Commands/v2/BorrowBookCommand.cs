using System;

namespace Library.Commands.v2
{
    public class BorrowBookCommand
    {
        public Guid BookId { get; set; }

        public DateTime ReturnBy { get; set; }
    }
}
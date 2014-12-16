using System;

namespace Library.Commands
{
    public class BorrowBookCommand
    {
        public Guid BookId { get; set; }

        public DateTime BorrowedAt { get; set; }
    }
}
using System;

namespace Library.Commands
{
    public class ReturnBookCommand
    {
        public Guid BookId { get; set; }

        public DateTime ReturnedAt { get; set; }
    }
}
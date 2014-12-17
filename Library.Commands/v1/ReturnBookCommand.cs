using System;

namespace Library.Commands.v1
{
    public class ReturnBookCommand
    {
        public Guid BookId { get; set; }

        public DateTime ReturnedAt { get; set; }
    }
}
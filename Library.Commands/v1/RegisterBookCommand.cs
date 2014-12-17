using System;

namespace Library.Commands.v1
{
    public class RegisterBookCommand
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }
    }
}
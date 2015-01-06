using CodeUtopia.Validators;

namespace Library.Validators.Book
{
    public class TitleTooLong : IValidationError
    {
        public TitleTooLong(string title)
        {
            _title = title;
        }

        public string Message
        {
            get
            {
                return "Title is too long.";
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly string _title;
    }
}
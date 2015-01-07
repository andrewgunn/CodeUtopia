using System.Collections.Generic;
using System.Linq;
using Library.Commands.v1.Replies;

namespace Library.Frontend.Host.ReplyHandlers
{
    public class BookValidationErrorCodeResourceMapper
    {
        public BookValidationErrorCodeResourceMapper()
        {
            _dictionary = new Dictionary<BookValidationErrorCodes, string>
                          {
                              {
                                  BookValidationErrorCodes.TitleIsNull, Resources.TitleIsNull
                              },
                              {
                                  BookValidationErrorCodes.TitleIsTooShort, Resources.TitleIsTooShort
                              },
                              {
                                  BookValidationErrorCodes.TitleIsTooLong, Resources.TitleIsTooLong
                              },
                              {
                                  BookValidationErrorCodes.TitleContainsInvalidCharacters, Resources.TitleContainsInvalidCharacters
                              }
                          };
        }

        public IReadOnlyCollection<string> Map(BookValidationErrorCodes errorCodes)
        {
            return _dictionary.Where(x => errorCodes.HasFlag(x.Key))
                              .Select(x => x.Value)
                              .ToList();
        }

        private readonly Dictionary<BookValidationErrorCodes, string> _dictionary;
    }
}
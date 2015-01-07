using System.Collections.Generic;
using System.Linq;
using Library.Commands.v1.Replies;

namespace Library.Frontend.Host.ReplyHandlers
{
    public class BookErrorCodeResourceMapper
    {
        public BookErrorCodeResourceMapper()
        {
            _mappings = new Dictionary<BookErrorCodes, string>
                          {
                              {
                                  BookErrorCodes.TitleIsNull, Resources.TitleIsNull
                              },
                              {
                                  BookErrorCodes.TitleIsTooShort, Resources.TitleIsTooShort
                              },
                              {
                                  BookErrorCodes.TitleIsTooLong, Resources.TitleIsTooLong
                              },
                              {
                                  BookErrorCodes.TitleContainsInvalidCharacters, Resources.TitleContainsInvalidCharacters
                              },
                              {
                                  BookErrorCodes.TitleHasWhiteSpaceAtTheBeginning, Resources.TitleHasWhiteSpaceAtTheBeginning
                              },
                              {
                                  BookErrorCodes.TitleHasWhiteSpaceAtTheEnd, Resources.TitleHasWhiteSpaceAtTheEnd
                              }
                          };
        }

        public IReadOnlyCollection<string> Map(BookErrorCodes errorCodes)
        {
            return _mappings.Where(x => errorCodes.HasFlag(x.Key))
                              .Select(x => x.Value)
                              .ToList();
        }

        private readonly Dictionary<BookErrorCodes, string> _mappings;
    }
}
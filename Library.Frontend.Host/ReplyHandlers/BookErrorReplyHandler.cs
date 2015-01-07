using Library.Commands.v1.Replies;
using Library.Frontend.Host.Hubs;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.ReplyHandlers
{
    public class BookErrorReplyHandler : IHandleMessages<BookErrorReply>
    {
        public void Handle(BookErrorReply bookValidationFailedReply)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<BookHub>();
            context.Clients.All.ValidationFailed(
                                                 new BookErrorCodeResourceMapper().Map(
                                                                                                 bookValidationFailedReply
                                                                                                     .ErrorCodes));
        }
    }
}
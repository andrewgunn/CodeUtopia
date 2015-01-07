using Library.Commands.v1.Replies;
using Library.Frontend.Host.Hubs;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.ReplyHandlers
{
    public class ValidationFailedReplyHandler : IHandleMessages<BookValidationFailedReply>
    {
        public void Handle(BookValidationFailedReply bookValidationFailedReply)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<BookHub>();
            context.Clients.All.ValidationFailed(
                                                 new BookValidationErrorCodeResourceMapper().Map(
                                                                                                 bookValidationFailedReply
                                                                                                     .ErrorCodes));
        }
    }
}
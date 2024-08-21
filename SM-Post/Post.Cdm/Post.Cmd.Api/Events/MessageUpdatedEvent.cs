using CQRS.Core.Events;

namespace Post.Cmd.Api.Events
{
    public class MessageUpdatedEvent : BaseEvent
    {
        protected MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent))
        {
        }
        public string Message { get; set; }
    }
}

using CQRS.Core.Events;

namespace Post.Cmd.Api.Events
{
    public class PostCreatedEvent : BaseEvent
    {
        protected PostCreatedEvent() : base(nameof(PostCreatedEvent))
        {
        }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime DatePosted { get; set; }
    }
}

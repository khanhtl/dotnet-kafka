using CQRS.Core.Events;

namespace Post.Cmd.Api.Events
{
    public class PostRemovedEvent : BaseEvent
    {
        public PostRemovedEvent() : base(nameof(CommentRemovedEvent))
        {
            
        }
    }
}

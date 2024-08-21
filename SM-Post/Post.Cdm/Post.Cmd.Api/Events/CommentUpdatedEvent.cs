using CQRS.Core.Events;

namespace Post.Cmd.Api.Events
{
    public class CommentUpdatedEvent : BaseEvent
    {
        public CommentUpdatedEvent() : base(nameof(CommentUpdatedEvent))
        {
            
        }
        public Guid CommentID { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public DateTime EditDate { get; set; }
    }
}

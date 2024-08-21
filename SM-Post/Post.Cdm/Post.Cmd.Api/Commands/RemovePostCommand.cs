using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public class RemovePostCommand : BaseCommand
    {
        public string UserName { get; set; }
    }
}

namespace CQRS.Core.Consumers;

public interface IEventConsumer
{
    void Comsume(string topic);
}

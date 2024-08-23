namespace CQRS.Core.Exceptions
{
    public class AggreateNotFoundException : Exception
    {
        public AggreateNotFoundException(string message) : base(message)
        {
            
        }
    }
}

namespace BTG_core.Models
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message) : base(message)
        {
        }
    }
}

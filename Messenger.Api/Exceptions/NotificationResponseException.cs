namespace Messenger.WebApi.Exceptions
{
    internal class NotificationResponseException(string message) : ApplicationException(message)
    {
        internal static void EmptyMessage()
        {
            throw new NotificationResponseException("The message is required");
        }
    }
}

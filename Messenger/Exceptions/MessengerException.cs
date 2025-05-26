namespace Messenger.Exceptions
{
    public class MessengerException(string message) : ApplicationException(message)
    {
        public static void NotificationMessageIsEmpty()
        {
            throw new MessengerException("The message added is empty.");
        }
    }
}

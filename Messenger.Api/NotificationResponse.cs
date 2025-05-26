using Messenger.WebApi.Exceptions;

namespace Messenger.WebApi
{
    /// <summary>
    /// Response model to notification.
    /// </summary>
    public class NotificationResponse
    {
        /// <summary>
        /// Message founded in the notifier.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// NotificationResponse constructor
        /// </summary>
        /// <param name="message">Message</param>
        public NotificationResponse(string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                NotificationResponseException.EmptyMessage();

            Message = message;
        }
    }
}

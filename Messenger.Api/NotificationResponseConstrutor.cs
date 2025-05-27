using Messenger.WebApi;

namespace Messenger.WebApi
{
    internal class NotificationResponseConstrutor
    {
        private string _message = "";

        internal NotificationResponseConstrutor WithMessage(string message)
        {
            _message = message;

            return this;
        }

        internal NotificationResponse Build()
        {
            var notificationResponse = new NotificationResponse(_message);

            return notificationResponse;
        }
    }
}

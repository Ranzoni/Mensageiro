using Messenger.Exceptions;

namespace Messenger
{
    public class Notifier : INotifier
    {
        const string STANDARD_MSG = "STANDARD";
        const string NOT_FOUND_MSG = "NOT_FOUND";
        const string UNAUTHORIZED_MSG = "UNAUTHORIZED";

        private readonly List<(string messageType, string message)> _messages = [];

        private void AddMessageToList(string message, string key = STANDARD_MSG)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                key = STANDARD_MSG;

            _messages.Add((key, message));
        }

        /// <summary>
        /// Add a string type message to the notifier.
        /// </summary>
        /// <param name="message">Message to add.</param>
        public void AddMessage(string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                return;

            var stringMessage = message;
            AddMessageToList(stringMessage);
        }

        /// <summary>
        /// Add a enum type message to the notifier.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="message">Enum message to add.</param>
        public void AddMessage<T>(T message) where T : struct, IConvertible
        {
            var stringMessage = message.Description();
            if (string.IsNullOrEmpty(stringMessage) || string.IsNullOrWhiteSpace(stringMessage))
            {
                MessengerException.NotificationMessageIsEmpty();
                return;
            }

            AddMessageToList(stringMessage);
        }

        /// <summary>
        /// Add a not found string type message to the notifier.
        /// It has the same effect as AddMessage, but an identification is added for the "not found" message.
        /// </summary>
        /// <param name="message">Message to add.</param>
        public void AddNotFoundMessage(string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                return;

            AddMessageToList(
                message: message,
                key: NOT_FOUND_MSG);
        }

        /// <summary>
        /// Add a not found enum type message to the notifier.
        /// It has the same effect as AddMessage, but an identification is added for the "not found" message.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="message">Message to add.</param>
        public void AddNotFoundMessage<T>(T message) where T : struct, IConvertible
        {
            var stringMessage = message.Description();
            if (string.IsNullOrEmpty(stringMessage) || string.IsNullOrWhiteSpace(stringMessage))
            {
                MessengerException.NotificationMessageIsEmpty();
                return;
            }

            AddMessageToList(
                message: stringMessage,
                key: NOT_FOUND_MSG);
        }

        /// <summary>
        /// Returns true if there is any message in the notifier, otherwise it returns false.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool AnyMessage()
        {
            return _messages.Count > 0;
        }

        /// <summary>
        /// Returns true if there is any not found message in the notifier, otherwise it returns false.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool AnyNotFoundMessage()
        {
            return _messages.Any(m => m.messageType.Equals(NOT_FOUND_MSG));
        }

        /// <summary>
        /// Return all messages from the notifier.
        /// </summary>
        /// <returns>A list of strings.</returns>
        public IEnumerable<string> Messages()
        {
            return _messages.Select(m => m.message);
        }

        /// <summary>
        /// Return all not found messages from the notifier.
        /// </summary>
        /// <returns>A list of strings.</returns>
        public IEnumerable<string> NotFoundMessages()
        {
            return _messages
                .Where(m => m.messageType.Equals(NOT_FOUND_MSG))
                .Select(m => m.message);
        }

        /// <summary>
        /// Add a unauthorized string message to the notifier.
        /// It has the same effect as AddMessage, but an identification is added for the "unauthorized" message.
        /// </summary>
        /// <param name="message">Message to add.</param>
        public void AddUnauthorizedMessage(string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                return;

            AddMessageToList(
                message: message,
                key: UNAUTHORIZED_MSG);
        }

        /// <summary>
        /// Add a unauthorized enum type message to the notifier.
        /// It has the same effect as AddMessage, but an identification is added for the "unauthorized" message.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="message">Message to add.</param>
        public void AddUnauthorizedMessage<T>(T message) where T : struct, IConvertible
        {
            var stringMessage = message.Description();
            if (string.IsNullOrEmpty(stringMessage) || string.IsNullOrWhiteSpace(stringMessage))
            {
                MessengerException.NotificationMessageIsEmpty();
                return;
            }

            AddMessageToList(
                message: stringMessage,
                key: UNAUTHORIZED_MSG);
        }

        /// <summary>
        /// Returns true if there is any unauthorized message in the notifier, otherwise it returns false.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool AnyUnauthorizedMessage()
        {
            return _messages.Any(m => m.messageType.Equals(UNAUTHORIZED_MSG));
        }

        /// <summary>
        /// Return all uauthorized type messages from the notifier.
        /// </summary>
        /// <returns>A list of strings</returns>
        public IEnumerable<string> UnauthorizedMessages()
        {
            return _messages
                .Where(m => m.messageType.Equals(UNAUTHORIZED_MSG))
                .Select(m => m.message);
        }
    }
}

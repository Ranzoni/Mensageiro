namespace Messenger
{
    public interface INotifier
    {
        /// <summary>
        /// Add a string type message to the notifier.
        /// </summary>
        /// <param name="message">Message to add.</param>
        void AddMessage(string message);

        /// <summary>
        /// Add a enum type message to the notifier.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="message">Enum message to add.</param>
        void AddMessage<T>(T message) where T : struct, IConvertible;

        /// <summary>
        /// Add a not found string type message to the notifier.
        /// It has the same effect as AddMessage, but an identification is added for the "not found" message.
        /// </summary>
        /// <param name="message">Message to add.</param>
        void AddNotFoundMessage(string message);

        /// <summary>
        /// Add a not found enum type message to the notifier.
        /// It has the same effect as AddMessage, but an identification is added for the "not found" message.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="message">Message to add.</param>
        void AddNotFoundMessage<T>(T message) where T : struct, IConvertible;

        /// <summary>
        /// Add a unauthorized string message to the notifier.
        /// It has the same effect as AddMessage, but an identification is added for the "unauthorized" message.
        /// </summary>
        /// <param name="message">Message to add.</param>
        void AddUnauthorizedMessage(string message);

        /// <summary>
        /// Add a unauthorized enum type message to the notifier.
        /// It has the same effect as AddMessage, but an identification is added for the "unauthorized" message.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="message">Message to add.</param>
        void AddUnauthorizedMessage<T>(T message) where T : struct, IConvertible;

        /// <summary>
        /// Returns true if there is any message in the notifier, otherwise it returns false.
        /// </summary>
        /// <returns>True or false.</returns>
        bool AnyMessage();

        /// <summary>
        /// Returns true if there is any not found message in the notifier, otherwise it returns false.
        /// </summary>
        /// <returns>True or false.</returns>
        bool AnyNotFoundMessage();

        /// <summary>
        /// Returns true if there is any unauthorized message in the notifier, otherwise it returns false.
        /// </summary>
        /// <returns>True or false.</returns>
        bool AnyUnauthorizedMessage();

        /// <summary>
        /// Return all messages from the notifier.
        /// </summary>
        /// <returns>A list of strings.</returns>
        IEnumerable<string> Messages();

        /// <summary>
        /// Return all not found messages from the notifier.
        /// </summary>
        /// <returns>A list of strings.</returns>
        IEnumerable<string> NotFoundMessages();

        /// <summary>
        /// Return all uauthorized type messages from the notifier.
        /// </summary>
        /// <returns>A list of strings</returns>
        IEnumerable<string> UnauthorizedMessages();
    }
}

using Messenger.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.WebApi
{
    public abstract class BaseMessengerController(INotifier _notifier, int? _statusCodeNotifier = null) : ControllerBase
    {
        /// <summary>
        /// Returns the 'response' if there is any message in the notifier.
        /// Otherwise returns a status 201 (Created).
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="response">Response object.</param>
        /// <returns>ActionResult</returns>
        protected ActionResult<T?> SuccessfullyCreated<T>(T? response)
        {
            var validationActionResultMessage = CheckMessages();
            if (validationActionResultMessage is not null)
                return validationActionResultMessage;

            if (response is null)
                return StatusCode(201);
            else
                return StatusCode(201, response);
        }

        /// <summary>
        /// Returns the 'response' if there is any message in the notifier.
        /// Otherwise returns a status 200 (Ok).
        /// </summary>
        /// <param name="response">Response object.</param>
        /// <returns>ActionResult</returns>
        protected ActionResult Success(object? response)
        {
            var validationActionResultMessage = CheckMessages();
            if (validationActionResultMessage is not null)
                return validationActionResultMessage;

            return Ok(response);
        }

        /// <summary>
        /// Returns the 'response' if there is any message in the notifier.
        /// Otherwise returns a predefined that was created by the 'statusCode' parameter.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <param name="retorno">Response object.</param>
        /// <returns>ActionResult</returns>
        protected ActionResult GetStatus(int statusCode, object? retorno)
        {
            var actionResultMensagemValidacao = CheckMessages();
            if (actionResultMensagemValidacao is not null)
                return actionResultMensagemValidacao;

            if (retorno is null)
                return StatusCode(statusCode);
            else
                return StatusCode(statusCode, retorno);
        }

        private ActionResult? CheckMessages()
        {
            if (_notifier.AnyUnauthorizedMessage())
            {
                var messagesList = ReturnNotifierResponse(_notifier.UnauthorizedMessages());
                return StatusCode(401, messagesList);
            }

            if (_notifier.AnyNotFoundMessage())
            {
                var messagesList = ReturnNotifierResponse(_notifier.NotFoundMessages());
                return NotFound(messagesList);
            }

            if (_notifier.AnyMessage())
            {
                if (_statusCodeNotifier is not null)
                {
                    var messagesList = ReturnNotifierResponse(_notifier.Messages());
                    return StatusCode(_statusCodeNotifier ?? 0, messagesList);
                }
                else
                {
                    var messagesList = ReturnNotifierResponse(_notifier.Messages());
                    return UnprocessableEntity(messagesList);
                }
            }

            return null;
        }

        private static List<NotificationResponse> ReturnNotifierResponse(IEnumerable<string> messages)
        {
            var notifications = new List<NotificationResponse>();

            var constructor = new NotificationResponseConstrutor();

            foreach (var msg in messages)
            {
                var notification = constructor
                    .WithMessage(msg)
                    .Build();

                notifications.Add(notification);
            }

            return notifications;
        }

        /// <summary>
        /// Geth the controller notifier.
        /// </summary>
        /// <returns>INotifier</returns>
        protected INotifier Notifier()
        {
            return _notifier;
        }
    }
}

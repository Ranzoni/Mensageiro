using Bogus;
using Messenger.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Messenger.Test.Messenger.WebApi
{
    public class BaseMessengerControllerTest
    {
        private readonly Faker _faker = new();

        [Fact]
        internal void ShouldReturnSuccessfullyCreated()
        {
            var entity = new
            {
                Name = "John"
            };
            var (controller, notifier) = CreateTestController();

            var response = controller.CreateEntity(entity);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal(entity, result.Value);
            Assert.Equal(201, result.StatusCode);
            Assert.Empty(notifier.Messages());
        }

        [Fact]
        internal void ShouldNotReturnSuccessfullyCreated()
        {
            var entity = new
            {
                Name = "John"
            };
            var (controller, notifier) = CreateTestController();
            var message = _faker.Lorem.Sentences();
            notifier.AddMessage(message);

            var response = controller.CreateEntity(entity);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<UnprocessableEntityObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<object>;
            var notificationObj = notifications?.FirstOrDefault();
            Assert.Equal(message, notificationObj?.GetType()?.GetProperty("Message", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(notificationObj));
            Assert.Equal(422, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnUnauthorizedForSuccessfullyCreated()
        {
            var entity = new
            {
                Name = "John"
            };
            var (controller, notifier) = CreateTestController();
            var message = "Unauthorized";
            notifier.AddUnauthorizedMessage(message);

            var response = controller.CreateEntity(entity);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<ObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(401, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnNootFoundForSuccessfullyCreated()
        {
            var entity = new
            {
                Name = "John"
            };
            var (controller, notifier) = CreateTestController();
            var message = "Not found";
            notifier.AddNotFoundMessage(message);

            var response = controller.CreateEntity(entity);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<NotFoundObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(404, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnCustomStatusForSuccessfullyCreated()
        {
            var entity = new
            {
                Name = "John"
            };
            var statusCode = 400;
            var (controller, notifier) = CreateTestController(statusCode);
            var message = _faker.Lorem.Sentences();
            notifier.AddMessage(message);

            var response = controller.CreateEntity(entity);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<ObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(statusCode, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var (controller, notifier) = CreateTestController();

            var response = controller.GetEntity(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<OkObjectResult>(response.Result);
            var entity = result.Value;
            var idRetornado = entity?.GetType().GetProperty("Id")?.GetValue(entity);
            Assert.Equal(id, idRetornado);
            Assert.Equal(200, result.StatusCode);
            Assert.Empty(notifier.Messages());
        }

        [Fact]
        internal void ShouldNotReturnOk()
        {
            var id = Guid.NewGuid();
            var (controller, notifier) = CreateTestController();
            var message = _faker.Lorem.Sentences();
            notifier.AddMessage(message);

            var response = controller.GetEntity(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<UnprocessableEntityObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(422, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnUnauthorizedForOk()
        {
            var id = Guid.NewGuid();
            var (controller, notifier) = CreateTestController();
            var message = "Unauthorized";
            notifier.AddUnauthorizedMessage(message);

            var response = controller.GetEntity(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<ObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(401, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnNotFoundForOk()
        {
            var id = Guid.NewGuid();
            var (controller, notifier) = CreateTestController();
            var message = "Not found";
            notifier.AddNotFoundMessage(message);

            var response = controller.GetEntity(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<NotFoundObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(404, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnCustomStatusForOk()
        {
            var id = Guid.NewGuid();
            var statusCode = 400;
            var (controller, notifier) = CreateTestController(statusCode);
            var message = _faker.Lorem.Sentences();
            notifier.AddMessage(message);

            var response = controller.GetEntity(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<ObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(statusCode, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnSpecificStatus()
        {
            var id = Guid.NewGuid();
            var (controller, notifier) = CreateTestController();

            var response = controller.GetEntityWithSpecificStatus(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<ObjectResult>(response.Result);
            var entity = result.Value;
            var idRetornado = entity?.GetType().GetProperty("Id")?.GetValue(entity);
            Assert.Equal(id, idRetornado);
            Assert.Equal(202, result.StatusCode);
            Assert.Empty(notifier.Messages());
        }

        [Fact]
        internal void ShouldNotReturnSpecificStatus()
        {
            var id = Guid.NewGuid();
            var (controller, notifier) = CreateTestController();
            var message = _faker.Lorem.Sentences();
            notifier.AddMessage(message);

            var response = controller.GetEntityWithSpecificStatus(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<UnprocessableEntityObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(422, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnUnauthorizedForSpecificStatus()
        {
            var id = Guid.NewGuid();
            var (controller, notifier) = CreateTestController();
            var message = _faker.Lorem.Sentences();
            notifier.AddUnauthorizedMessage(message);

            var response = controller.GetEntityWithSpecificStatus(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<ObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(401, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnNotFoundForSpecificStatus()
        {
            var id = Guid.NewGuid();
            var (controller, notifier) = CreateTestController();
            var message = _faker.Lorem.Sentences();
            notifier.AddNotFoundMessage(message);

            var response = controller.GetEntityWithSpecificStatus(id);

            Assert.NotNull(response.Result);
            var result = Assert.IsType<NotFoundObjectResult>(response.Result);
            var notifications = result.Value as IEnumerable<NotificationResponse>;
            Assert.Equal(message, notifications?.FirstOrDefault()?.Message);
            Assert.Equal(404, result.StatusCode);
            Assert.NotEmpty(notifier.Messages());
        }

        [Fact]
        internal void ShouldReturnNotifier()
        {
            var msg = _faker.Lorem.Sentences();
            var (controller, notifier) = CreateTestController();

            var result = controller.GetNotifier();
            result.AddMessage(msg);

            Assert.NotNull(result);
            Assert.Equal(notifier, result);
            Assert.True(notifier.AnyMessage());
            Assert.Equal(msg, notifier.Messages().First());
        }

        private static (ControllerTeste controller, Notifier notifier) CreateTestController(int? messengerStatusCode = null)
        {
            var notifier = new Notifier();
            var controller = new ControllerTeste(notifier, messengerStatusCode);

            return (controller, notifier);
        }
    }

    internal class ControllerTeste(INotifier notifier, int? statusCodeMessenger = null) : BaseMessengerController(notifier, statusCodeMessenger)
    {
        internal ActionResult<object?> CreateEntity(object entity)
        {
            return SuccessfullyCreated(entity);
        }

        internal ActionResult<object?> GetEntity(Guid id)
        {
            var entity = new
            {
                Id = id,
                Name = "John"
            };

            return Success(entity);
        }

        internal ActionResult<object?> GetEntityWithSpecificStatus(Guid id)
        {
            var entity = new
            {
                Id = id,
                Name = "John"
            };

            return GetStatus(202, entity);
        }

        internal INotifier GetNotifier()
        {
            return Notifier();
        }
    }
}

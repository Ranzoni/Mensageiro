using Bogus;
using Messenger.Exceptions;
using System.ComponentModel;

namespace Messenger.Test.Messenger
{
    public class NotifierTest
    {
        private readonly Faker _faker = new();

        [Fact]
        internal void ShouldAddAStringMessage()
        {
            var msg = _faker.Lorem.Sentence();
            var notifier = new Notifier();

            notifier.AddMessage(msg);

            Assert.True(notifier.AnyMessage());
            var messageCreated = notifier.Messages().First();
            Assert.Equal(msg, messageCreated);
        }

        [Fact]
        internal void ShouldAddManyStringMessages()
        {
            var notifier = new Notifier();
            var messagesCountByTest = 2;

            for (var i = 1; i <= messagesCountByTest; i++)
            {
                var msg = _faker.Lorem.Sentence();
                notifier.AddMessage(msg);

                Assert.Contains(msg, notifier.Messages());
            }

            Assert.Equal(messagesCountByTest, notifier.Messages().Count());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void ShouldNotAddEmptyStringMessage(string invalidMessage)
        {
            var notifier = new Notifier();

            notifier.AddMessage(invalidMessage);

            Assert.False(notifier.AnyMessage());
            Assert.False(notifier.Messages().Any());
        }

        [Fact]
        internal void ShouldAddEnumMessage()
        {
            var notifier = new Notifier();

            notifier.AddMessage(ETestMessage.TestMessage);

            Assert.True(notifier.AnyMessage());
            var messageCreated = notifier.Messages().First();
            var stringEnumMsg = Description(ETestMessage.TestMessage);
            Assert.Equal(stringEnumMsg, messageCreated);
        }

        [Theory]
        [InlineData(ETestMessage.ParameterlessDescriptionTestMessage)]
        [InlineData(ETestMessage.EmptyDescriptionTestMessage)]
        [InlineData(ETestMessage.WhitespacesDescriptionTestMessage)]
        internal void ShouldThrowsExceptionToAddEmptyEnumMessage(ETestMessage invalidMessage)
        {
            var notifier = new Notifier();

            var exception = Assert.Throws<MessengerException>(() => notifier.AddMessage(invalidMessage));

            Assert.IsType<MessengerException>(exception);
        }

        [Fact]
        internal void ShouldAddNotFoundStringMessage()
        {
            var msg = _faker.Lorem.Sentence();
            var notifier = new Notifier();

            notifier.AddNotFoundMessage(msg);

            Assert.True(notifier.AnyNotFoundMessage());
            var messageCreated = notifier.NotFoundMessages().First();
            Assert.Equal(msg, messageCreated);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void ShouldNotAddEmptyStringNotFoundMessage(string invalidMessage)
        {
            var notifier = new Notifier();

            notifier.AddNotFoundMessage(invalidMessage);

            Assert.False(notifier.AnyNotFoundMessage());
            Assert.False(notifier.AnyMessage());
            Assert.False(notifier.NotFoundMessages().Any());
            Assert.False(notifier.Messages().Any());
        }

        [Fact]
        internal void ShouldAddNotFoundEnumMessage()
        {
            var notifier = new Notifier();

            notifier.AddNotFoundMessage(ETestMessage.TestMessage);

            Assert.True(notifier.AnyNotFoundMessage());
            var messageCreated = notifier.NotFoundMessages().First();
            var stringEnumMsg = Description(ETestMessage.TestMessage);
            Assert.Equal(stringEnumMsg, messageCreated);
        }

        [Theory]
        [InlineData(ETestMessage.ParameterlessDescriptionTestMessage)]
        [InlineData(ETestMessage.EmptyDescriptionTestMessage)]
        [InlineData(ETestMessage.WhitespacesDescriptionTestMessage)]
        internal void ShouldThrowsExceptionToAddEmptyNotFoundEnumMessage(ETestMessage invalidMessage)
        {
            var notifier = new Notifier();

            var exception = Assert.Throws<MessengerException>(() => notifier.AddNotFoundMessage(invalidMessage));

            Assert.IsType<MessengerException>(exception);
        }

        [Fact]
        internal void ShouldAddUnauthorizedStringMessage()
        {
            var msg = _faker.Lorem.Sentence();
            var notifier = new Notifier();

            notifier.AddUnauthorizedMessage(msg);

            Assert.True(notifier.AnyUnauthorizedMessage());
            var messageCreated = notifier.UnauthorizedMessages().First();
            Assert.Equal(msg, messageCreated);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void ShouldNotAddEmptyUnauthorizedStringMessage(string invalidMessage)
        {
            var notifier = new Notifier();

            notifier.AddUnauthorizedMessage(invalidMessage);

            Assert.False(notifier.AnyUnauthorizedMessage());
            Assert.False(notifier.AnyMessage());
            Assert.False(notifier.UnauthorizedMessages().Any());
            Assert.False(notifier.Messages().Any());
        }

        [Fact]
        internal void ShouldAddUnauthorizedEnumMessage()
        {
            var notifier = new Notifier();

            notifier.AddUnauthorizedMessage(ETestMessage.TestMessage);

            Assert.True(notifier.AnyUnauthorizedMessage());
            var messageCreated = notifier.UnauthorizedMessages().First();
            var stringEnumMsg = Description(ETestMessage.TestMessage);
            Assert.Equal(stringEnumMsg, messageCreated);
        }

        [Theory]
        [InlineData(ETestMessage.ParameterlessDescriptionTestMessage)]
        [InlineData(ETestMessage.EmptyDescriptionTestMessage)]
        [InlineData(ETestMessage.WhitespacesDescriptionTestMessage)]
        internal void ShouldThrowsExceptionToAddEmptyUnauthorizedEnumMessage(ETestMessage invalidMessage)
        {
            var notifier = new Notifier();

            var exception = Assert.Throws<MessengerException>(() => notifier.AddUnauthorizedMessage(invalidMessage));

            Assert.IsType<MessengerException>(exception);
        }

        private static string? Description<T>(T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = value.ToString();
            var fieldInfo = value.GetType().GetField(value.ToString() ?? "");

            if (fieldInfo != null)
            {
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes != null && attributes.Length > 0)
                    description = ((DescriptionAttribute)attributes[0]).Description;
            }

            return description;
        }
    }
}

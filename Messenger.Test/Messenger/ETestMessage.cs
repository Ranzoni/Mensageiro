using System.ComponentModel;

namespace Messenger.Test.Messenger
{
    internal enum ETestMessage
    {
        [Description("This is a test message.")]
        TestMessage,
        [Description()]
        ParameterlessDescriptionTestMessage,
        [Description("")]
        EmptyDescriptionTestMessage,
        [Description("   ")]
        WhitespacesDescriptionTestMessage,
    }
}

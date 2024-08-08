using Bogus;
using System.ComponentModel;

namespace Mensageiro.Teste
{
    public class NotificadorTeste
    {
        private readonly Faker _faker = new();

        [Fact]
        internal void DeveAdicionarMensagemEmString()
        {
            var msg = _faker.Lorem.Sentence();
            var notificador = new Notificador();

            notificador.AddMensagem(msg);

            Assert.True(notificador.ExisteMensagem());
            var mensagemCriada = notificador.Mensagens().First();
            Assert.Equal(msg, mensagemCriada);
        }

        [Fact]
        internal void DeveAdicionarMensagemEmEnum()
        {
            var notificador = new Notificador();
            notificador.AddMensagem(EMensagemTeste.MensagemTeste);

            Assert.True(notificador.ExisteMensagem());
            var mensagemCriada = notificador.Mensagens().First();
            var enumMsgEmString = Descricao(EMensagemTeste.MensagemTeste);
            Assert.Equal(enumMsgEmString, mensagemCriada);
        }

        private static string? Descricao<T>(T value) where T : struct, IConvertible
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
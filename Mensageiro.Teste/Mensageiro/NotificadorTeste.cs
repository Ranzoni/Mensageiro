using Bogus;
using Mensageiro.Excecoes;
using System.ComponentModel;

namespace Mensageiro.Teste.Mensageiro
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void NaoDeveAdicionarMensagemEmStringVazia(string msgInvalida)
        {
            var notificador = new Notificador();

            notificador.AddMensagem(msgInvalida);

            Assert.False(notificador.ExisteMensagem());
            Assert.False(notificador.Mensagens().Any());
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

        [Theory]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoSemParametro)]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoVazia)]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoEspacoEmBranco)]
        internal void DeveRetornarExcecaoAoAdicionarMensagemEmEnumVazia(EMensagemTeste msgInvalida)
        {
            var notificador = new Notificador();

            var excecao = Assert.Throws<MensageiroExcecao>(() => notificador.AddMensagem(msgInvalida));

            Assert.IsType<MensageiroExcecao>(excecao);
        }

        [Fact]
        internal void DeveAdicionarMensagemEmStringDeNaoEncontrado()
        {
            var msg = _faker.Lorem.Sentence();
            var notificador = new Notificador();

            notificador.AddMensagemNaoEncontrado(msg);

            Assert.True(notificador.ExisteMsgNaoEncontrado());
            var mensagemCriada = notificador.MensagensDeNaoEncontrado().First();
            Assert.Equal(msg, mensagemCriada);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void NaoDeveAdicionarMensagemEmStringVaziaDeNaoEncontrado(string msgInvalida)
        {
            var notificador = new Notificador();

            notificador.AddMensagemNaoEncontrado(msgInvalida);

            Assert.False(notificador.ExisteMsgNaoEncontrado());
            Assert.False(notificador.ExisteMensagem());
            Assert.False(notificador.MensagensDeNaoEncontrado().Any());
            Assert.False(notificador.Mensagens().Any());
        }

        [Fact]
        internal void DeveAdicionarMensagemEmEnumDeNaoEncontrado()
        {
            var notificador = new Notificador();

            notificador.AddMensagemNaoEncontrado(EMensagemTeste.MensagemTeste);

            Assert.True(notificador.ExisteMsgNaoEncontrado());
            var mensagemCriada = notificador.MensagensDeNaoEncontrado().First();
            var enumMsgEmString = Descricao(EMensagemTeste.MensagemTeste);
            Assert.Equal(enumMsgEmString, mensagemCriada);
        }

        [Theory]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoSemParametro)]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoVazia)]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoEspacoEmBranco)]
        internal void DeveRetornarExcecaoAoAdicionarMensagemEmEnumVaziaDeNaoEncontrado(EMensagemTeste msgInvalida)
        {
            var notificador = new Notificador();

            var excecao = Assert.Throws<MensageiroExcecao>(() => notificador.AddMensagemNaoEncontrado(msgInvalida));

            Assert.IsType<MensageiroExcecao>(excecao);
        }

        [Fact]
        internal void DeveAdicionarMensagemEmStringDeNaoAutorizado()
        {
            var msg = _faker.Lorem.Sentence();
            var notificador = new Notificador();

            notificador.AddMensagemNaoAutorizado(msg);

            Assert.True(notificador.ExisteMsgNaoAutorizado());
            var mensagemCriada = notificador.MensagensDeNaoAutorizado().First();
            Assert.Equal(msg, mensagemCriada);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void NaoDeveAdicionarMensagemEmStringVaziaDeNaoAutorizado(string msgInvalida)
        {
            var notificador = new Notificador();

            notificador.AddMensagemNaoAutorizado(msgInvalida);

            Assert.False(notificador.ExisteMsgNaoAutorizado());
            Assert.False(notificador.ExisteMensagem());
            Assert.False(notificador.MensagensDeNaoAutorizado().Any());
            Assert.False(notificador.Mensagens().Any());
        }

        [Fact]
        internal void DeveAdicionarMensagemEmEnumDeNaoAutorizado()
        {
            var notificador = new Notificador();

            notificador.AddMensagemNaoAutorizado(EMensagemTeste.MensagemTeste);

            Assert.True(notificador.ExisteMsgNaoAutorizado());
            var mensagemCriada = notificador.MensagensDeNaoAutorizado().First();
            var enumMsgEmString = Descricao(EMensagemTeste.MensagemTeste);
            Assert.Equal(enumMsgEmString, mensagemCriada);
        }

        [Theory]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoSemParametro)]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoVazia)]
        [InlineData(EMensagemTeste.MensagemTesteDescricaoEspacoEmBranco)]
        internal void DeveRetornarExcecaoAoAdicionarMensagemEmEnumVaziaDeNaoAutorizado(EMensagemTeste msgInvalida)
        {
            var notificador = new Notificador();

            var excecao = Assert.Throws<MensageiroExcecao>(() => notificador.AddMensagemNaoAutorizado(msgInvalida));

            Assert.IsType<MensageiroExcecao>(excecao);
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
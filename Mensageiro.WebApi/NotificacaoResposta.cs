using Mensageiro.WebApi.Excecoes;

namespace Mensageiro.WebApi
{
    internal class NotificacaoResposta
    {
        internal string Mensagem { get; private set; }

        internal NotificacaoResposta(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                NotificacaoRespostaException.MensagemNaoPreenchida();

            Mensagem = mensagem;
        }
    }
}

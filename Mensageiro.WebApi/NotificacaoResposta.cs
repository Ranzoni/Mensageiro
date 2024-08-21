using Mensageiro.WebApi.Excecoes;

namespace Mensageiro.WebApi
{
    public class NotificacaoResposta
    {
        public string Mensagem { get; private set; }

        public NotificacaoResposta(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                NotificacaoRespostaException.MensagemNaoPreenchida();

            Mensagem = mensagem;
        }
    }
}

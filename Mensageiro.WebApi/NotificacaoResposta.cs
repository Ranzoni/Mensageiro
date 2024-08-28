using Mensageiro.WebApi.Excecoes;

namespace Mensageiro.WebApi
{
    /// <summary>
    /// Modelo da resposta quando houver notificação
    /// </summary>
    public class NotificacaoResposta
    {
        /// <summary>
        /// Mensagem encontrada no notificador
        /// </summary>
        public string Mensagem { get; private set; }

        /// <summary>
        /// Construtor da NotificacaoResposta
        /// </summary>
        /// <param name="mensagem"></param>
        public NotificacaoResposta(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                NotificacaoRespostaException.MensagemNaoPreenchida();

            Mensagem = mensagem;
        }
    }
}

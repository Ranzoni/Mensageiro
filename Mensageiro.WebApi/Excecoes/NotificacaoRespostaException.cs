namespace Mensageiro.WebApi.Excecoes
{
    internal class NotificacaoRespostaException(string mensagem) : ApplicationException(mensagem)
    {
        internal static void MensagemNaoPreenchida()
        {
            throw new NotificacaoRespostaException("A mensagem é obrigatória");
        }
    }
}

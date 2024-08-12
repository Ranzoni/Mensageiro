namespace Mensageiro.Excecoes
{
    public class MensageiroExcecao(string mensagem) : ApplicationException(message: mensagem)
    {
        public static void MensagemNotificacaoVazia()
        {
            throw new MensageiroExcecao("A mensagem adicionada para notificação está vazia");
        }
    }
}

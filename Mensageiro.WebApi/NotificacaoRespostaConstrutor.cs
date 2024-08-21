namespace Mensageiro.WebApi
{
    internal class NotificacaoRespostaConstrutor
    {
        private string _mensagem = "";

        internal NotificacaoRespostaConstrutor ComMensagem(string mensagem)
        {
            _mensagem = mensagem;

            return this;
        }

        internal NotificacaoResposta Construir()
        {
            var notificacaoResposta = new NotificacaoResposta(_mensagem);

            return notificacaoResposta;
        }
    }
}

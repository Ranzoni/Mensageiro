namespace Mensageiro
{
    public abstract class NotificadorBase
    {
        const string MSG_PADRAO = "PADRAO";

        private readonly Dictionary<string, string> _mensagens = [];

        protected void AddMensagemLista(string mensagem, string chave = MSG_PADRAO)
        {
            if (string.IsNullOrEmpty(chave) || string.IsNullOrWhiteSpace(chave))
                chave = MSG_PADRAO;

            _mensagens.Add(chave, mensagem);
        }

        protected IDictionary<string, string> ListaMensagens()
        {
            return _mensagens;
        }
    }
}

using Mensageiro.Excecoes;

namespace Mensageiro
{
    public class Notificador : NotificadorBase, INotificador
    {
        const string MSG_NAO_ENCONTRADO = "NAO_ENCONTRADO";

        public void AddMensagem(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                return;

            var mensagemEmString = mensagem;
            AddMensagemLista(mensagemEmString);
        }

        public void AddMensagem<T>(T mensagem) where T : struct, IConvertible
        {
            var mensagemEmString = mensagem.Descricao();
            if (string.IsNullOrEmpty(mensagemEmString) || string.IsNullOrWhiteSpace(mensagemEmString))
            {
                MensageiroExcecao.MensagemNotificacaoVazia();
                return;
            }

            AddMensagemLista(mensagemEmString);
        }

        public void AddMensagemNaoEncontrado(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                return;

            AddMensagemLista(
                mensagem: mensagem,
                chave: MSG_NAO_ENCONTRADO);
        }

        public void AddMensagemNaoEncontrado<T>(T mensagem) where T : struct, IConvertible
        {
            var mensagemEmString = mensagem.Descricao();
            if (string.IsNullOrEmpty(mensagemEmString) || string.IsNullOrWhiteSpace(mensagemEmString))
            {
                MensageiroExcecao.MensagemNotificacaoVazia();
                return;
            }

            AddMensagemLista(
                mensagem: mensagemEmString,
                chave: MSG_NAO_ENCONTRADO);
        }

        public bool ExisteMensagem()
        {
            return ListaMensagens().Count > 0;
        }

        public bool ExisteMsgNaoEncontrado()
        {
            return ListaMensagens()
                .Any(m => m.Key.Equals(MSG_NAO_ENCONTRADO));
        }

        public IEnumerable<string> Mensagens()
        {
            return ListaMensagens().Values;
        }

        public IEnumerable<string> MensagensDeNaoEncontrado()
        {
            return ListaMensagens()
                .Where(m => m.Key.Equals(MSG_NAO_ENCONTRADO))
                .Select(m => m.Value);
        }
    }
}

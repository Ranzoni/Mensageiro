
using Mensageiro.Excecoes;

namespace Mensageiro
{
    public class Notificador : INotificador
    {
        private readonly List<string> _mensagens = [];

        public void AddMensagem(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                return;

            var mensagemEmString = mensagem;
            _mensagens.Add(mensagemEmString);
        }

        public void AddMensagem<T>(T mensagem) where T : struct, IConvertible
        {
            var mensagemEmString = mensagem.Descricao();
            if (string.IsNullOrEmpty(mensagemEmString) || string.IsNullOrWhiteSpace(mensagemEmString))
            {
                MensageiroExcecao.MensagemNotificacaoVazia();
                return;
            }

            _mensagens.Add(mensagemEmString);
        }

        public bool ExisteMensagem()
        {
            return _mensagens.Count > 0;
        }

        public IEnumerable<string> Mensagens()
        {
            return _mensagens;
        }
    }
}

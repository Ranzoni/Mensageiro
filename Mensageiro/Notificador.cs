using Mensageiro.Excecoes;

namespace Mensageiro
{
    /// <summary>
    /// Classe responsável por gerenciar mensagens de notificação.
    /// </summary>
    public class Notificador : INotificador
    {
        const string MSG_PADRAO = "PADRAO";
        const string MSG_NAO_ENCONTRADO = "NAO_ENCONTRADO";
        const string MSG_NAO_AUTORIZADO = "MSG_NAO_AUTORIZADO";

        private readonly Dictionary<string, string> _mensagens = [];

        private void AddMensagemLista(string mensagem, string chave = MSG_PADRAO)
        {
            if (string.IsNullOrEmpty(chave) || string.IsNullOrWhiteSpace(chave))
                chave = MSG_PADRAO;

            _mensagens.Add(chave, mensagem);
        }

        /// <summary>
        /// Adiciona uma mensagem do tipo string ao notificador.
        /// </summary>
        /// <param name="mensagem"></param>
        public void AddMensagem(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                return;

            var mensagemEmString = mensagem;
            AddMensagemLista(mensagemEmString);
        }

        /// <summary>
        /// Adiciona uma mensagem do tipo enum ao notificador.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mensagem"></param>
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

        /// <summary>
        /// Adiciona uma mensagem de não encontrado do tipo string ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não encontrado".
        /// </summary>
        /// <param name="mensagem"></param>
        public void AddMensagemNaoEncontrado(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                return;

            AddMensagemLista(
                mensagem: mensagem,
                chave: MSG_NAO_ENCONTRADO);
        }

        /// <summary>
        /// Adiciona uma mensagem de não encontrado do tipo enum ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não encontrado".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mensagem"></param>
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

        /// <summary>
        /// Retorna verdadeiro caso exista alguma mensagem no notificador, caso contrário retorna falso.
        /// </summary>
        /// <returns></returns>
        public bool ExisteMensagem()
        {
            return _mensagens.Count > 0;
        }

        /// <summary>
        /// Retorna verdadeiro caso exista alguma mensagem no notificador do tipo 'não encontrado', caso contrário retorna falso.
        /// </summary>
        /// <returns></returns>
        public bool ExisteMsgNaoEncontrado()
        {
            return _mensagens.Any(m => m.Key.Equals(MSG_NAO_ENCONTRADO));
        }

        /// <summary>
        /// Retorna todas as mensagens do notificador.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Mensagens()
        {
            return _mensagens.Values;
        }

        /// <summary>
        /// Retorna todas as mensagens de não encontrado do notificador.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> MensagensDeNaoEncontrado()
        {
            return _mensagens
                .Where(m => m.Key.Equals(MSG_NAO_ENCONTRADO))
                .Select(m => m.Value);
        }

        /// <summary>
        /// Adiciona uma mensagem de não autorizado do tipo string ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não autorizado".
        /// </summary>
        /// <param name="mensagem"></param>
        public void AddMensagemNaoAutorizado(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem) || string.IsNullOrWhiteSpace(mensagem))
                return;

            AddMensagemLista(
                mensagem: mensagem,
                chave: MSG_NAO_AUTORIZADO);
        }

        /// <summary>
        /// Adiciona uma mensagem de não autorizado do tipo enum ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não autorizado".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mensagem"></param>
        public void AddMensagemNaoAutorizado<T>(T mensagem) where T : struct, IConvertible
        {
            var mensagemEmString = mensagem.Descricao();
            if (string.IsNullOrEmpty(mensagemEmString) || string.IsNullOrWhiteSpace(mensagemEmString))
            {
                MensageiroExcecao.MensagemNotificacaoVazia();
                return;
            }

            AddMensagemLista(
                mensagem: mensagemEmString,
                chave: MSG_NAO_AUTORIZADO);
        }

        /// <summary>
        /// Retorna verdadeiro caso exista alguma mensagem no notificador do tipo 'não autorizado', caso contrário retorna falso.
        /// </summary>
        /// <returns></returns>
        public bool ExisteMsgNaoAutorizado()
        {
            return _mensagens.Any(m => m.Key.Equals(MSG_NAO_AUTORIZADO));
        }

        /// <summary>
        /// Retorna todas as mensagens de não autorizado do notificador.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> MensagensDeNaoAutorizado()
        {
            return _mensagens
                .Where(m => m.Key.Equals(MSG_NAO_AUTORIZADO))
                .Select(m => m.Value);
        }
    }
}

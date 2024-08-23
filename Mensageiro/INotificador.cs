namespace Mensageiro
{
    /// <summary>
    /// Interface responsável por gerenciar mensagens de notificação.
    /// </summary>
    public interface INotificador
    {
        /// <summary>
        /// Adiciona uma mensagem do tipo string ao notificador.
        /// </summary>
        /// <param name="mensagem"></param>
        void AddMensagem(string mensagem);
        /// <summary>
        /// Adiciona uma mensagem do tipo enum ao notificador.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mensagem"></param>
        void AddMensagem<T>(T mensagem) where T : struct, IConvertible;
        /// <summary>
        /// Adiciona uma mensagem de não encontrado do tipo string ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não encontrado".
        /// </summary>
        /// <param name="mensagem"></param>
        void AddMensagemNaoEncontrado(string mensagem);
        // <summary>
        /// Adiciona uma mensagem de não encontrado do tipo enum ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não encontrado".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mensagem"></param>
        void AddMensagemNaoEncontrado<T>(T mensagem) where T : struct, IConvertible;
        /// <summary>
        /// Retorna verdadeiro caso exista alguma mensagem no notificador, caso contrário retorna falso.
        /// </summary>
        /// <returns></returns>
        bool ExisteMensagem();
        /// <summary>
        /// Retorna verdadeiro caso exista alguma mensagem no notificador do tipo 'não encontrado', caso contrário retorna falso.
        /// </summary>
        /// <returns></returns>
        bool ExisteMsgNaoEncontrado();
        /// <summary>
        /// Retorna todas as mensagens do notificador.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> Mensagens();
        /// <summary>
        /// Retorna todas as mensagens de não encontrado do notificador.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> MensagensDeNaoEncontrado();
    }
}

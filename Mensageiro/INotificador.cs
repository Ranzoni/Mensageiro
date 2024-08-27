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
        /// <summary>
        /// Adiciona uma mensagem de não encontrado do tipo enum ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não encontrado".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mensagem"></param>
        void AddMensagemNaoEncontrado<T>(T mensagem) where T : struct, IConvertible;
        /// <summary>
        /// Adiciona uma mensagem de não autorizado do tipo string ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não autorizado".
        /// </summary>
        /// <param name="mensagem"></param>
        void AddMensagemNaoAutorizado(string mensagem);
        /// <summary>
        /// Adiciona uma mensagem de não autorizado do tipo enum ao notificador.
        /// É o mesmo efeito do AddMensagem, porém é adicionada uma identificação para a mensagem de "não autorizado".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mensagem"></param>
        void AddMensagemNaoAutorizado<T>(T mensagem) where T : struct, IConvertible;
        /// <summary>
        /// Verifica se existe alguma mensagem no notificador
        /// </summary>
        /// <returns>Retorna true caso exista uma mensagem no notificador; caso contrário retorna false.</returns>
        bool ExisteMensagem();
        /// <summary>
        /// Verifica se existe alguma mensagem no notificador do tipo 'não encontrado'
        /// </summary>
        /// <returns>Retorna true caso exista uma mensagem no notificador do tipo 'não encontrado'; caso contrário retorna false.</returns>
        bool ExisteMsgNaoEncontrado();
        /// <summary>
        /// Verifica se existe alguma mensagem no notificador do tipo 'não autorizado'
        /// </summary>
        /// <returns>Retorna true caso exista uma mensagem no notificador do tipo 'não autorizado'; caso contrário retorna false.</returns>
        bool ExisteMsgNaoAutorizado();
        /// <summary>
        /// Retorna todas as mensagens do notificador.
        /// </summary>
        /// <returns>Retorna um IEnumerable de strings.</returns>
        IEnumerable<string> Mensagens();
        /// <summary>
        /// Retorna todas as mensagens de não encontrado do notificador.
        /// </summary>
        /// <returns>Retorna um IEnumerable de strings.</returns>
        IEnumerable<string> MensagensDeNaoEncontrado();
        /// <summary>
        /// Retorna todas as mensagens de não autorizado do notificador.
        /// </summary>
        /// <returns>Retorna um IEnumerable de strings.</returns>
        IEnumerable<string> MensagensDeNaoAutorizado();
    }
}

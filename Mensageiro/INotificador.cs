namespace Mensageiro
{
    public interface INotificador
    {
        void AddMensagem(string mensagem);
        void AddMensagem<T>(T mensagem) where T : struct, IConvertible;
        void AddMensagemNaoEncontrado(string mensagem);
        void AddMensagemNaoEncontrado<T>(T mensagem) where T : struct, IConvertible;
        bool ExisteMensagem();
        bool ExisteMsgNaoEncontrado();
        IEnumerable<string> Mensagens();
        IEnumerable<string> MensagensDeNaoEncontrado();
    }
}

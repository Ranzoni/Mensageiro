namespace Mensageiro
{
    public interface INotificador
    {
        void AddMensagem(string mensagem);
        void AddMensagem<T>(T mensagem) where T : struct, IConvertible;
        bool ExisteMensagem();
        IEnumerable<string> Mensagens();
    }
}

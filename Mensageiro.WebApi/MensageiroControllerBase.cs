using Microsoft.AspNetCore.Mvc;

namespace Mensageiro.WebApi
{
    /// <summary>
    /// Esta classe possui métodos que retornam um ActionResult, onde é tratado se existe mensagem no Notificador, e em caso positivo retornam elas como resposta ao controlador.
    /// Mensagens do tipo 'não encontrado' serão retornadas como 404 NotFound.
    /// </summary>
    /// <param name="_notificador"></param>
    /// <param name="_statusCodeNotificador"></param>
    public abstract class MensageiroControllerBase(INotificador _notificador, int? _statusCodeNotificador = null) : ControllerBase
    {
        /// <summary>
        /// Caso exista mensagem no notificador irá retornar.
        /// Caso contrário, retorna um código de status 201 (Criado).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="retorno"></param>
        /// <returns></returns>
        protected ActionResult<T?> CriadoComSucesso<T>(T? retorno)
        {
            var actionResultMensagemValidacao = VerificarMensagens();
            if (actionResultMensagemValidacao is not null)
                return actionResultMensagemValidacao;

            if (retorno is null)
                return StatusCode(201);
            else
                return StatusCode(201, retorno);
        }

        /// <summary>
        /// Caso exista mensagem no notificador irá retornar.
        /// Caso contrário, retorna um código de status 200 (Ok).
        /// </summary>
        /// <param name="retorno"></param>
        /// <returns></returns>
        protected ActionResult Sucesso(object retorno)
        {
            var actionResultMensagemValidacao = VerificarMensagens();
            if (actionResultMensagemValidacao is not null)
                return actionResultMensagemValidacao;

            return Ok(retorno);
        }

        /// <summary>
        /// Caso exista mensagem no notificador irá retornar.
        /// Caso contrário, retorna um código de status que foi definido pelo parâmetro 'statusCode'.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="retorno"></param>
        /// <returns></returns>
        protected ActionResult RetornarStatus(int statusCode, object? retorno)
        {
            var actionResultMensagemValidacao = VerificarMensagens();
            if (actionResultMensagemValidacao is not null)
                return actionResultMensagemValidacao;

            if (retorno is null)
                return StatusCode(statusCode);
            else
                return StatusCode(statusCode, retorno);
        }

        private ActionResult? VerificarMensagens()
        {
            if (_notificador.ExisteMsgNaoEncontrado())
            {
                var listaMsgs = RetornarNotificacaoResposta(_notificador.MensagensDeNaoEncontrado());
                return NotFound(listaMsgs);
            }

            if (_notificador.ExisteMensagem())
            {
                if (_statusCodeNotificador is not null)
                {
                    var listaMsgs = RetornarNotificacaoResposta(_notificador.Mensagens());
                    return StatusCode(_statusCodeNotificador ?? 0, listaMsgs);
                }
                else
                {
                    var listaMsgs = RetornarNotificacaoResposta(_notificador.Mensagens());
                    return UnprocessableEntity(listaMsgs);
                }
            }

            return null;
        }

        private static List<NotificacaoResposta> RetornarNotificacaoResposta(IEnumerable<string> mensagens)
        {
            var listaNotificacoesResposta = new List<NotificacaoResposta>();

            var construtor = new NotificacaoRespostaConstrutor();

            foreach (var mensagem in mensagens)
            {
                var notificacaoResposta = construtor
                    .ComMensagem(mensagem)
                    .Construir();

                listaNotificacoesResposta.Add(notificacaoResposta);
            }

            return listaNotificacoesResposta;
        }

        /// <summary>
        /// Retorna o notificador do controlador
        /// </summary>
        /// <returns></returns>
        protected INotificador Notificador()
        {
            return _notificador;
        }
    }
}

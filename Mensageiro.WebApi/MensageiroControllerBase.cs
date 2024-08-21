using Microsoft.AspNetCore.Mvc;

namespace Mensageiro.WebApi
{
    public abstract class MensageiroControllerBase(INotificador _notificador, int? _statusCodeNotificador = null) : ControllerBase
    {
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

        protected ActionResult Sucesso(object retorno)
        {
            var actionResultMensagemValidacao = VerificarMensagens();
            if (actionResultMensagemValidacao is not null)
                return actionResultMensagemValidacao;

            return Ok(retorno);
        }

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

        protected INotificador Notificador()
        {
            return _notificador;
        }
    }
}

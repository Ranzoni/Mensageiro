﻿using Microsoft.AspNetCore.Mvc;

namespace Mensageiro.WebApi
{
    public abstract class MensageiroControllerBase(INotificador _notificador, int? _statusCodeNotificador = null) : ControllerBase
    {
        protected ActionResult<T?> CriadoComSucesso<T>(T? retorno)
        {
            var actionResultMensagemValidacao = VerificarMensagens();
            if (actionResultMensagemValidacao is not null)
                return actionResultMensagemValidacao;

            return StatusCode(201, retorno);
        }

        protected ActionResult Sucesso(object retorno)
        {
            var actionResultMensagemValidacao = VerificarMensagens();
            if (actionResultMensagemValidacao is not null)
                return actionResultMensagemValidacao;

            return Ok(retorno);
        }

        private ActionResult? VerificarMensagens()
        {
            if (_notificador.ExisteMsgNaoEncontrado())
                return NotFound(_notificador.MensagensDeNaoEncontrado());

            if (_notificador.ExisteMensagem())
                if (_statusCodeNotificador is not null)
                    return StatusCode(_statusCodeNotificador ?? 0, _notificador.Mensagens());
                else
                    return UnprocessableEntity(_notificador.Mensagens());

            return null;
        }
    }
}

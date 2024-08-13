using Mensageiro.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Mensageiro.Teste.Mensageiro.WebApi
{
    public class MensageiroControllerBaseTeste
    {
        [Fact]
        internal void DeveRetornarCriadoComSucesso()
        {
            var entidade = new
            {
                Nome = "Fulano"
            };
            var notificador = new Notificador();
            var controllerTeste = new ControllerTeste(notificador);

            var retorno = controllerTeste.CriarEntidade(entidade);

            Assert.NotNull(retorno.Result);
            Assert.Equal(entidade, retorno.Value);
            Assert.Equal(201, ((IStatusCodeActionResult)retorno.Result).StatusCode);
        }
    }

    internal class ControllerTeste(INotificador notificador) : MensageiroControllerBase(notificador)
    {
        internal ActionResult<object?> CriarEntidade(object entidade)
        {
            return CriadoComSucesso(entidade);
        }
    }
}

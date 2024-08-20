using Bogus;
using Mensageiro.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Mensageiro.Teste.Mensageiro.WebApi
{
    public class MensageiroControllerBaseTeste
    {
        private readonly Faker _faker = new();

        [Fact]
        internal void DeveRetornarCriadoComSucesso()
        {
            var entidade = new
            {
                Nome = "Fulano"
            };
            var (controller, notificador) = CriarControllerTeste();

            var retorno = controller.CriarEntidade(entidade);

            Assert.NotNull(retorno.Result);
            Assert.IsType<ObjectResult>(retorno.Result);
            if (retorno.Result is ObjectResult objResult)
                Assert.Equal(entidade, objResult.Value);
            Assert.Equal(201, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.Empty(notificador.Mensagens());
        }

        [Fact]
        internal void NaoDeveRetornarCriadoComSucesso()
        {
            var entidade = new
            {
                Nome = "Fulano"
            };
            var (controller, notificador) = CriarControllerTeste();
            var mensagem = _faker.Lorem.Sentences();
            notificador.AddMensagem(mensagem);

            var retorno = controller.CriarEntidade(entidade);

            Assert.NotNull(retorno.Result);
            Assert.IsType<UnprocessableEntityObjectResult>(retorno.Result);
            if (retorno.Result is UnprocessableEntityObjectResult unprocessable)
            {
                var valueCollection = unprocessable.Value as Dictionary<string, string>.ValueCollection;
                var mensagens = valueCollection?.ToList();
                Assert.Equal(mensagem, mensagens?.FirstOrDefault());
            }
            Assert.Equal(422, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.NotEmpty(notificador.Mensagens());
        }

        [Fact]
        internal void DeveRetornarNaoEncontradoParaCriadoComSucesso()
        {
            var entidade = new
            {
                Nome = "Fulano"
            };
            var (controller, notificador) = CriarControllerTeste();
            var mensagem = "Não encontrado";
            notificador.AddMensagemNaoEncontrado(mensagem);

            var retorno = controller.CriarEntidade(entidade);

            Assert.NotNull(retorno.Result);
            Assert.IsType<NotFoundObjectResult>(retorno.Result);
            if (retorno.Result is NotFoundObjectResult notFound)
            {
                var valueCollection = notFound.Value as IEnumerable<string>;
                var mensagens = valueCollection?.ToList();
                Assert.Equal(mensagem, mensagens?.FirstOrDefault());
            }
            Assert.Equal(404, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.NotEmpty(notificador.Mensagens());
        }

        [Fact]
        internal void DeveRetornaStatusPersonalizadoParaCriadoComSucesso()
        {
            var entidade = new
            {
                Nome = "Fulano"
            };
            var statusCode = 400;
            var (controller, notificador) = CriarControllerTeste(statusCode);
            var mensagem = _faker.Lorem.Sentences();
            notificador.AddMensagem(mensagem);

            var retorno = controller.CriarEntidade(entidade);

            Assert.NotNull(retorno.Result);
            Assert.IsType<ObjectResult>(retorno.Result);
            if (retorno.Result is ObjectResult objResult)
            {
                var valueCollection = objResult.Value as IEnumerable<string>;
                var mensagens = valueCollection?.ToList();
                Assert.Equal(mensagem, mensagens?.FirstOrDefault());
            }
            Assert.Equal(statusCode, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.NotEmpty(notificador.Mensagens());
        }

        [Fact]
        internal void DeveRetornarOk()
        {
            var id = Guid.NewGuid();
            var (controller, notificador) = CriarControllerTeste();

            var retorno = controller.BuscarEntidade(id);

            Assert.NotNull(retorno.Result);
            Assert.IsType<OkObjectResult>(retorno.Result);
            if (retorno.Result is OkObjectResult okResult)
            {
                var entidade = okResult.Value;
                var idRetornado = entidade?.GetType().GetProperty("Id")?.GetValue(entidade);
                Assert.Equal(id, idRetornado);
            }
            Assert.Equal(200, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.Empty(notificador.Mensagens());
        }

        [Fact]
        internal void NaoDeveRetornarOk()
        {
            var id = Guid.NewGuid();
            var (controller, notificador) = CriarControllerTeste();
            var mensagem = _faker.Lorem.Sentences();
            notificador.AddMensagem(mensagem);

            var retorno = controller.BuscarEntidade(id);

            Assert.NotNull(retorno.Result);
            Assert.IsType<UnprocessableEntityObjectResult>(retorno.Result);
            if (retorno.Result is UnprocessableEntityObjectResult unprocessable)
            {
                var valueCollection = unprocessable.Value as Dictionary<string, string>.ValueCollection;
                var mensagens = valueCollection?.ToList();
                Assert.Equal(mensagem, mensagens?.FirstOrDefault());
            }
            Assert.Equal(422, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.NotEmpty(notificador.Mensagens());
        }

        [Fact]
        internal void DeveRetornarNaoEncontradoParaOk()
        {
            var id = Guid.NewGuid();
            var (controller, notificador) = CriarControllerTeste();
            var mensagem = "Não encontrado";
            notificador.AddMensagemNaoEncontrado(mensagem);

            var retorno = controller.BuscarEntidade(id);

            Assert.NotNull(retorno.Result);
            Assert.IsType<NotFoundObjectResult>(retorno.Result);
            if (retorno.Result is NotFoundObjectResult notFound)
            {
                var valueCollection = notFound.Value as IEnumerable<string>;
                var mensagens = valueCollection?.ToList();
                Assert.Equal(mensagem, mensagens?.FirstOrDefault());
            }
            Assert.Equal(404, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.NotEmpty(notificador.Mensagens());
        }

        [Fact]
        internal void DeveRetornarStatusPersonalizadoParaOk()
        {
            var id = Guid.NewGuid();
            var statusCode = 400;
            var (controller, notificador) = CriarControllerTeste(statusCode);
            var mensagem = _faker.Lorem.Sentences();
            notificador.AddMensagem(mensagem);

            var retorno = controller.BuscarEntidade(id);

            Assert.NotNull(retorno.Result);
            Assert.IsType<ObjectResult>(retorno.Result);
            if (retorno.Result is ObjectResult objRessult)
            {
                var valueCollection = objRessult.Value as IEnumerable<string>;
                var mensagens = valueCollection?.ToList();
                Assert.Equal(mensagem, mensagens?.FirstOrDefault());
            }
            Assert.Equal(statusCode, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.NotEmpty(notificador.Mensagens());
        }

        [Fact]
        internal void DeveRetornarStatusEspecifico()
        {
            var id = Guid.NewGuid();
            var (controller, notificador) = CriarControllerTeste();

            var retorno = controller.BuscarEntidadeComStatusEspecifico(id);

            Assert.NotNull(retorno.Result);
            Assert.IsType<ObjectResult>(retorno.Result);
            if (retorno.Result is ObjectResult objResult)
            {
                var entidade = objResult.Value;
                var idRetornado = entidade?.GetType().GetProperty("Id")?.GetValue(entidade);
                Assert.Equal(id, idRetornado);
            }
            Assert.Equal(202, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.Empty(notificador.Mensagens());
        }

        [Fact]
        internal void NaoDeveRetornarStatusEspecifico()
        {
            var id = Guid.NewGuid();
            var (controller, notificador) = CriarControllerTeste();
            var mensagem = _faker.Lorem.Sentences();
            notificador.AddMensagem(mensagem);

            var retorno = controller.BuscarEntidadeComStatusEspecifico(id);

            Assert.NotNull(retorno.Result);
            Assert.IsType<UnprocessableEntityObjectResult>(retorno.Result);
            if (retorno.Result is UnprocessableEntityObjectResult unprocessable)
            {
                var valueCollection = unprocessable.Value as Dictionary<string, string>.ValueCollection;
                var mensagens = valueCollection?.ToList();
                Assert.Equal(mensagem, mensagens?.FirstOrDefault());
            }
            Assert.Equal(422, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.NotEmpty(notificador.Mensagens());
        }

        [Fact]
        internal void DeveRetornarNaoEncontradoStatusEspecifico()
        {
            var id = Guid.NewGuid();
            var (controller, notificador) = CriarControllerTeste();
            var mensagem = _faker.Lorem.Sentences();
            notificador.AddMensagemNaoEncontrado(mensagem);

            var retorno = controller.BuscarEntidadeComStatusEspecifico(id);

            Assert.NotNull(retorno.Result);
            Assert.IsType<NotFoundObjectResult>(retorno.Result);
            if (retorno.Result is NotFoundObjectResult notFound)
            {
                var valueCollection = notFound.Value as IEnumerable<string>;
                var mensagens = valueCollection?.ToList();
                Assert.Equal(mensagem, mensagens?.FirstOrDefault());
            }
            Assert.Equal(404, ((IStatusCodeActionResult)retorno.Result).StatusCode);
            Assert.NotEmpty(notificador.Mensagens());
        }

        [Fact]
        internal void DeveRetornarNotificador()
        {
            var msg = _faker.Lorem.Sentences();
            var (controller, notificador) = CriarControllerTeste();

            var retorno = controller.RetornarNotificador();
            retorno.AddMensagem(msg);

            Assert.NotNull(retorno);
            Assert.Equal(notificador, retorno);
            Assert.True(notificador.ExisteMensagem());
            Assert.Equal(msg, notificador.Mensagens().First());
        }

        private static (ControllerTeste controller, Notificador notificador) CriarControllerTeste(int? statusCodeMensageiro = null)
        {
            var notificador = new Notificador();
            var controller = new ControllerTeste(notificador, statusCodeMensageiro);

            return (controller, notificador);
        }
    }

    internal class ControllerTeste(INotificador notificador, int? statusCodeMensageiro = null) : MensageiroControllerBase(notificador, statusCodeMensageiro)
    {
        internal ActionResult<object?> CriarEntidade(object entidade)
        {
            return CriadoComSucesso(entidade);
        }

        internal ActionResult<object?> BuscarEntidade(Guid id)
        {
            var entidade = new
            {
                Id = id,
                Nome = "Fulano"
            };

            return Sucesso(entidade);
        }

        internal ActionResult<object?> BuscarEntidadeComStatusEspecifico(Guid id)
        {
            var entidade = new
            {
                Id = id,
                Nome = "Fulano"
            };

            return RetornarStatus(202, entidade);
        }

        internal INotificador RetornarNotificador()
        {
            return Notificador();
        }
    }
}

﻿using Bogus;
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

        private static (ControllerTeste controller, Notificador notificador) CriarControllerTeste()
        {
            var notificador = new Notificador();
            var controller = new ControllerTeste(notificador);

            return (controller, notificador);
        }
    }

    internal class ControllerTeste(INotificador notificador) : MensageiroControllerBase(notificador)
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
    }
}
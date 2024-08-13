using System.ComponentModel;

namespace Mensageiro.Teste.Mensageiro
{
    internal enum EMensagemTeste
    {
        [Description("Este é um teste de mensagem")]
        MensagemTeste,
        [Description()]
        MensagemTesteDescricaoSemParametro,
        [Description("")]
        MensagemTesteDescricaoVazia,
        [Description("   ")]
        MensagemTesteDescricaoEspacoEmBranco,
    }
}

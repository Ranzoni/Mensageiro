using System.ComponentModel;

namespace Mensageiro.Teste
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

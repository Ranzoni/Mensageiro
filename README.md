# Mensageiro

## Para que usar o mensageiro?

Cansado de ter que criar *Exceptions* no seu projeto para tratar validações? O mensageiro propõe a solução.

### Ele é um gerenciador de mensagens para você utilizar. Basta você criar uma instância dele, adicionar as mensagens e recuperar quando for necessário.

## Como usar?

### Mensageiro padrão:

O mensageiro padrão possui quatro métodos para adicionar uma mensagem. São eles:

**void AddMensagem(string mensagem)**
	Adiciona uma mensagem em texto.
	
**void AddMensagem<T>(T mensagem) where T : struct, IConvertible**
	Permite adicionar uma mensagem em Enum.
	**ATENÇÃO: O ENUM PRECISA TER UM DESCRIPTION COM O CONTEÚDO DA MENSAGEM PARA CORRETO FUNCIONAMENTO!!**
	
**void AddMensagemNaoEncontrado(string mensagem)**
	Adiciona uma mensagem que fica identificada pelo notificador como sendo uma mensagem para algo que não foi encontrado.

**void AddMensagemNaoEncontrado<T>(T mensagem) where T : struct, IConvertible**
	O mesmo caso de cima, mas aceitando um Enum.
	**ATENÇÃO: O ENUM PRECISA TER UM DESCRIPTION COM O CONTEÚDO DA MENSAGEM PARA CORRETO FUNCIONAMENTO!!**
	
Você também pode recuperar estas notificações:

**bool ExisteMensagem()**
	Verifica se existem mensagens no notificador.
	
**bool ExisteMsgNaoEncontrado()**
	Verifica se existe mensagem do tipo *Não Encontrado*.

**IEnumerable<string> Mensagens()**
	Retorna todas as mensagens inseridas no notificador no formato de string, independente de como foram adicionadas.

**IEnumerable<string> MensagensDeNaoEncontrado()**
	Retorna todas as mensagens do tipo *Não Encontrado* inseridas no notificador no formato de string, independente de como foram adicionadas.
	
### Mensageiro Web API:

Quer retornar suas mensagens pelos os seus endpoints de forma dinâmica? O Mensageiro Web API pode ajudar.

Esta biblioteca foi criada com o intuito de integrar *ActionResult* com o *Notificador*. Para utilizar você deve fazer o seguinte:

1. Fazer o seu controller herdar da classe *MensageiroControllerBase*. Ele já herda do *ControllerBase*, não precisa fazer o seu controller herdar também.
2. Passar para o *MensageiroControllerBase* o *Notificador* através do seu construtor.

Com o controller configurado, você só precisa usar os métodos dele para retornar *ActionResult*.

**ATENÇÃO: Todos os métodos dele tratam se existem mensagems no notificador. Em caso positivo, ele retorna as mensagens no *response* **
**Se uma mensagem foi adicionada ao *Notificador* como *NãoEncontrado* o *ActionResult* será um *NotFound* **

Estes são os métodos do MensageiroControllerBase:

**ActionResult<T?> CriadoComSucesso<T>(T? retorno)**
	Este método espera um parâmetro qualquer (pode ser nulo). Ele irá retornar um *StatusCode(201)*.
	
**ActionResult Sucesso(object retorno)**
	Este método espera um parâmetro qualquer. Ele irá retornar um *Ok()*
	
**ActionResult RetornarStatus(int statusCode, object? retorno)**
	Este método espera um parâmetro qualquer (pode ser nulo). Ele irá retornar um código de status que você passou pelo parâmetro.
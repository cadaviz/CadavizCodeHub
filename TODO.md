
Como ver os diagramas.

PlantUML extension.
Para visualizar os diagramas é necessário ter o Java e o GraphViz instalados.

For windows, you can go here to download GraphViz for Windows. http://www.graphviz.org/download/
We can assume that you already have Java installed for Windows 10, but if you don't, you will need to download Java for Windows.  https://www.java.com/download/ie_manual.jsp


TODO List:


- Adicionar metrics
- Adicionar logs
- Revisar swagger
- Adicionar SQL Server database
  - EF
  - Dapper
- Serviço de pagamentos
  - polimorfismo com metodos de pagamento
  - polimorfismo com metodos de entrega
- Diagrama de sequencia.
- Adicionar chamadas de api sincronas e assincronas
- polly, hangfire ou outro retry
- idempotencia
- Adicionar docker
- Adicionar globalization para as mensagens
- Adicionar live and readyness probe
- Padronizar os retornos de erro/validação
- Atualizar readme
- Gerar o XML para o github
- Adicionar template de PR
- Adicionar description ao swagger
- Adicionar cache em memoria
- Evoluir para cache com redis
  - Criar um ambiente com 2 pods pra validar o cache

- Modelar classes como VO
  - Address
  - Email é um VO com operator
- Criar padrão stamped entity
  - Order
- Trabalhar com implementação nas interfaces (??)
- Validar headers de API
  - CORS
- Criar mais de uma versao de API
- Criar cache em memória
  - Usar chamada encadeada
- Criar autenticação
- Montar um cenario de cancelamento onde percorre varias entidades raiz

- domain service - implementa uma logica de negocio que nao cabe numa entidade
- app service - implementa um fluxo. Pode usar varios domains services
- infra service - implementa comunicações externas (pode ser via Domain service, ex: enviarEmailNovoCliente. eh uma domain service que usa a infra de enviar email)
		// repos
		        //TODO: dar um dinamismo
        //TODO: Implementar unit of work


		controller
		            //TODO: uri builder


					    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
        //where TRequest : CreateOrderRequest, IRequest




		//Startup
            //.ConfigureApiBehaviorOptions(options =>
            //{
            //    options.SuppressMapClientErrors = true;
            //    options.InvalidModelStateResponseFactory = context =>
            //    {
            //        var defaultErrorMessage = "Something went wrong!";
            //        var d = context.ModelState;
            //        var applicationMessages = new ApplicationMessage[] { new ApplicationMessage(defaultErrorMessage) };

            //        var response = new ApplicationErrorResponse(StatusCode: StatusCodes.Status400BadRequest,
            //                                                    Messages: applicationMessages);

            //        return new BadRequestObjectResult(response)
            //        {
            //            ContentTypes = { Application.Json }
            //        };
            //    };
            //});

            //ADICIONAR FILTRO ANTES DE EXECUTAR PARA VALIDAR O REQUEST DINAMICAMENTE
                //Farfetch.LookupService.Domain.Common.Extensions
            

            //app.UseExceptionHandler()
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using CadavizCodeHub.Api.Mappers;
using CadavizCodeHub.Api.Requests;
using CadavizCodeHub.Api.Responses;
using CadavizCodeHub.Api.Validations;
using CadavizCodeHub.Domain.Services;
using CadavizCodeHub.Framework.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;

namespace CadavizCodeHub.Api.Controllers
{
    [ApiController]
    [Route(controllerName)]
    [Produces(MediaTypeNames.Application.Json)]
    public class OrderController : ControllerBase
    {
        private const string controllerName = "order";
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderCreationService;

        public OrderController(ILogger<OrderController> logger,
                               IOrderService orderCreationService) : base()
        {
            _logger = logger;
            _orderCreationService = orderCreationService;
        }

        /// <summary>
        /// Get an Order
        /// </summary>
        /// <param name="id" example="ef310f03-b3ce-45ef-b6e3-dd641840fb90">Order identifier</param>
        /// <returns>Requested order</returns>
        [HttpGet(Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderCreationService.GetOrderAsync(id);

            return OkOrNoContent(order.MapNullable());
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <remarks>
        /// Valid request example:
        ///
        ///     POST /order
        ///     
        ///     {
        ///       "items": [
        ///         {
        ///           "product": {
        ///             "description": "Playstation 5",
        ///             "price": 429.99
        ///           },
        ///           "quantity": 1
        ///         }
        ///       ]
        ///     }
        /// </remarks>
        /// <param name="request">The new order request body</param>
        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [SwaggerResponseHeader(StatusCodes.Status201Created, "Location", type: "string", description: $"{controllerName}/{{id}}")]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var validationResult = ValidateRequest<CreateOrderRequestValidator, CreateOrderRequest>(request);

            if (validationResult is not null)
            {
                return validationResult;
            }
            
            var order = request.Map();

            order = await _orderCreationService.CreateOrderAsync(order);

            var response = order.Map();
            var locationUri = BuildLocationUri(pathValue: $"{Request.Path}/{order.Id}");

            return Created(locationUri, response);
        }
    }
}

using AutoMapper;
using CadavizCodeHub.Application.Services;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Framework.Responses;
using CadavizCodeHub.WebApi.Requests;
using CadavizCodeHub.WebApi.Responses;
using CadavizCodeHub.WebApi.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.WebApi.Controllers
{
    [ApiController]
    [Route(controllerName)]
    [Produces(MediaTypeNames.Application.Json)]
    public class OrderController : ControllerBase
    {
        private const string controllerName = "orders";
        private readonly IMapper _mapper;
        private readonly IOrderApplicationService _orderApplicationService;

        public OrderController(IMapper mapper, IOrderApplicationService orderCreationService) : base()
        {
            _mapper = mapper;
            _orderApplicationService = orderCreationService;
        }

        /// <summary>
        /// Get an Order
        /// </summary>
        /// <param name="id" example="ef310f03-b3ce-45ef-b6e3-dd641840fb90">Order identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>Requested order</returns>
        [HttpGet("{id}", Name = "GetOrderById")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderApplicationService.GetOrderAsync(id, cancellationToken);

            return OkOrNotFound(_mapper.Map<OrderResponse>(order));
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
        /// <param name="cancellationToken">The cancellation token</param>
        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [SwaggerResponseHeader(StatusCodes.Status201Created, "Location", type: "string", description: $"{controllerName}/{{id}}")]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var validationResult = ValidateRequest<CreateOrderRequestValidator, CreateOrderRequest>(request);

            if (validationResult is not null)
            {
                return validationResult;
            }

            var order = _mapper.Map<Order>(request);

            order = await _orderApplicationService.CreateOrderAsync(order, cancellationToken);

            var response = _mapper.Map<OrderResponse>(order);

            return CreatedAtAction(nameof(GetOrderById), new { id = response.Id }, response);
        }
    }
}

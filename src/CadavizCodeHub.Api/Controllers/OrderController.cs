using System;
using System.Net.Mime;
using CadavizCodeHub.Api.Mappers;
using CadavizCodeHub.Api.Requests;
using CadavizCodeHub.Api.Responses;
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
                               IOrderService orderCreationService)
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
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status500InternalServerError)]
        public OrderResponse GetOrder(Guid id)
        {
            throw new NotImplementedException();

            var order = _orderCreationService.GetOrder(id);
            return order.Map();
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponseHeader(StatusCodes.Status201Created, "Location", type: "string", description: $"{controllerName}/{{id}}")]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status500InternalServerError)]
        public void CreateOrder(CreateOrderRequest request)
        {
            var order = request.Map();
            order = _orderCreationService.CreateOrder(order);
            //TODO: uri builder
            Response.Headers.Add("Location", $"{Request.Scheme}://{Request.Host}{Request.Path}/{order.Id}");
        }
    }
}

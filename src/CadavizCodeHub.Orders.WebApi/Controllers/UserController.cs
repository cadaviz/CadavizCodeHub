using AutoMapper;
using CadavizCodeHub.Core.WebApi.Responses;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Orders.Domain.Repositories;
using CadavizCodeHub.Orders.WebApi.Requests;
using CadavizCodeHub.Orders.WebApi.Responses;
using CadavizCodeHub.Orders.WebApi.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.Orders.WebApi.Controllers
{
    [ApiController]
    [Route(controllerName)]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserController : Core.WebApi.Controllers.ControllerBase
    {
        private const string controllerName = "users";
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(ILogger<UserController> logger, IMapper mapper, IUserRepository userRepository) : base(logger)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserById(id, cancellationToken);
            var response = _mapper.Map<UserResponse>(user);
            return OkOrNotFound(response);
        }

        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUsers(CancellationToken cancellationToken)
        {
            var users = _userRepository.GetAllUsers(cancellationToken);
            var response = _mapper.Map<List<UserResponse>>(users);
            return Ok(response);
        }

        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [SwaggerResponseHeader(StatusCodes.Status201Created, "Location", type: "string", description: $"{controllerName}/{{id}}")]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status400BadRequest)]
        public IActionResult AddUser([FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            var validationResult = ValidateRequest<CreateUserValidator, UserRequest>(request);

            if (validationResult is not null)
            {
                return validationResult;
            }

            var user = _mapper.Map<UserRequest, User>(request);
            user = _userRepository.AddUser(user, cancellationToken);
            var response = _mapper.Map<UserResponse>(user);

            return CreatedAtAction(nameof(AddUser), new { id = response.Id }, response);
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var response = _userRepository.DeleteUser(id, cancellationToken);
            return NoContentOrNotFound(response);
        }
    }
}
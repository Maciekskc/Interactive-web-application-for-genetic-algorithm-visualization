using API.Utilities;
using Application.Dtos.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Application.Utilities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult SendResponse(ServiceResponse response)
        {
            switch (response.ResponseType)
            {
                case HttpStatusCode.OK:
                    return Ok();

                case HttpStatusCode.Unauthorized:
                    return Unauthorized();

                case HttpStatusCode.Forbidden:
                    return Forbid();

                case HttpStatusCode.NotFound:
                    return NotFound(response.Errors);

                case HttpStatusCode.NoContent:
                    return NoContent();

                case HttpStatusCode.Created:
                    return StatusCode(201);

                default:
                    return BadRequest(new ErrorResponse(response.Errors));
            }
        }

        protected IActionResult SendResponse<T>(ServiceResponse<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return IsSubclassOfRawGeneric(typeof(PagedResponse<>), typeof(T)) ? Ok(response.Payload) : Ok(new Response<T>(response.Payload));

                case HttpStatusCode.Unauthorized:
                    return Unauthorized();

                case HttpStatusCode.NotFound:
                    return NotFound(response.Errors);

                case HttpStatusCode.Forbidden:
                    return Forbid();

                case HttpStatusCode.Created:
                    return Created("uri",response.Payload);

                default:
                    return BadRequest(new ErrorResponse(response.Errors));
            }
        }

        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}
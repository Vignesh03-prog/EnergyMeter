using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;

namespace SabEnergy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    // Helper for 200 OK
    protected ObjectResult OkResponse<T>(T data, string message = "Success")
    {
        return Ok(new apiResponseDto<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        });
    }

    // Helper for 404 Not Found
    protected ObjectResult NotFoundResponse(string message = "Resource not found")
    {
        return NotFound(new apiResponseDto<object>
        {
            IsSuccess = false,
            Message = message,
            Data = null
        });
    }

    // Helper for 400 Bad Request
    protected ObjectResult BadRequestResponse(string message = "Invalid request")
    {
        return BadRequest(new apiResponseDto<object>
        {
            IsSuccess = false,
            Message = message,
            Data = null
        });
    }

    // Helper for 500 Internal Server Error
    protected ObjectResult ErrorResponse(string message)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, new apiResponseDto<object>
        {
            IsSuccess = false,
            Message = message,
            Data = null
        });
    }

    // Helper for 403 Forbidden
    protected ObjectResult ForbiddenResponse(string message = "Access denied")
    {
        return StatusCode(StatusCodes.Status403Forbidden, new apiResponseDto<object>
        {
            IsSuccess = false,
            Message = message,
            Data = null
        });
    }

    // Helper to map FluentResult directly to the centralized response helpers
    protected ActionResult HandleResult<T>(Result<T> result, string successMessage = "Success")
    {
        if (result.IsSuccess)
        {
            return OkResponse(result.Value, successMessage);
        }

        var message = result.Errors.FirstOrDefault()?.Message ?? "An unexpected error occurred.";
        if (message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            return NotFoundResponse(message);

        return BadRequestResponse(message);
    }

    protected ActionResult HandleResult(Result result, string successMessage = "Success")
    {
        if (result.IsSuccess)
        {
            return OkResponse<object?>(null, successMessage);
        }

        var message = result.Errors.FirstOrDefault()?.Message ?? "An unexpected error occurred.";
        if (message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            return NotFoundResponse(message);

        return BadRequestResponse(message);
    }
}

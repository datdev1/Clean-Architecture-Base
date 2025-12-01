using Application.Common.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Extension
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess) return new OkObjectResult(result.Value);

            if (result.HasError<NotFoundError>())
                return new NotFoundObjectResult(result.Errors) { StatusCode = 404 };

            if (result.HasError<ForbiddenError>())
                return new ObjectResult(result.Errors) { StatusCode = 403 };

            return new BadRequestObjectResult(result.Errors);
        }
    }
}

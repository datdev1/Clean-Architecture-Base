using Application.Common.Errors;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extenstions
{
    public static class ResultResponseExtension
    {

        public static Result<T> ToResultOrNotFound<T>(this T? data, string message)
        {
            if(data is null)
            {
                return Result.Fail(new NotFoundError("User not found, please Register"));
            }

            return Result.Ok(data);
        }

    }
}

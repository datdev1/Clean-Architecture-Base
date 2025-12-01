using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors
{
    public class ValidationError : Error
    {
        public ValidationError(string message) : base(message)
        {

            Metadata.Add("ErrorCode", "400");
        }

        protected ValidationError()
        {
            Metadata.Add("ErrorCode", "400");
        }
    }
}

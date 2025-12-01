using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors
{
    public class ForbiddenError : Error
    {
        public ForbiddenError(string message) : base(message)
        {
            Metadata.Add("ErrorCode", "403");
        }

        protected ForbiddenError()
        {
            Metadata.Add("ErrorCode", "403");
        }
    }
}

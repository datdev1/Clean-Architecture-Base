using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors
{
    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message)
        {
            Metadata.Add("ErrorCode", "404");
        }

        protected NotFoundError()
        {
            Metadata.Add("ErrorCode", "404");
        }
    }
}

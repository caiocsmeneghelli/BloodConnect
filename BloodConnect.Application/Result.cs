using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public List<string> Errors { get; private set; }
        public object Value { get; private set; }

        public static Result Success(object value) => new Result { IsSuccess = true, Value = value };
        public static Result Failure(object value, List<string> errors) => new Result { Value = value, IsSuccess = false, Errors = errors };
    }
}

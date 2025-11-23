using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Shared.Common
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public static Result<T> Ok(T data, string message = "")
            => new() { Success = true, Data = data, Message = message };

        public static Result<T> Fail(string message, IEnumerable<string>? errors = null)
            => new() { Success = false, Message = message, Errors = errors };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Application.Common
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError => !IsSuccess;
        public string? Errors { get; set; }
        public T Data { get; set; }

        public Response(bool success, T data, string? error)
        {
            IsSuccess = success;
            Data = data;
            Errors = error;
        }

        public static Response<T> Success(T data)
        {
            return new Response<T>(true, data, null);
        }

        public static Response<T> Failure(string error)
        {
            return new Response<T>(false, default!, error);
        }
    }
}

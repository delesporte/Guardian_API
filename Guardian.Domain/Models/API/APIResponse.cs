﻿using System.Net;

namespace Guardian.Domain.Models.API
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages= new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}

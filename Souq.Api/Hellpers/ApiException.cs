﻿namespace Souq.Api.Hellpers
{
    public class ApiException : ResponseApi
    {
        public ApiException(int statusCode, string? message = null , string? details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}

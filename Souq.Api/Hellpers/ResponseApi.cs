namespace Souq.Api.Hellpers
{
    public class ResponseApi
    {
        public ResponseApi(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public string? GetMessageFromStatusCode(int statusCode) 
        {
          return statusCode switch 
            {
                200=>"Successfully",
                400=>"BadRequest",
                401=>"UnAuthorized",
                403 =>"Forbidden",
                404 => "Not Found",
                500=>"Server Error",
                409 => "Conflict",
                _ =>null,

            };     
        }
    }
}

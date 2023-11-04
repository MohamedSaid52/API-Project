namespace API.Errors
{
    public class APIResponse
    {
        public APIResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatusCode { get;  set; }
        public string Message { get; set; }

     
        private string GetDefaultMessageForStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "You Made ABad Request",
                401 => "You Are Not Authorize",
                404 => "Resource Not Found",
                500 => "Server is died",
                _ => string.Empty
            };
        }
    }
}

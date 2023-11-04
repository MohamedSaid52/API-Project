namespace API.Errors
{
    public class ApiException:APIResponse
    {
        public string Details { get; set; }
        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

    }
}

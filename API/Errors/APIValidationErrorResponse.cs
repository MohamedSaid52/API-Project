namespace API.Errors
{
    public class APIValidationErrorResponse:ApiException
    {
        public APIValidationErrorResponse():base(400)
        {
            
        }
        public IEnumerable<string> Errors { get; set; } 
    }
}

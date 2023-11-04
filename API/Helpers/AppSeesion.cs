namespace API.Helpers
{
    public class AppSeesion : IAppSeesion
    {
        protected IHttpContextAccessor HttpContextAccessor;
        public AppSeesion(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public string AuthorizeToken 
        {
            get
            {
                if (HttpContextAccessor.HttpContext.Request.Headers["Authorize"].Any()) ;
                return HttpContextAccessor.HttpContext.Request.Headers["Authorize"].FirstOrDefault();
                return string.Empty;
            }
        }

        public string UserName => HttpContextAccessor?.HttpContext?.User.GetDisplayName();
    }
}

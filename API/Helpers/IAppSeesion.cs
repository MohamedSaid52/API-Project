namespace API.Helpers
{
    public interface IAppSeesion
    {
        string AuthorizeToken { get; }
        string UserName { get; }
    }
}

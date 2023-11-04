using API.BLL.Interfaces___Copy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text;

namespace API.Helpers
{
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timetoliveinseconds;

        public CashedAttribute(int timetoliveinseconds)
        {
            this.timetoliveinseconds = timetoliveinseconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cashService = context.HttpContext.RequestServices.GetRequiredService<IResponseCashService>();
            var cashkey = GenerateCashKeyFromRequest(context.HttpContext.Request);
            var cashedResponse = await cashService.GetCashResponse(cashkey);
            if (!string.IsNullOrEmpty(cashedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content=cashedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result= contentResult;
                return;
            }
            var executedContext=await next();
            if (executedContext.Result is OkObjectResult OkObjectResult)
                await cashService.CashResponseAsync(cashkey, ObjectResult, TimeSpan.FromSeconds(timetoliveinseconds));
        }
        private string GenerateCashKeyFromRequest(HttpRequest request)
        {
            var keybuilder=new StringBuilder();
            keybuilder.Append($"{request.Path}");
            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                keybuilder.Append($"|{key}-{value}");
            }
            return keybuilder.ToString();
        }
    }
}

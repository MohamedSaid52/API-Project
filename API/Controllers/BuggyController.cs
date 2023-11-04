using API.DAL.Data;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class BuggyController : BaseAPIController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }
        [HttpGet ("NotFound")]
        public IActionResult GetNotFound()
        {
            var anything = context.Products.Find(100);
            if(anything is null)
                return NotFound(new APIResponse(404));
            return Ok();
        }

        [HttpGet("Server Error")]
        public IActionResult GetServerError()
        {
            var anything = context.Products.Find(100);
            var something = anything.ToString();
            return Ok();
        }

        [HttpGet("BadRequest")]
        public IActionResult GetBadRequest()
        {
          return BadRequest(new APIResponse(400));  
        }
    }
}

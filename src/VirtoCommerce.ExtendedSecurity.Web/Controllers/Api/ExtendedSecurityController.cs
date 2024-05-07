using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.ExtendedSecurity.Core;

namespace VirtoCommerce.ExtendedSecurity.Web.Controllers.Api
{
    [Route("api/extended-security")]
    public class ExtendedSecurityController : Controller
    {
        // GET: api/extended-security
        /// <summary>
        /// Get message
        /// </summary>
        /// <remarks>Return "Hello world!" message</remarks>
        [HttpGet]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public ActionResult<string> Get()
        {
            return Ok(new { result = "Hello world!" });
        }
    }
}

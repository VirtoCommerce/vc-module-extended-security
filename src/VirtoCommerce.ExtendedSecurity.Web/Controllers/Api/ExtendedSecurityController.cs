using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Permissions = VirtoCommerce.ExtendedSecurity.Core.ModuleConstants.Security.Permissions;

namespace VirtoCommerce.ExtendedSecurity.Web.Controllers.Api;

[Authorize]
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
    [Authorize(Permissions.Read)]
    public ActionResult<string> Get()
    {
        return Ok(new { result = "Hello world!" });
    }
}

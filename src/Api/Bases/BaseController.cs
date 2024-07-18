using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Bases;

public class BaseController: ControllerBase
{
    public int CurrentUserId  => GetCurrentUserId();
    private int GetCurrentUserId()
    {
        var userId = -1;
        var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim != null)
        {
            int.TryParse(claim.Value, out userId);
        }
        return userId;
    }
}

using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;

namespace login_with_google_api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    List<AppUser> _users;

    public AuthController()
    {
        _users = new List<AppUser>();
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthRequest request)
    {
        try
        {
            // 1. Verify Google token (signature + issuer)
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

            // 2. Check if user exists in DB (pseudo-code)
            var _userService = new UserService();
            var user = _userService.GetOrCreateUser(payload.Email, payload.Name);

            // 3. Generate your own JWT (pseudo-code)
            var _jwtService = new JwtService();
            var token = _jwtService.GenerateToken(user);

            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }
}

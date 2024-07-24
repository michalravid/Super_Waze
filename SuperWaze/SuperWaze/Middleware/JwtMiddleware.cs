
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SuperWaze.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            AttachUserToContext(context, token);
        }

        await _next(context);
    }
    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("helloWorldMichalRAvidAndRutiMalkin"); // ודא שהמפתח כאן הוא אותו מפתח ששימש ליצירת ה־Token
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            // הצמד את ה־Claims המתאימים ל-context
            if (userName != null)
                context.Items["User"] = userName;
        }
        catch (Exception ex)
        {
            // אם האימות נכשל, אין להצמיד את המידע
            context.Response.StatusCode = 401;
            context.Response.WriteAsync($"Token validation failed: {ex.Message}");
        }
    }

}

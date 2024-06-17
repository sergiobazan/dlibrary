using Application.Abstractions.Behavior;
using Domain.Reader;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentications;

internal class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly IPermissionService _permissionService;

    public JwtProvider(IOptions<JwtOptions> jwtOptions, IPermissionService permissionService)
    {
        _jwtOptions = jwtOptions.Value;
        _permissionService = permissionService;
    }

    public async Task<string> GenerateAsync(Reader reader)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, reader.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, reader.Email!.Value)
        };

        HashSet<string> roles = await _permissionService.GetPermissionAsync(reader.Id);

        foreach (string role in roles)
        {
            claims.Add(new(CustomClaims.Permissions, role));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenResult;
    }
}

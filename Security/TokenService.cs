using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{
    private readonly byte[] _keyBytes;

    public TokenService(string key)
    {
        // Convert the key string to bytes (adjust key size as needed)
        _keyBytes = Encoding.ASCII.GetBytes(key);
    }
    
    public string GenerateToken(string userId, string username)
    {
        // Create a list of claims representing the user identity
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            // Add any additional claims as needed
        };

        // Create a symmetric security key
        var key = new SymmetricSecurityKey(_keyBytes);

        // Create signing credentials using the key and algorithm
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Set token expiration (adjust as needed)
        var tokenExpiry = DateTime.UtcNow.AddHours(1);

        // Create a JWT token
        var token = new JwtSecurityToken(
            issuer: "YourIssuer",
            audience: "YourAudience",
            claims: claims,
            expires: tokenExpiry,
            signingCredentials: credentials
        );

        // Serialize the token to a string
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}

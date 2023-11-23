using BidIt_Saitynai.Auth.Model;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace BidIt_Saitynai.Auth
{
    public static class AuthEndPoints
    {
        public static void AddAuthApi(this WebApplication app)
        {
            app.MapPost("api/register", async (UserManager<ForumRestUser> userManager, RegisterUserDto registerUserDto) =>
            {
                var user = await userManager.FindByNameAsync(registerUserDto.Username);
                if (user != null)
                {
                    return Results.UnprocessableEntity("username already taken");
                }
                var newUser = new ForumRestUser
                {
                    Email = registerUserDto.Email,
                    UserName = registerUserDto.Username,
                };
                var createUserResult = await userManager.CreateAsync(newUser, registerUserDto.Password);
                if (!createUserResult.Succeeded)
                {
                    return Results.UnprocessableEntity("Nepavyko");
                }
                await userManager.AddToRoleAsync(newUser, ForumRoles.ForumUser);

                return Results.Created("api/login", new AuthUserDto(newUser.Id, newUser.UserName, newUser.Email));
            });
            app.MapPost("api/login", async (UserManager<ForumRestUser> userManager, JwtTokenService jwtTokenService, LoginDto loginDto) =>
            {

                var user = await userManager.FindByNameAsync(loginDto.Username);
                if (user == null)
                {
                    return Results.UnprocessableEntity("username or password was incorrect");
                }
                var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!isPasswordValid)
                {
                    return Results.UnprocessableEntity("password was incorrect");
                }
                user.ForceRelogin = false;
                await userManager.UpdateAsync(user);
                var roles = await userManager.GetRolesAsync(user);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(user.Id);

                return Results.Ok(new SuccesfulLoginDto(accessToken, refreshToken));

            });
            app.MapPost("api/accessToken", async (UserManager<ForumRestUser> userManager, JwtTokenService jwtTokenService, RefreshAccessTokenDto refreshAccessTokenDto) =>
            {
                if (!jwtTokenService.TryParseRefreshToken(refreshAccessTokenDto.RefreshToken, out var claims))
                {
                    return Results.UnprocessableEntity();
                }

                var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Results.UnprocessableEntity("Invalid token");
                }
                if (user.ForceRelogin)
                {
                    return Results.UnprocessableEntity();
                }

                var roles = await userManager.GetRolesAsync(user);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(user.Id);

                return Results.Ok(new SuccesfulLoginDto(accessToken, refreshToken));
            });
        }
    }
    public record SuccesfulLoginDto(string AccessToken, string RefreshToken);
    public record AuthUserDto(string UserId, string UserName, string Email);
    public record RegisterUserDto(string Username, string Email, string Password);
    public record LoginDto(string Username, string Password);
    public record RefreshAccessTokenDto(string RefreshToken);
}

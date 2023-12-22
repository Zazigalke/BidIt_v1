using BidIt_Saitynai.Data.Entities;
using BidIt_Saitynai.Data;
using O9d.AspNet.FluentValidation;
using Microsoft.EntityFrameworkCore;
using static BidIt_Saitynai.Data.Dtos.UserDtos;
using BidIt_Saitynai.Helpers;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authorization;
using BidIt_Saitynai.Auth;

namespace BidIt_Saitynai.Endpoints
{
    public static class UserEndPoints
    {
        public static void AddUserAPI(RouteGroupBuilder usersGroup)
        {
            // /api/users/?pageNumber=1
            usersGroup.MapGet("users", async ([AsParameters] SearchParameters searchParams, BidDbContext dbContext, LinkGenerator linkGenerator, HttpContext httpContext) =>
            {
                var queryable = dbContext.Users.AsQueryable().OrderBy(o => o.Id);

                var pagedList = await PagedList<User>.CreateAsync(queryable, searchParams.PageNumber!.Value, searchParams.PageSize!.Value);

                var previousPageLink = pagedList.HasPrevious ? linkGenerator.GetUriByName(httpContext, "GetUsers", new { pageNumber = searchParams.PageNumber - 1, pageSize = searchParams.PageSize }) : null;

                var nextPageLink = pagedList.HasNext ? linkGenerator.GetUriByName(httpContext, "GetUsers", new { pageNumber = searchParams.PageNumber + 1, pageSize = searchParams.PageSize }) : null;


                var paginationMetadata = new PaginationMetadata(pagedList.TotalCount, pagedList.PageSize, pagedList.CurrentPage, pagedList.TotalPages, previousPageLink, nextPageLink);

                httpContext.Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

                return pagedList.Select(user => new UserDto(user.Id, user.FirstName, user.LastName, user.Email));

            }).WithName("GetUsers");
            usersGroup.MapGet("users/{id}", async (int id, BidDbContext dbContext) =>
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(o => o.Id == id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(new UserDto(user.Id, user.FirstName, user.LastName, user.Email));

            }).WithName("GetUser");

            usersGroup.MapPost("users",[Authorize(Roles = ForumRoles.Admin)] async ([Validate] CreateUserDto createUserDto, BidDbContext dbContext, HttpContext httpContext, LinkGenerator linkGenerator) =>
            {

                var user = new User()
                {
                    FirstName = createUserDto.firstName,
                    LastName = createUserDto.lastName,
                    Email = createUserDto.email,
                    UserId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                };
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();

                //var links = CreateLinks(user.Id, httpContext, linkGenerator);

                //var userDto = new UserDto(user.Id, user.FirstName, user.LastName, user.Email);

                //var resource = new ResourceDto<UserDto>(userDto, links.ToArray()); 

                return Results.Created($"/api/users/{user.Id}", new UserDto(user.Id, user.FirstName, user.LastName, user.Email));

            }).WithName("CreateUser");

            usersGroup.MapPut("users/{id}", [Authorize(Roles = ForumRoles.Admin)] async (int id, [Validate] UpdateUserDto dto, BidDbContext dbContext, HttpContext httpContext) =>
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(o => o.Id == id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                if(!httpContext.User.IsInRole(ForumRoles.Admin) && httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != user.UserId)
                {
                    return Results.Forbid();
                }
                user.FirstName = dto.firstName;
                user.LastName = dto.lastName;
                user.Email = dto.email;

                dbContext.Update(user);
                await dbContext.SaveChangesAsync();

                return Results.Ok(new UserDto(user.Id, user.FirstName, user.LastName, user.Email));

            }).WithName("UpdateUser");

            usersGroup.MapDelete("users/{id}", async (int id, BidDbContext dbContext) =>
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(o => o.Id == id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();

            }).WithName("RemoveUser");


        }

        /*static IEnumerable<LinkDto> CreateLinks(int userId, HttpContext httpContext, LinkGenerator linkGenerator)
        {
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "GetUser", new { userId }), "self", "GET");
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "UpdateUser", new { userId }), "edit", "PUT");
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "RemoveUser", new { userId }), "delete", "DELETE");
        }*/

    }
}

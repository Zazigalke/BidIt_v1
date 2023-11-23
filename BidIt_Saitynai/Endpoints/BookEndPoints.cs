using BidIt_Saitynai.Auth;
using BidIt_Saitynai.Data;
using BidIt_Saitynai.Data.Entities;
using BidIt_Saitynai.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using O9d.AspNet.FluentValidation;
using System.Security.Claims;
using System.Text.Json;
using static BidIt_Saitynai.Data.Dtos.BookDtos;

namespace BidIt_Saitynai.Endpoints
{
    public class BookEndPoints
    {
        public static void AddBookAPI(RouteGroupBuilder booksGroup)
        {
            // /api/books/?pageNumber=1
            booksGroup.MapGet("books", async ([AsParameters] SearchParameters searchParams, BidDbContext dbContext, LinkGenerator linkGenerator, HttpContext httpContext) =>
            {

                var queryable = dbContext.Books.Include(x => x.User).AsQueryable().OrderBy(o => o.Id);

                var pagedList = await PagedList<Book>.CreateAsync(queryable, searchParams.PageNumber!.Value, searchParams.PageSize!.Value);

                var previousPageLink = pagedList.HasPrevious ? linkGenerator.GetUriByName(httpContext, "GetBooks", new { pageNumber = searchParams.PageNumber - 1, pageSize = searchParams.PageSize }) : null;

                var nextPageLink = pagedList.HasNext ? linkGenerator.GetUriByName(httpContext, "GetBooks", new { pageNumber = searchParams.PageNumber + 1, pageSize = searchParams.PageSize }) : null;


                var paginationMetadata = new PaginationMetadata(pagedList.TotalCount, pagedList.PageSize, pagedList.CurrentPage, pagedList.TotalPages, previousPageLink, nextPageLink);

                httpContext.Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

                return pagedList.Select(book => new BookDto(book.Id, book.Title, book.Condition, book.PageCount, book.StartingPrice, book.User.Id));

            }).WithName("GetBooks");

            booksGroup.MapGet("books/{id}", async (int id, BidDbContext dbContext) =>
            {
                var book = await dbContext.Books.Include(x => x.User).FirstOrDefaultAsync(o => o.Id == id);
                if (book == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(new BookDto(book.Id, book.Title, book.Condition, book.PageCount, book.StartingPrice, book.User.Id));

            }).WithName("GetBook");

            booksGroup.MapPost("books", [Authorize(Roles = ForumRoles.ForumUser)] async ([Validate] CreateBookDto createBookDto, BidDbContext dbContext, HttpContext httpContext, LinkGenerator linkGenerator) =>
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(o => o.Id == createBookDto.user);
                if (user == null)
                {
                    return Results.NotFound();
                }
                var book = new Book()
                {
                    Title = createBookDto.title,
                    Condition = createBookDto.condition,
                    PageCount = createBookDto.pageCount,
                    StartingPrice = createBookDto.startingPrice,
                    User = user,
                    UserId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                };
                dbContext.Books.Add(book);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/api/books/{book.Id}", new BookDto(book.Id, book.Title, book.Condition, book.PageCount, book.StartingPrice, book.User.Id));

            }).WithName("CreateBook");

            booksGroup.MapPut("books/{id}", [Authorize(Roles = ForumRoles.ForumUser)] async (int id, [Validate] UpdateBookDto dto, BidDbContext dbContext, HttpContext httpContext) =>
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == dto.user);
                if (user == null)
                {
                    return Results.NotFound();
                }
                var book = await dbContext.Books.FirstOrDefaultAsync(o => o.Id == id);
                if (book == null)
                {
                    return Results.NotFound();
                }
                if (!httpContext.User.IsInRole(ForumRoles.ForumUser) && httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != book.UserId)
                {
                    return Results.Forbid();
                }
                book.Title = dto.title;
                book.Condition = dto.condition;
                book.PageCount = dto.pageCount;
                book.StartingPrice = dto.startingPrice;
                book.User = user;

                dbContext.Update(book);
                await dbContext.SaveChangesAsync();

                return Results.Ok(new BookDto(book.Id, book.Title, book.Condition, book.PageCount, book.StartingPrice, book.User.Id));

            }).WithName("UpdateBook");

            booksGroup.MapDelete("books/{id}", async (int id, BidDbContext dbContext) =>
            {
                var book = await dbContext.Books.FirstOrDefaultAsync(o => o.Id == id);
                if (book == null)
                {
                    return Results.NotFound();
                }
                dbContext.Remove(book);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();

            }).WithName("RemoveBook");


        }
    }
}

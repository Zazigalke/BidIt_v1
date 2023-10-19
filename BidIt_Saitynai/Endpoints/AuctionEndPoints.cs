using BidIt_Saitynai.Data;
using BidIt_Saitynai.Data.Entities;
using BidIt_Saitynai.Helpers;
using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using System.Text.Json;
using static BidIt_Saitynai.Data.Dtos.AuctionDtos;

namespace BidIt_Saitynai.Endpoints
{
    public class AuctionEndPoints
    {
        public static void AddAuctionAPI(RouteGroupBuilder auctionsGroup)
        {
            // /api/books/?pageNumber=1
            auctionsGroup.MapGet("auctions", async ([AsParameters] SearchParameters searchParams, BidDbContext dbContext, LinkGenerator linkGenerator, HttpContext httpContext) =>
            {

                var queryable = dbContext.Auctions.Include(x => x.Company).AsQueryable().OrderBy(o => o.Id);

                var pagedList = await PagedList<Auction>.CreateAsync(queryable, searchParams.PageNumber!.Value, searchParams.PageSize!.Value);

                var previousPageLink = pagedList.HasPrevious ? linkGenerator.GetUriByName(httpContext, "GetBooks", new { pageNumber = searchParams.PageNumber - 1, pageSize = searchParams.PageSize }) : null;

                var nextPageLink = pagedList.HasNext ? linkGenerator.GetUriByName(httpContext, "GetBooks", new { pageNumber = searchParams.PageNumber + 1, pageSize = searchParams.PageSize }) : null;


                var paginationMetadata = new PaginationMetadata(pagedList.TotalCount, pagedList.PageSize, pagedList.CurrentPage, pagedList.TotalPages, previousPageLink, nextPageLink);

                httpContext.Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

                return pagedList.Select(auction => new AuctionDto(auction.Id, auction.Name, auction.City, auction.CreationDate, auction.StartingTime, auction.EndingTime, auction.Company.Name));
            }).WithName("GetAuctions");

            auctionsGroup.MapGet("auctions/{id}", async (int id, BidDbContext dbContext) =>
            {
                var auction = await dbContext.Auctions.Include(x => x.Company).FirstOrDefaultAsync(o => o.Id == id);
                if (auction == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(new AuctionDto(auction.Id, auction.Name, auction.City, auction.CreationDate, auction.StartingTime, auction.EndingTime, auction.Company.Name));

            }).WithName("GetAuction");

            auctionsGroup.MapPost("auctions", async ([Validate] CreateAuctionDto createAuctionDto, BidDbContext dbContext, HttpContext httpContext, LinkGenerator linkGenerator) =>
            {
                var company = await dbContext.Companies.FirstOrDefaultAsync(o => o.Id.Equals(createAuctionDto.company));
                if (company == null)
                {
                    return Results.NotFound();
                }
                var auction = new Auction()
                {
                    Name = createAuctionDto.name,
                    City = createAuctionDto.city,
                    CreationDate = DateTime.Now,
                    StartingTime = DateTime.Now,
                    EndingTime = DateTime.Now,
                    Company = company
                };
                dbContext.Auctions.Add(auction);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/api/auction/{auction.Id}", new AuctionDto(auction.Id, auction.Name, auction.City, auction.CreationDate, auction.StartingTime, auction.EndingTime, auction.Company.Name));

            }).WithName("CreateAuction");

            auctionsGroup.MapPut("auctions/{id}", async (int id, [Validate] UpdateAuctionDto dto, BidDbContext dbContext) =>
            {
                var company = await dbContext.Companies.FirstOrDefaultAsync(x => x.Id.Equals(dto.company));
                if (company == null)
                {
                    return Results.NotFound();
                }
                var auction = await dbContext.Auctions.FirstOrDefaultAsync(o => o.Id == id);
                if (auction == null)
                {
                    return Results.NotFound();
                }
                auction.Name = dto.name;
                auction.City = dto.city;
                auction.CreationDate = dto.creationDate;
                auction.StartingTime = dto.startingTime;
                auction.EndingTime = dto.endingTime;
                auction.Company = company;

                dbContext.Update(auction);
                await dbContext.SaveChangesAsync();

                return Results.Ok(new AuctionDto(auction.Id, auction.Name, auction.City, auction.CreationDate, auction.StartingTime, auction.EndingTime, auction.Company.Name));

            }).WithName("UpdateAuction");

            auctionsGroup.MapDelete("auctions/{id}", async (int id, BidDbContext dbContext) =>
            {
                var auction = await dbContext.Auctions.FirstOrDefaultAsync(o => o.Id == id);
                if (auction == null)
                {
                    return Results.NotFound();
                }
                dbContext.Remove(auction);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();

            }).WithName("RemoveAuction");


        }
    }
}

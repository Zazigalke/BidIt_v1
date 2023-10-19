using BidIt_Saitynai.Data;
using BidIt_Saitynai.Data.Entities;
using BidIt_Saitynai.Helpers;
using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using System;
using System.Text.Json;
using static BidIt_Saitynai.Data.Dtos.CompanyDtos;
using static BidIt_Saitynai.Data.Dtos.OfferDtos;

namespace BidIt_Saitynai.Endpoints
{
    public class OfferEndPoints
    {
        public static void AddOfferAPI(RouteGroupBuilder offersGroup)
        {
            // /api/books/?pageNumber=1
            offersGroup.MapGet("offers", async ([AsParameters] SearchParameters searchParams, BidDbContext dbContext, LinkGenerator linkGenerator, HttpContext httpContext) =>
            {

                var queryable = dbContext.Offers.Include(x => x.Book).Include(x => x.Auction).AsQueryable().OrderBy(o => o.Id);

                var pagedList = await PagedList<Offer>.CreateAsync(queryable, searchParams.PageNumber!.Value, searchParams.PageSize!.Value);

                var previousPageLink = pagedList.HasPrevious ? linkGenerator.GetUriByName(httpContext, "GetBooks", new { pageNumber = searchParams.PageNumber - 1, pageSize = searchParams.PageSize }) : null;

                var nextPageLink = pagedList.HasNext ? linkGenerator.GetUriByName(httpContext, "GetBooks", new { pageNumber = searchParams.PageNumber + 1, pageSize = searchParams.PageSize }) : null;


                var paginationMetadata = new PaginationMetadata(pagedList.TotalCount, pagedList.PageSize, pagedList.CurrentPage, pagedList.TotalPages, previousPageLink, nextPageLink);

                httpContext.Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

                return pagedList.Select(offer => new OfferDto(offer.Id, offer.Price, offer.CreationDate, offer.Book.Title, offer.Auction.Name));
            }).WithName("GetOffers");

            offersGroup.MapGet("offers/{id}", async (int id, BidDbContext dbContext) =>
            {
                var offer = await dbContext.Offers.Include(x => x.Book).Include(x => x.Auction).FirstOrDefaultAsync(o => o.Id == id);
                if (offer == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(new OfferDto(offer.Id, offer.Price, offer.CreationDate, offer.Book.Title, offer.Auction.Name));
            }).WithName("GetOffer");

            offersGroup.MapPost("offers", async ([Validate] CreateOfferDto createOfferDto, BidDbContext dbContext, HttpContext httpContext, LinkGenerator linkGenerator) =>
            {
                var book = await dbContext.Books.FirstOrDefaultAsync(o => o.Id.Equals(createOfferDto.book));
                if (book == null)
                {
                    return Results.NotFound();
                }
                var auction = await dbContext.Auctions.FirstOrDefaultAsync(o => o.Id.Equals(createOfferDto.auction));
                if (auction == null)
                {
                    return Results.NotFound();
                }
                var offer = new Offer()
                {
                    Price = createOfferDto.price,
                    CreationDate = DateTime.Now,
                    Book = book,
                    Auction = auction,
                };

                dbContext.Offers.Add(offer);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/api/offer/{offer.Id}", new OfferDto(offer.Id, offer.Price, offer.CreationDate, offer.Book.Title, offer.Auction.Name));

            }).WithName("CreateOffer");

            offersGroup.MapPut("offers/{id}", async (int id, [Validate] UpdateOfferDto dto, BidDbContext dbContext) =>
            {
                var book = await dbContext.Books.FirstOrDefaultAsync(x => x.Id.Equals(dto.book));
                if (book == null)
                {
                    return Results.NotFound();
                }

                var auction = await dbContext.Auctions.FirstOrDefaultAsync(o => o.Id.Equals(dto.auction));
                if (auction == null)
                {
                    return Results.NotFound();
                }
                var offer = await dbContext.Offers.FirstOrDefaultAsync(o => o.Id == id);
                if (offer == null)
                {
                    return Results.NotFound();
                }
                offer.Price = dto.price;
                offer.CreationDate = dto.creationDate;
                offer.Book = book;
                offer.Auction = auction;

                dbContext.Update(auction);
                await dbContext.SaveChangesAsync();

                return Results.Ok(new OfferDto(offer.Id, offer.Price, offer.CreationDate, offer.Book.Title, offer.Auction.Name));

            }).WithName("UpdateOffer");

            offersGroup.MapDelete("offers/{id}", async (int id, BidDbContext dbContext) =>
            {
                var offer = await dbContext.Offers.FirstOrDefaultAsync(o => o.Id == id);
                if (offer == null)
                {
                    return Results.NotFound();
                }
                dbContext.Remove(offer);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();

            }).WithName("RemoveOffer");

            offersGroup.MapGet("companies/{id}/auctions/{aucId}/offers", async (int id, int aucId, BidDbContext dbContext) =>
            {
               
                
                var offer = await dbContext.Offers.Include(x => x.Book).Include(x => x.Auction).ThenInclude(x => x.Company).Where(o => o.Auction.Id == aucId && id == o.Auction.Company.Id).ToListAsync();
                if (offer == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(offer.Select(offer => new OfferDto(offer.Id, offer.Price, offer.CreationDate, offer.Book.Title, offer.Auction.Name)));
                

            }).WithName("GetHierarchyOffer");
        }
    }
}

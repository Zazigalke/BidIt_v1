using BidIt_Saitynai.Data;
using BidIt_Saitynai.Data.Entities;
using BidIt_Saitynai.Helpers;
using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using System.Text.Json;
using static BidIt_Saitynai.Data.Dtos.CompanyDtos;

namespace BidIt_Saitynai.Endpoints
{
    public class CompanyEndPoints
    {
        public static void AddCompanyAPI(RouteGroupBuilder companiesGroup)
        {
            // /api/companies/?pageNumber=1
            companiesGroup.MapGet("companies", async ([AsParameters] SearchParameters searchParams, BidDbContext dbContext, LinkGenerator linkGenerator, HttpContext httpContext) =>
            {
                var queryable = dbContext.Companies.AsQueryable().OrderBy(o => o.Id);

                var pagedList = await PagedList<Company>.CreateAsync(queryable, searchParams.PageNumber!.Value, searchParams.PageSize!.Value);

                var previousPageLink = pagedList.HasPrevious ? linkGenerator.GetUriByName(httpContext, "GetCompanies", new { pageNumber = searchParams.PageNumber - 1, pageSize = searchParams.PageSize }) : null;

                var nextPageLink = pagedList.HasNext ? linkGenerator.GetUriByName(httpContext, "GetCompanies", new { pageNumber = searchParams.PageNumber + 1, pageSize = searchParams.PageSize }) : null;


                var paginationMetadata = new PaginationMetadata(pagedList.TotalCount, pagedList.PageSize, pagedList.CurrentPage, pagedList.TotalPages, previousPageLink, nextPageLink);

                httpContext.Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

                return pagedList.Select(company => new CompanyDto(company.Id, company.Name));

            }).WithName("GetCompanies");

            companiesGroup.MapGet("companies/{id}", async (int id, BidDbContext dbContext) =>
            {
                var company = await dbContext.Companies.FirstOrDefaultAsync(o => o.Id == id);
                if (company == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(new CompanyDto(company.Id, company.Name));

            }).WithName("GetCompany");

            companiesGroup.MapPost("companies", async ([Validate] CreateCompanyDto createCompanyDto, BidDbContext dbContext, HttpContext httpContext, LinkGenerator linkGenerator) =>
            {
                var company = new Company()
                {
                    Name = createCompanyDto.name,

                };
                dbContext.Companies.Add(company);
                await dbContext.SaveChangesAsync();
                return Results.Created($"/api/companies/{company.Id}", new CompanyDto(company.Id, company.Name));

            }).WithName("CreateCompany");

            companiesGroup.MapPut("companies/{id}", async (int id, [Validate] UpdateCompanyDto dto, BidDbContext dbContext) =>
            {
                var company = await dbContext.Companies.FirstOrDefaultAsync(o => o.Id == id);
                if (company == null)
                {
                    return Results.NotFound();
                }
                company.Name = dto.name;
                dbContext.Update(company);
                await dbContext.SaveChangesAsync();

                return Results.Ok(new CompanyDto(company.Id, company.Name));

            }).WithName("UpdateCompany");

            companiesGroup.MapDelete("companies/{id}", async (int id, BidDbContext dbContext) =>
            {
                var company = await dbContext.Companies.FirstOrDefaultAsync(o => o.Id == id);
                if (company == null)
                {
                    return Results.NotFound();
                }
                dbContext.Remove(company);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();

            }).WithName("RemoveCompany");

            

        }
    }
}

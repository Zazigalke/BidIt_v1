using BidIt_Saitynai.Data;
using BidIt_Saitynai.Data.Entities;
using BidIt_Saitynai.Endpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using System.Security.Cryptography.X509Certificates;
using static BidIt_Saitynai.Data.Entities.User;

namespace BidIt_Saitynai
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddDbContext<BidDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("BidIt")!));

            builder.Services.AddRazorPages();
            var app = builder.Build();

            var usersGroup = app.MapGroup("/api").WithValidationFilter().RequireValidation();
            var companiesGroup = app.MapGroup("/api").WithValidationFilter().RequireValidation();
            var booksGroup = app.MapGroup("/api").WithValidationFilter().RequireValidation();
            var auctionsGroup = app.MapGroup("/api").WithValidationFilter().RequireValidation();
            var offersGroup = app.MapGroup("/api").WithValidationFilter().RequireValidation();
            UserEndPoints.AddUserAPI(usersGroup);
            CompanyEndPoints.AddCompanyAPI(companiesGroup);
            BookEndPoints.AddBookAPI(booksGroup);
            AuctionEndPoints.AddAuctionAPI(auctionsGroup);
            OfferEndPoints.AddOfferAPI(offersGroup);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();



        }


    }
}
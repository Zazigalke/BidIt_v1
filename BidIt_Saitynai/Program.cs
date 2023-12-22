using BidIt_Saitynai.Auth.Model;
using BidIt_Saitynai.Data;
using BidIt_Saitynai.Data.Entities;
using BidIt_Saitynai.Endpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using O9d.AspNet.FluentValidation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static BidIt_Saitynai.Data.Entities.User;
using System.IdentityModel.Tokens.Jwt;
using BidIt_Saitynai.Auth;
using Org.BouncyCastle.Pkix;
using Microsoft.AspNetCore.Builder;

namespace BidIt_Saitynai
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddDbContext<BidDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("BidIt")!));

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddTransient<JwtTokenService>();
            builder.Services.AddScoped<AuthDbSeeder>();

            builder.Services.AddIdentity<ForumRestUser, IdentityRole>().AddEntityFrameworkStores<BidDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options => {

                options.TokenValidationParameters.ValidAudience = builder.Configuration["Jwt:ValidAudience"];
                options.TokenValidationParameters.ValidIssuer = builder.Configuration["Jwt:ValidIssuer"];
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));

            });
            builder.Services.AddAuthorization();
            builder.Services.AddRazorPages();
			var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

			// Add services to the container.
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("myAppCors", policy =>
				{
					policy.WithOrigins(allowedOrigin)
							.AllowAnyHeader()
							.AllowAnyMethod();
				});
			});

			var app = builder.Build();
			app.UseCors("myAppCors");
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

            app.AddAuthApi();
            app.MapRazorPages();
            app.UseAuthentication();
            app.UseAuthorization();
            using var scope2 = app.Services.CreateScope();
            var context = scope2.ServiceProvider.GetRequiredService<BidDbContext>();

            context.Database.EnsureCreated();
            using var scope = app.Services.CreateScope();
            var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthDbSeeder>();

			
			await dbSeeder.SeedAsync();
            app.Run();

        }


    }
}
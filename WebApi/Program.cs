
using System.Text;
using Application.Interfaces;
using Final.database;
using Final.Entities;
using Final.Interfaces;
using Final.Repositories;
using Final.Services;
using HotelProject.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DatabaseContext>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddHostedService<PostStatusChecker>();
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
            builder.Services.AddIdentity<User, IdentityRole>()
             .AddEntityFrameworkStores<DatabaseContext>()
             .AddDefaultTokenProviders();

            ConfigureRoles(builder.Services.BuildServiceProvider().GetService<RoleManager<IdentityRole>>());

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrator"));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigureRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole("Administrator");
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole("User");
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}



using AutoMapper;
using System.Text;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using OnlineMovieTicketBookingSystem.Services;
using OnlineMovieTicketBookingSystem;
using OnlineMovieTicketBookingSystem.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDefaultCS"));
});

builder.Services.AddScoped<IGenericRepository<Movie>, GenericRepository<Movie>>();
builder.Services.AddScoped<IGenericRepository<Theatre>, GenericRepository<Theatre>>();
builder.Services.AddScoped<IGenericRepository<Show>, GenericRepository<Show>>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<Ticket>, TicketRepository>(); // Use TicketRepository for Ticket
builder.Services.AddScoped<IGenericRepository<PaymentDetail>, GenericRepository<PaymentDetail>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddSingleton<EmailService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add other services and repositories as needed
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"]))
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        Description = "Jwt Authentication Header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Name = "Bearer",
                Scheme = "oauth2"
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

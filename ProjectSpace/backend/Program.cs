using dotNETify.Data;
using dotNETify.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using dotNETify;
using Swashbuckle.AspNetCore.SwaggerUI;
using dotNETify.Models;
using dotNETIFY.Persistance;
using dotNETify.Services;
using dotNETIFY.Persistance;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Rejestracja AppDbContext z PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipal>();


builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<UsersRepository, UsersRepository>();
builder.Services.AddScoped<FriendshipRepository, FriendshipRepository>();
builder.Services.AddScoped<ISongsRepository, SongsRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<ILikedSongsRepository, LikedSongsRepository>();
builder.Services.AddScoped<ILikedArtistsRepository, LikedArtistsRepository>();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapIdentityApi<User>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
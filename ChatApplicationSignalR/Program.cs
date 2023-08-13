using ChatApplicationSignalR;
using ChatApplicationSignalR.Factories;
using ChatApplicationSignalR.Managers;
using ChatApplicationSignalR.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChatApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ChatApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<DbContextFactory>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<RoleManager>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.MapHub<ChatHub>("/chathub");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager>();
    string[] roles = { "Admin", "User" };

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager>();
    await userManager.AddUserAsync("Admin", "Admin123!" );
    await userManager.AssignRoleToUserAsync("Admin", "Admin");

    await userManager.AddUserAsync("Bob", "Bob123!");
    await userManager.AssignRoleToUserAsync("Bob", "User");

    if (await roleManager.CreateRoles(roles) is { Succeeded: true} result) { }
}

app.Run();

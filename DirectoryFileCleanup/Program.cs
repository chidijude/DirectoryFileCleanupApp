global using DirectoryFileCleanup.Shared.Data;
global using Microsoft.EntityFrameworkCore;
using DirectoryFileCleanup.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.RegisterServices(builder);
 
var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

app.MapDirectoryEndpoints();
app.Run();
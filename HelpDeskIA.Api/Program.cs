using HelpDeskIA.Api.Models;
using HelpDeskIA.Api.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<HelpDeskIaContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IAsistenteTickets, AsistenteReglas>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.MapOpenApi();
}
{   
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

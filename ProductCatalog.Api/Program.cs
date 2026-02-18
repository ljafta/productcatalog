using Microsoft.AspNetCore.Http.HttpResults;
using ProductCatalog.Api.Domain.Entities;
using ProductCatalog.Api.Middleware;
using ProductCatalog.Api.Repositories.InMemory;
using ProductCatalog.Api.Repositories.Interfaces;
using ProductCatalog.Api.Search;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngular",
            policy =>
            {
                policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//“Whenever someone asks for IRepository<Product>, give them InMemoryProductRepository”
//“Create one ProductSearchEngine and reuse it”
builder.Services.AddSingleton<IRepository<Product>, InMemoryProductRepository>();
builder.Services.AddSingleton<ProductSearchEngine>();
builder.Services.AddSingleton<InMemoryCategoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular"); // ? ADD THIS LINE
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ProductCatalog.Api.Middleware.RequestTimingMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();

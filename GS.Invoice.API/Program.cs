using FluentValidation;
using FluentValidation.AspNetCore;
using GS.Invoice.Application.Interfaces;
using GS.Invoice.Application.Services;
using GS.Invoice.Domain.Interfaces;
using GS.Invoice.Domain.ModelValidators;
using GS.Invoice.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(InvoiceDetailValidator).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddSingleton<IInvoiceRepository>(DependencyResolver.InitializeInvoiceRepositoryInstanceAsync(builder.Configuration.GetSection("CosmosDB")).GetAwaiter().GetResult());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

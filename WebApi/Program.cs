using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Repositories.EF_Core;
using Services.Contracts;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
//nlog tanýmý yapmýþ olduk.
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
// Accept ifadesinde XML çýktýsý vermek için kullanýlýr.
// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true; // Ýçerik pazarlýðýna açýk hale getirildi.
    config.ReturnHttpNotAcceptable = true; // Ýstek geldiðinde kabul eder.
})
.AddCustomCsvFormatter() //Csv formatýnda çýktý verir.
.AddXmlDataContractSerializerFormatters() // Ýstenildiðinde XML çýktýsý verir.
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly) //JSON kaydý yapmýþ oluruz.
.AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Ýstemci hatasýnýn tipini belirtmek için kullanýlýr.

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service Extensions kullanmak için ConfigureSqlContext tanýmý yaparýz.
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();
builder.Services.ConfigureDataShaper();

var app = builder.Build();

//UseExceptionHandler yapýlandýrma
var logger=app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(app.Environment.IsProduction())
{ app.UseHsts(); }

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

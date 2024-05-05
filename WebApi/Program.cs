using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Repositories.EF_Core;
using Services.Contracts;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
//nlog tan�m� yapm�� olduk.
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
// Accept ifadesinde XML ��kt�s� vermek i�in kullan�l�r.
// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true; // ��erik pazarl���na a��k hale getirildi.
    config.ReturnHttpNotAcceptable = true; // �stek geldi�inde kabul eder.
})
.AddCustomCsvFormatter() //Csv format�nda ��kt� verir.
.AddXmlDataContractSerializerFormatters() // �stenildi�inde XML ��kt�s� verir.
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly) //JSON kayd� yapm�� oluruz.
.AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// �stemci hatas�n�n tipini belirtmek i�in kullan�l�r.

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service Extensions kullanmak i�in ConfigureSqlContext tan�m� yapar�z.
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();
builder.Services.ConfigureDataShaper();

var app = builder.Build();

//UseExceptionHandler yap�land�rma
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

using System.Reflection;
using BrokerHub.API.Configuration;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Infrastructure.Persistence.Data;
using BrokerHub.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BrokerHub API",
        Version = "v1",
        Description = "API para corretoras de imóveis: Cadastre e altere imóveis, consulte e agende visitas.",
        Contact = new OpenApiContact()
        {
            Name = "Mateus Mourão",
            Email = "mateusvmourao@gmail.com",
            Url = new Uri("https://mathieux-dev.github.io/")
        }
    });
    var arquivoSwaggerXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var diretorioArquivoXML = Path.Combine(AppContext.BaseDirectory, arquivoSwaggerXML);
    c.IncludeXmlComments(diretorioArquivoXML);
});

builder.Services.AddDbContext<BrokerHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IImovelRepository, ImovelRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

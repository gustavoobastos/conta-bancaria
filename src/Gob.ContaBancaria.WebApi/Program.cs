using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.OperacoesBancarias;
using Gob.ContaBancaria.Domain.Options;
using Gob.ContaBancaria.Domain.Services;
using Gob.ContaBancaria.Infra.Data.Contexts;
using Gob.ContaBancaria.Infra.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;
string connectionString = configuration.GetConnectionString("DefaultConnection");

services.AddDbContext<ContaBancariaDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
services.AddHealthChecks().AddSqlServer(connectionString);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

services.Configure<TaxasOperacionaisOptions>(configuration.GetSection(TaxasOperacionaisOptions.TaxasOperacionais));
services.AddScoped<IPessoaRepository, PessoaRepository>();
services.AddScoped<IContaRepository, ContaRepository>();
services.AddScoped<ILancamentoRepository, LancamentoRepository>();
services.AddScoped<IOperacaoFactory, OperacaoFactory>();
services.AddScoped<IContaBancariaService, ContaBancariaService>();
services.AddScoped<IOperacaoBancariaService, OperacaoBancariaService>();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("health");

app.Run();

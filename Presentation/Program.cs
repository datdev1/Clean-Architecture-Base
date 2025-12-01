using Application.Interface.Service;
using Application.Mapping;
using Application.Service;
using AutoMapper;
using Infrastructure.Context;
using Infrastructure.Interface.UnitOfWork;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TGSContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TGSConnectionStrings"),
        b => b.MigrationsAssembly(typeof(TGSContext).Assembly.FullName)
        );

});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

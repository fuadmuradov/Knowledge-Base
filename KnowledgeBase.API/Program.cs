using FluentValidation.AspNetCore;
using KnowledgeBase.Data.DbAccess;
using KnowledgeBase.Service.DTOs.AuthorDTOs;
using Microsoft.EntityFrameworkCore;
using KnowledgeBase.Service.Profiles;
using KnowledgeBase.Core;
using KnowledgeBase.Data;
using KnowledgeBase.Service.IServices;
using KnowledgeBase.Service.Implementations;
using KnowledgeBase.API.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AuthorPostDto>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//NewtonSoft loop Dedector
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

//Oracle Database Connection
builder.Services.AddDbContext<KnowledgeDbContext>(options =>
options.UseOracle(builder.Configuration.GetConnectionString("Default")));

// AutoMapper
builder.Services.AddAutoMapper(opt =>
opt.AddProfile(new MapProfile())
);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379"; // redis is the container name of the redis service. 6379 is the default port
    options.InstanceName = "SampleInstance";
});
//Service Configure
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPublicationService, PublicationService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IOrganizationService, OrganizatoinService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IPublicationTagService, PublicationTagService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

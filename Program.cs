using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Application.Services;
using InvestmentGameAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabaný baðlantýsý için InvestmentDbContext'i ekle
builder.Services.AddDbContext<InvestmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Application Katmanýndaki Servisler
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserCompanyOwnershipService, UserCompanyOwnershipService>();
builder.Services.AddScoped<IInGameItemService, InGameItemService>();

// Build the app
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

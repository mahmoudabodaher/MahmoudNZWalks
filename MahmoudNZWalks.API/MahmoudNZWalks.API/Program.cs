using MahmoudNZWalks.API.Data;
using MahmoudNZWalks.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject the DBContext
builder.Services.AddDbContext<MahmoudNZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MahmoudNZWalks"));
});

// Inject every single repository
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();

// Inject the Auto mapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);




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

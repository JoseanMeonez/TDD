using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence;
using RoomBookingApp.Persistence.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
connection.Open();

builder.Services.AddDbContext<RoomBookingAppDbContext>(opt => opt.UseSqlite(connection));

// Ensure Database created
DbContextOptionsBuilder<RoomBookingAppDbContext> dbBuilder = new();
dbBuilder.UseSqlite(connection);

using RoomBookingAppDbContext context = new(dbBuilder.Options);
context.Database.EnsureCreated();

// Inject application services
builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();
builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();

WebApplication app = builder.Build();

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

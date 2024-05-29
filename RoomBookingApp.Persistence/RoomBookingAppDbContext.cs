using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence;

public class RoomBookingAppDbContext : DbContext
{
	public RoomBookingAppDbContext(DbContextOptions<RoomBookingAppDbContext> options) : base(options)
	{

	}

	public DbSet<Room> Rooms { get; set; }
	public DbSet<RoomBooking> RoomBookings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Room>().HasData(
			new Room { Id = 1, Name = "Conference room A" },
			new Room { Id = 2, Name = "Conference room B" },
			new Room { Id = 3, Name = "Conference room C" }
		);
	}
}

using Microsoft.EntityFrameworkCore;
using villaApi.Models;

namespace villaApi.Services
{
	public class VillaDBContext : DbContext
	{
		public VillaDBContext(DbContextOptions options) : base(options){}
		public DbSet<Villa> Villas { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Villa>().HasData(
				new Villa()
				{
					Id = 1,
					Name = "Pool view",
					Details = "lorem lorem lorem lorem lorem lorem lorem lorem lorem ",
					ImageUrl = "No pic available",
					Occupancy = 5,
					Rate = 300, 
					Sqft = 550,
					Amenity ="", 	
				},
				new Villa()
				{
					Id = 2,
					Name = "Mountain view",
					Details = "lorem lorem lorem lorem lorem lorem lorem lorem lorem ",
					ImageUrl = "No pic available",
					Occupancy = 5,
					Rate = 300, 
					Sqft = 550,
					Amenity ="", 	
				},
				new Villa()
				{
					Id = 3,
					Name = "Best view",
					Details = "lorem lorem lorem lorem lorem lorem lorem lorem lorem ",
					ImageUrl = "No pic available",
					Occupancy = 5,
					Rate = 300, 
					Sqft = 550,
					Amenity ="", 	
				},
				new Villa()
				{
					Id = 4,
					Name = "Beach view",
					Details = "lorem lorem lorem lorem lorem lorem lorem lorem lorem ",
					ImageUrl = "No pic available",
					Occupancy = 5,
					Rate = 300, 
					Sqft = 550,
					Amenity ="", 	
				}
			);
		}
	}
}
using BearingStock.Domain.Entityes;
using Microsoft.EntityFrameworkCore;

namespace BearingStock.Core.DBContext
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Bearing> Bearings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BearingStock.Core.DBContext
{
	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			//Configured for migrations with:
			//dotnet ef migrations add InitialCreate --project BearingStock.Core --startup-project BearingStock.Api
			//dotnet ef database update --project BearingStock.Core --startup-project BearingStock.Api

			var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../BearingStock.Api");

			Console.WriteLine($"Using base path for configuration: {basePath}");
			var config = new ConfigurationBuilder()
				.SetBasePath(basePath) // Set base path
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			var connectionString = config.GetConnectionString("DbConnection");

			if (string.IsNullOrEmpty(connectionString))
			{
				throw new InvalidOperationException("Connection string 'DbConnection' is missing in appsettings.json");
			}

			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new AppDbContext(optionsBuilder.Options);
		}
	}
}

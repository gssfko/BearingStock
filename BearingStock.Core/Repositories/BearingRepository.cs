using BearingStock.Core.DBContext;
using BearingStock.Core.Repositories.Interfaces;
using BearingStock.Domain.Entityes;
using Microsoft.EntityFrameworkCore;

namespace BearingStock.Core.Repositories
{
	public class BearingRepository : GenericRepository<Bearing>, IBearingRepository
	{
		public BearingRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<IEnumerable<Bearing>> GetBearingAsync() => await _context.Bearings.ToListAsync();
	}
}

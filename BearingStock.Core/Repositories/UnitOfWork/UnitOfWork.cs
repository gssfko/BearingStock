using BearingStock.Core.DBContext;
using BearingStock.Core.Repositories.Interfaces;

namespace BearingStock.Core.Repositories.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;
		public IBearingRepository Bearings { get; }

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
			Bearings = new BearingRepository(_context);
		}

		public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
		public void Dispose() => _context.Dispose();
	}
}

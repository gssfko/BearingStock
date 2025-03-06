using BearingStock.Domain.Entityes;

namespace BearingStock.Core.Repositories.Interfaces
{
	public interface IBearingRepository : IGenericRepository<Bearing>
	{
		public Task<IEnumerable<Bearing>> GetBearingAsync();
	}
}

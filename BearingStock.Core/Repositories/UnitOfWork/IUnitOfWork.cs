using BearingStock.Core.Repositories.Interfaces;

namespace BearingStock.Core.Repositories.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IBearingRepository Bearings { get; }
		Task<int> SaveAsync();
	}
}

using BearingStock.Core.Repositories.UnitOfWork;
using BearingStock.Domain.Entityes;

namespace BearingStock.Core.Services
{
	public class BearingService
	{
		private readonly IUnitOfWork _unitOfWork;
		public BearingService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<Bearing>> GetAllBearingsAsync()
		{
			return (await _unitOfWork.Bearings.GetAllAsync()).ToList();
		}

		public async Task<Bearing?> GetBearingByIdAsync(int id)
		{
			return await _unitOfWork.Bearings.GetByIdAsync(id);
		}

		public async Task AddBearingAsync(Bearing bearing)
		{
			await _unitOfWork.Bearings.AddAsync(bearing);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateBearingAsync(Bearing bearing)
		{
			_unitOfWork.Bearings.Update(bearing);
			await _unitOfWork.SaveAsync();
		}

		public async Task DeleteBearingAsync(int id)
		{
			var bearing = await _unitOfWork.Bearings.GetByIdAsync(id);
			if (bearing != null)
			{
				_unitOfWork.Bearings.Delete(bearing);
				await _unitOfWork.SaveAsync();
			}
		}
	}
}

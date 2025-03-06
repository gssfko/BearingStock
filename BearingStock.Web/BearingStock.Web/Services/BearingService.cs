using BearingStock.Web.Models;

namespace BearingStock.Web.Services
{
    public class BearingService
    {
		private readonly HttpClient _httpClient;

		public BearingService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<BearingMinimal>> GetBearingsAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<BearingMinimal>>("api/bearings") ?? new List<BearingMinimal>();
		}

		public async Task<BearingMinimal?> GetBearingByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<BearingMinimal>($"api/bearings/{id}");
		}

		public async Task<bool> CreateBearingAsync(BearingMinimal bearing)
		{
			var response = await _httpClient.PostAsJsonAsync("api/bearings", bearing);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateBearingAsync(BearingMinimal bearing)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/bearings/{bearing.Id}", bearing);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteBearingAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/bearings/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}

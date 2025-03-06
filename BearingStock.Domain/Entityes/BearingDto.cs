using BearingStock.Domain.Enums;

namespace BearingStock.Domain.Entityes
{
	public class BearingDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Type { get; set; }
		public string? Manufacturer { get; set; }
		public decimal? Size { get; set; }
		public BearingSizeType? SizeType { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}

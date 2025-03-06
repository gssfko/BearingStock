using BearingStock.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BearingStock.Web.Models
{
	public class BearingMinimal
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? Name { get; set; }

		[Required]
		public string? Type { get; set; }

		[Required]
		public string? Manufacturer { get; set; }

		[Required]
		public decimal? Size { get; set; }

		[Required]
		public BearingSizeType? SizeType { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}

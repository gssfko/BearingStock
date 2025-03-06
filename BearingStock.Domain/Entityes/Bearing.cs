using BearingStock.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BearingStock.Domain.Entityes
{
	[Table("Bearings")]
	public class Bearing
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? Name { get; set; }

		[Required]
		public string? Type { get; set; }

		[Required]
		public string? Manufacturer { get; set; }

		/// <summary>
		/// Size of the bearing (optional). It could represent diameter, width, etc.
		/// </summary>
		[Required]
		public decimal? Size { get; set; }

		/// <summary>
		/// Type of the size measurement.
		/// </summary>
		[Required]
		public BearingSizeType? SizeType { get; set; }

		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}

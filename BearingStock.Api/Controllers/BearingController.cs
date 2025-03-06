using BearingStock.Core.Services;
using BearingStock.Domain.Entityes;
using Microsoft.AspNetCore.Mvc;

namespace BearingStock.Api.Controllers
{
	[ApiController]
	[Route("api/bearings")]
	public class BearingController : ControllerBase
	{
		private readonly BearingService _bearingService;
		private readonly ILogger<BearingController> _logger;

		public BearingController(BearingService bearingService, ILogger<BearingController> logger)
		{
			_bearingService = bearingService;
			_logger = logger;
		}

		/// <summary>
		/// Get all bearings
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			_logger.LogInformation("Fetching all bearings.");

			var products = await _bearingService.GetAllBearingsAsync();
			return Ok(products);
		}

		/// <summary>
		/// Get a bearing by ID
		/// </summary>
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			_logger.LogInformation("Fetching with ID {BearingId}.", id);

			var product = await _bearingService.GetBearingByIdAsync(id);
			if (product == null)
			{
				_logger.LogWarning("Bearing with ID {BearingId} not found.", id);
				return NotFound($"Bearing with ID {id} not found.");
			}

			return Ok(product);
		}

		/// <summary>
		/// Create a new bearing
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] BearingDto bearingDto)
		{
			if (bearingDto == null)
			{
				_logger.LogWarning("Invalid data received.");
				return BadRequest("Invalid data.");
			}

			var bearing = new Bearing
			{
				Name = bearingDto.Name,
				Type = bearingDto.Type,
				Manufacturer = bearingDto.Manufacturer,
				Size = bearingDto.Size,
				SizeType = bearingDto.SizeType,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await _bearingService.AddBearingAsync(bearing);

			_logger.LogInformation("Created new bearing with ID {BearingId}.", bearing.Id);
			return CreatedAtAction(nameof(GetById), new { id = bearing.Id }, bearing);
		}

		/// <summary>
		/// Update an existing bearing
		/// </summary>
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] BearingDto bearingDto)
		{
			if (bearingDto == null || id != bearingDto.Id)
			{
				_logger.LogWarning("Invalid data received for ID {BearingId}.", id);
				return BadRequest("Invalid data.");
			}

			var existingBearing = await _bearingService.GetBearingByIdAsync(id);
			if (existingBearing == null)
			{
				_logger.LogWarning("Bearing with ID {BearingId} not found.", id);
				return NotFound($"Bearing with ID {id} not found.");
			}

			existingBearing.Name = bearingDto.Name;
			existingBearing.Type = bearingDto.Type;
			existingBearing.Manufacturer = bearingDto.Manufacturer;
			existingBearing.Size = bearingDto.Size;
			existingBearing.SizeType = bearingDto.SizeType;
			existingBearing.UpdatedAt = DateTime.UtcNow;

			await _bearingService.UpdateBearingAsync(existingBearing);
			_logger.LogInformation("Updated bearing with ID {BearingId}.", id);

			return NoContent();
		}

		/// <summary>
		/// Delete a product by ID
		/// </summary>
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _bearingService.GetBearingByIdAsync(id);
			if (product == null)
			{
				_logger.LogWarning("Attempted to delete non-existent bearing with ID {BearingId}.", id);
				return NotFound($"Bearing with ID {id} not found.");
			}

			await _bearingService.DeleteBearingAsync(id);
			_logger.LogInformation("Deleted bearing with ID {BearingId}.", id);

			return NoContent();
		}
	}
}

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using villaApi.DTOs;
using villaApi.Models;
using villaApi.Repository.IRepository;
using villaApi.Services;

namespace villaApi.Controllers
{
	[ApiController]
	[Route("api/villas")]
	public class VillaController : ControllerBase
	{
		public readonly IVillaRepository Ivilla;
		public readonly IMapper Mapper;
		public VillaController(IMapper mapper, IVillaRepository ivilla)
		{
			Ivilla = ivilla;
			Mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetVillas()
		{
			var villas = await Ivilla.GetAllAsync();
			return Ok(villas);
		}
		
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetVilla(int id)
		{
			if (id == 0) return BadRequest();
			var villa = await Ivilla.GetAsync(v => v.Id == id);

			if (villa == null) return NotFound();
			return Ok(villa); 
		}
		
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreatingVilla([FromBody]VillaCreateDTO villaDTO)
		{
			var UniqueName = await Ivilla.GetAsync(u => u.Name.ToLower() == villaDTO.Name.ToLower());
	
			if(UniqueName != null || villaDTO == null)
			{
				ModelState.AddModelError("", "Name already exists or wrong data");
				return BadRequest(ModelState);
			}

			Villa villa = new()
			{
				Name = villaDTO.Name,
				Details = villaDTO.Details,
				ImageUrl = villaDTO.ImageUrl,
				Occupancy = villaDTO.Occupancy,
				Rate = villaDTO.Rate, 
				Sqft = villaDTO.Sqft,
				Amenity = villaDTO.Amenity,
			};
			await Ivilla.CreateAsync(villa);
			await Ivilla.SaveAsync();
			return Ok();
		}
		
		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteVilla(int id)
		{
			if (id == 0) return BadRequest();
			var VillaToBeDeleted = await Ivilla.GetAsync(u => u.Id == id);

			if (VillaToBeDeleted == null) return NotFound();

			await Ivilla.RemoveAsync(VillaToBeDeleted);
			await Ivilla.SaveAsync();
			return NoContent();
		}
		

		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDTO)
		{
			if (id == 0) 
			{
				ModelState.AddModelError("", "");
				return BadRequest(ModelState);
			}
			var VillaToBeUpdated = Ivilla.GetAsync(u => u.Id == id);
			// var VillaToBeUpdated = Context.Villas.Find(id);

			if (VillaToBeUpdated == null) return NotFound();

			// VillaToBeUpdated.Name = villaDTO.Name;
			// VillaToBeUpdated.Details = villaDTO.Details;
			// VillaToBeUpdated.ImageUrl = villaDTO.ImageUrl;
			// VillaToBeUpdated.Occupancy = villaDTO.Occupancy;
			// VillaToBeUpdated.Rate = villaDTO.Rate;
			// VillaToBeUpdated.Sqft = villaDTO.Sqft;
			// VillaToBeUpdated.Amenity = villaDTO.Amenity;
			// VillaToBeUpdated.UpdatedAt = DateTime.Now;
			
			Villa model = Mapper.Map<Villa>(villaDTO);
			Ivilla.UpdateAsync(model);
			Ivilla.SaveAsync();
			return Ok();
		}

		// [HttpPatch("{id:int}")]
		// [ProducesResponseType(StatusCodes.Status204NoContent)]
		// [ProducesResponseType(StatusCodes.Status400BadRequest)]
		// [ProducesResponseType(StatusCodes.Status404NotFound)]
		// public IActionResult UpdatePartialvilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
		// {
		// 	if (patchDTO == null || id == 0) return BadRequest();
		// 	var villa = Context.Villas.FirstOrDefault(u => u.Id == id);
		// 	if (villa == null) return NotFound();
			
		// 	VillaDTO villaDTO = new()
		// 	{
		// 		Name = villa.Name,
		// 		Details = villa.Details,
		// 		ImageUrl = villa.ImageUrl,
		// 		Occupancy = villa.Occupancy,
		// 		Rate = villa.Rate, 
		// 		Sqft = villa.Sqft,
		// 		Amenity = villa.Amenity,
		// 	};

		// 	patchDTO.ApplyTo(villaDTO, ModelState);
		// 	Villa model = new()
		// 	{
		// 		Name = villaDTO.Name,
		// 		Details = villaDTO.Details,
		// 		ImageUrl = villaDTO.ImageUrl,
		// 		Occupancy = villaDTO.Occupancy,
		// 		Rate = villaDTO.Rate,
		// 		Sqft = villaDTO.Sqft,
		// 		Amenity = villaDTO.Amenity,
		// 	};

		// 	Context.Villas.Update(model);
		// 	Context.SaveChanges();
			
		// 	if (!ModelState.IsValid) return BadRequest(ModelState);
		// 	return NoContent();
		// }
	}
}
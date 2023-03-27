using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaintingsWebApi.Dto;
using PaintingsWebApi.Interface;
using PaintingsWebApi.Models;
using System.Data;

namespace PaintingsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : Controller
    {
        private readonly IGalleryRepository galleryRepository;
        private readonly IMapper _mapper;

        public GalleryController(IGalleryRepository galleryRepository, IMapper mapper)
        {
            this.galleryRepository = galleryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Gallery>))]
        public IActionResult GetGalleries()
        {
            var owners = _mapper.Map<List<GalleryDto>>(galleryRepository.GetGalleries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{galleryId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Gallery))]
        [ProducesResponseType(400)]
        public IActionResult GetGallery(int galleryId)
        {
            if (!galleryRepository.GalleryExists(galleryId))
                return NotFound();

            var owner = _mapper.Map<GalleryDto>(galleryRepository.GetGallery(galleryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{galleryId}/painting")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Gallery))]
        [ProducesResponseType(400)]
        public IActionResult GetPaintingByGallery(int galleryId)
        {
            if (!galleryRepository.GalleryExists(galleryId))
            {
                return NotFound();
            }

            var owner = _mapper.Map<List<PaintingDto>>(
                galleryRepository.GetPaintingByGallery(galleryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(owner);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGallery([FromBody] GalleryDto galleryCreate)
        {
            if (galleryCreate == null)
                return BadRequest(ModelState);

            var owners = galleryRepository.GetGalleries()
                .Where(c => c.Name.Trim().ToUpper() == galleryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "Galery already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var galleryMap = _mapper.Map<Gallery>(galleryCreate);

            if (!galleryRepository.CreateGallery(galleryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{galleryId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGallery(int galleryId, [FromBody] GalleryDto updatedGallery)
        {
            if (updatedGallery == null)
                return BadRequest(ModelState);

            if (galleryId != updatedGallery.Id)
                return BadRequest(ModelState);

            if (!galleryRepository.GalleryExists(galleryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = _mapper.Map<Gallery>(updatedGallery);

            if (!galleryRepository.UpdateGallery(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{galleryId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGallery(int galleryId)
        {
            if (!galleryRepository.GalleryExists(galleryId))
            {
                return NotFound();
            }

            var ownerToDelete = galleryRepository.GetGallery(galleryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!galleryRepository.DeleteGallery(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting gallery.");
            }

            return NoContent();

        }


    }
}

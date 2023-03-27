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
    public class PaintingController : Controller
    {
        private readonly IPaintingRepository _paintingRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public PaintingController(IPaintingRepository paintingRepository,
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _paintingRepository = paintingRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Painting>))]
        public IActionResult GetPaintings()
        {
            var paintings = _mapper.Map<List<PaintingDto>>(_paintingRepository.GetPaintings());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(paintings);
        }

        [HttpGet("{paintingId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Painting))]
        [ProducesResponseType(400)]
        public IActionResult GetPainting(int paintingId)
        {
            if (!_paintingRepository.PaintingExists(paintingId))
                return NotFound();

            var painting = _mapper.Map<PaintingDto>(_paintingRepository.GetPainting(paintingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(painting);
        }

        [HttpGet("{paintingId}/rating")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPaintingRating(int paintingId)
        {
            if (!_paintingRepository.PaintingExists(paintingId))
                return NotFound();

            var rating = _paintingRepository.GetPaintingRating(paintingId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePainting([FromQuery] int galleryId, [FromQuery] int categoryId, [FromBody] PaintingDto paintingCreate)
        {
            if (paintingCreate == null)
                return BadRequest(ModelState);

            var paintings = _paintingRepository.GetPaintingTrimToUpper(paintingCreate);

            if (paintings != null)
            {
                ModelState.AddModelError("", "Gallery already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var paintingMap = _mapper.Map<Painting>(paintingCreate);


            if (!_paintingRepository.CreatePainting(galleryId, categoryId, paintingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }

        [HttpPut("{paintingId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePainting(int paintingId, [FromQuery] int galleryId, [FromQuery] int categoryId, [FromBody] PaintingDto updatedPainting)
        {
            if (updatedPainting == null)
                return BadRequest(ModelState);

            if (paintingId != updatedPainting.Id)
                return BadRequest(ModelState);

            if (!_paintingRepository.PaintingExists(paintingId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var paintingMap = _mapper.Map<Painting>(updatedPainting);

            if (!_paintingRepository.UpdatePainting(galleryId, categoryId, paintingMap))
            {
                ModelState.AddModelError("", "Something went wrong updating gallery.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{paintingId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePainitng(int paintingId)
        {
            if (!_paintingRepository.PaintingExists(paintingId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfAPainting(paintingId);
            var paintingToDelete = _paintingRepository.GetPainting(paintingId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews.");
            }

            if (!_paintingRepository.DeletePainting(paintingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}

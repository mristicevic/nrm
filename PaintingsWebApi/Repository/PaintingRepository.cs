using PaintingsWebApi.Data;
using PaintingsWebApi.Dto;
using PaintingsWebApi.Interface;

namespace PaintingsWebApi.Models
{
    public class PaintingRepository : IPaintingRepository
    {
        private readonly DataContext _context;

        public PaintingRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePainting(int galleryId, int categoryId, Painting painting)
        {
            var paintingGalleryEntity = _context.Galleries.Where(a => a.Id == galleryId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var paintingGallery = new PaintingGallery()
            {
                Gallery = paintingGalleryEntity,
                Painting = painting,
            };

            _context.Add(paintingGallery);

            var paintingCategory = new PaintingCategory()
            {
                Category = category,
                Painting = painting,
            };

            _context.Add(paintingCategory);

            _context.Add(painting);

            return Save();
        }

        public bool DeletePainting(Painting painting)
        {
            _context.Remove(painting);
            return Save();
        }

        public Painting GetPainting(int id)
        {
            return _context.Paintings.Where(p => p.Id == id).FirstOrDefault();
        }

        public Painting GetPainting(string name)
        {
            return _context.Paintings.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPaintingRating(int paintingId)
        {
            var review = _context.Reviews.Where(p => p.Painting.Id == paintingId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Painting> GetPaintings()
        {
            return _context.Paintings.OrderBy(p => p.Id).ToList();
        }

        public Painting GetPaintingTrimToUpper(PaintingDto paintingCreate)
        {
            return GetPaintings().Where(c => c.Name.Trim().ToUpper() == paintingCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool PaintingExists(int paintingId)
        {
            return _context.Paintings.Any(p => p.Id == paintingId);
        }

        public bool UpdatePainting(int ownerId, int categoryId, Painting painting)
        {
            _context.Update(painting);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

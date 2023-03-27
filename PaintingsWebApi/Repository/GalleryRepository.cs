using PaintingsWebApi.Data;
using PaintingsWebApi.Interface;

namespace PaintingsWebApi.Models
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly DataContext _context;

        public GalleryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGallery(Gallery owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool DeleteGallery(Gallery owner)
        {
            _context.Remove(owner);
            return Save();
        }

        public Gallery GetGallery(int ownerId)
        {
            return _context.Galleries.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Gallery> GetGalleryOfAPainting(int paintingId)
        {
            return _context.PaintingsGalleries.Where(p => p.Painting.Id == paintingId).Select(o => o.Gallery).ToList();
        }

        public ICollection<Gallery> GetGalleries()
        {
            return _context.Galleries.ToList();
        }

        public ICollection<Painting> GetPaintingByGallery(int galleryId)
        {
            return _context.PaintingsGalleries.Where(p => p.Gallery.Id == galleryId).Select(o => o.Painting).ToList();
        }

        public bool GalleryExists(int ownerId)
        {
            return _context.Galleries.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateGallery(Gallery gallery)
        {
            _context.Update(gallery);
            return Save();
        }
    }
}

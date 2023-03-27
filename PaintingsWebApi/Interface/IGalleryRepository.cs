using PaintingsWebApi.Models;

namespace PaintingsWebApi.Interface
{
    public interface IGalleryRepository
    {
        ICollection<Gallery> GetGalleries();
        Gallery GetGallery(int galleryId);
        ICollection<Gallery> GetGalleryOfAPainting(int paintingId);
        ICollection<Painting> GetPaintingByGallery(int galleryId);
        bool GalleryExists(int ownerId);
        bool CreateGallery(Gallery owner);
        bool UpdateGallery(Gallery owner);
        bool DeleteGallery(Gallery owner);
        bool Save();
    }
}

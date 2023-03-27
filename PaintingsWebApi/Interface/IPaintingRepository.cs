using PaintingsWebApi.Dto;
using PaintingsWebApi.Models;

namespace PaintingsWebApi.Interface
{
    public interface IPaintingRepository
    {
        ICollection<Painting> GetPaintings();
        Painting GetPainting(int id);
        Painting GetPainting(string name);
        Painting GetPaintingTrimToUpper(PaintingDto paintingCreate);
        decimal GetPaintingRating(int paintingId);
        bool PaintingExists(int paintingId);
        bool CreatePainting(int galleryId, int categoryId, Painting painting);
        bool UpdatePainting(int galleryId, int categoryId, Painting painting);
        bool DeletePainting(Painting painting);
        bool Save();
    }
}

namespace PaintingsWebApi.Models
{
    public class PaintingGallery
    {
        public int PaintingId { get; set; }
        public int GalleryId { get; set; }
        public Painting Painting { get; set; }
        public Gallery Gallery { get; set; }
    }
}

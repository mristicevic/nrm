namespace PaintingsWebApi.Models
{
    public class Painting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Artist { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PaintingGallery> PaintingsGalleries { get; set; }
        public ICollection<PaintingCategory> PaintingsCategories { get; set; }

    }
}

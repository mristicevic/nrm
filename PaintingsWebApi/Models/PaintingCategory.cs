namespace PaintingsWebApi.Models
{
    public class PaintingCategory
    {
        public int PaintingId { get; set; }
        public int CategoryId { get; set; }
        public Painting Painting { get; set; }
        public Category Category { get; set; }
    }
}

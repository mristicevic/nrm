namespace PaintingsWebApi.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PaintingGallery> PaintingsGalleries { get; set; }


    }
}

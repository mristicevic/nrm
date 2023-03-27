using PaintingsWebApi.Data;
using PaintingsWebApi.Models;
using System.Diagnostics.Metrics;

namespace PaintingsWebApi
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.PaintingsCategories.Any())
            {
                var paintingsGalleris = new List<PaintingGallery>()
                {
                    new PaintingGallery()
                    {
                        Painting = new Painting()
                        {
                            Name = "Mona Lisa",
                            DateOfCreation = new DateTime(1600,1,1),
                            Artist = "Leonardo da Vinci",
                            PaintingsCategories = new List<PaintingCategory>()
                            {
                                new PaintingCategory { Category = new Category() { Name = "Small paintings"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Mistery smile",Text = "Lorem ipsum.....", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Sam", LastName = "Smith" , Country = "Canada", BirthDate = new DateTime(1983,1,1)} },
                                new Review { Title="On of the greatest painting ever", Text = "Lorem ipsum.....", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Berry", Country = "UK", BirthDate = new DateTime(1953,1,1) } },
                                new Review { Title="Da Vinci's best painting",Text = "Lorem ipsum.....", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Tina", LastName = "Artur", Country = "France", BirthDate = new DateTime(1993,1,1) } },
                            }
                        },
                        Gallery = new Gallery()
                        {
                            Name = "Louvre",
                            City = "Paris",
                            Country = "France",
                        }
                    },
                    new PaintingGallery()
                    {
                        Painting = new Painting()
                        {
                            Name = "The Death of Socrates",
                            DateOfCreation = new DateTime(1787,1,1),
                            Artist = "Jacques-Louis David",
                            PaintingsCategories = new List<PaintingCategory>()
                            {
                                new PaintingCategory { Category = new Category() { Name = "Oil on canvas"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="History",Text = "Lorem ipsum.....", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Sam", LastName = "Smith", Country = "Canada", BirthDate = new DateTime(1983,1,1)} },
                                new Review { Title="Sokrates", Text = "Lorem ipsum.....", Rating = 3,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Berry", Country = "UK", BirthDate = new DateTime(1953,1,1) } },
                                new Review { Title="Philosophers in painting",Text = "Lorem ipsum.....", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Tina", LastName = "Artur", Country = "France", BirthDate = new DateTime(1993,1,1) } },
                            }
                        },
                        Gallery = new Gallery()
                        {
                            Name = "The Metropolitan Museum of Art",
                            City = "New York",
                            Country = "USA",
                        }
                    },
                     new PaintingGallery()
                    {
                        Painting = new Painting()
                        {
                            Name = "The School of Athens",
                            DateOfCreation = new DateTime(1511,1,1),
                            Artist = "Raphael Santi",
                            PaintingsCategories = new List<PaintingCategory>()
                            {
                                new PaintingCategory { Category = new Category() { Name = "Fresco"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Vatican fresco",Text = "Lorem ipsum.....", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Sam", LastName = "Smith", Country = "Canada", BirthDate = new DateTime(1983,1,1)} },
                                new Review { Title="Breathtaking fresco in Vatican", Text = "Lorem ipsum.....", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Berry", Country = "UK", BirthDate = new DateTime(1953,1,1) } },
                                new Review { Title="Philosophers",Text = "Lorem ipsum.....", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Tina", LastName = "Artur", Country = "France", BirthDate = new DateTime(1993,1,1) } },
                            }
                        },
                        Gallery = new Gallery()
                        {
                            Name = "Apostolic Palace",
                            City = "Vatican city",
                            Country = "Vatican City",
                        }
                    }

                };
                dataContext.PaintingsGalleries.AddRange(paintingsGalleris);
                dataContext.SaveChanges();
            }
        }
    }
  
   
}

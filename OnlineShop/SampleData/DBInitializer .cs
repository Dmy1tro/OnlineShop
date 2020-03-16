using OnlineShop.Concrete;
using OnlineShop.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.SampleData
{
    public class DBInitializer
    {
        public void Seed(EFDbContext context)
        {
            var genres = this.GetGenres();

            var books = this.GetBooks();

            var users = this.GetUsers();

            if (!context.Genres.Any())
            {
                context.Genres.AddRange(genres);

                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                context.Books.AddRange(books);

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(users);

                context.SaveChanges();
            }
        }

        private List<Genre> GetGenres()
        {
            return new List<Genre>
            {
                new Genre
                {
                    GenreName = "History"
                },
                new Genre
                {
                    GenreName = "Anime"
                }
            };
        }


        private List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book
                {
                    Name = "Book_1",
                    GenreId = 1,
                    Description = "Description_1",
                    Author = "Rey Bredbery",
                    Price = 1488,
                    Year = 1234
                },

                new Book
                {
                    Name = "Book_2",
                    GenreId = 2,
                    Description = "Description_2",
                    Author = "Omar Hayam",
                    Price = 1488,
                    Year = 1234
                }
            };
        }


        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    UserName = "Gym Bo$$",
                    Password = "Nagibator777".GetHashCode().ToString(),
                    ActivatedEmail = true,
                    Email = "Destroyer@qq.qq",
                }
            };
        }
    }
}
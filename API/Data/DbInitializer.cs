using API.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibContext context)
        {
            if(context.Books.Any()) return;
            var Books = new List<Book>
            {
                new Book
                {
                    Name = "Angular Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 20000,
                    Type = "Boards",
                    Qty = 100
                },
                new Book
                {
                    Name = "Green Angular Board 3000",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 15000,
                    Type = "Boards",
                    Qty = 100
                },
                new Book
                {
                    Name = "Core Board Speed Rush 3",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    Type = "Boards",
                    Qty = 100
                },
                new Book
                {
                    Name = "Net Core Super Board",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 30000,
                    Type = "Boards",
                    Qty = 100
                }
            };
            foreach(var Book in Books){
                context.Books.Add(Book);
            }
            context.SaveChanges();
        }
    }
}
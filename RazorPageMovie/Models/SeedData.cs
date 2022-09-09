using Microsoft.EntityFrameworkCore;
using RazorPageMovie.Data;

namespace RazorPageMovie.Models;

public static class SeedData
{
    public static void Initalize(IServiceProvider serviceProvider)
    {
        using (var context = 
               new RazorPageMovieContext(
                   serviceProvider.GetRequiredService<DbContextOptions<RazorPageMovieContext>>()))
        {
            // 判断空
            if (context == null || context.Movie == null)
            {
                throw new ArgumentException("Null RazorPagesMovieContext");
            }
            
            // 判断数据库内容
            if (context.Movie.Any())
            {
                return;
            }
            
            // 否则加入一些电影记录
            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseTime = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M,
                    Rating = "R"
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseTime = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M,
                    Rating = "G"
                },

                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseTime = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M,
                    Rating = "G"
                },

                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseTime = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "NA"
                }
            );
            context.SaveChanges();
        }
    }
}
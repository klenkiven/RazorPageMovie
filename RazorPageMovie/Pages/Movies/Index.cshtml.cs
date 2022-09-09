using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPageMovie.Data;
using RazorPageMovie.Models;

namespace RazorPageMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPageMovie.Data.RazorPageMovieContext _context;

        // 容器自动注入这个类型的对象依赖
        public IndexModel(RazorPageMovie.Data.RazorPageMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        // 用户在搜索框输入的内容
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        // 包含 流派 下拉列表
        public SelectList? Genres { get; set; }
        // 包含用户选择的特定 流派
        [BindProperty(SupportsGet = true)]
        public string ? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            // 流派搜索条件
            var genreQuery = from m in _context.Movie
                select m.Genre;
            // 电影搜索条件
            var movies = from m in _context.Movie
                select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(m => m.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(m => m.Genre == MovieGenre);
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}

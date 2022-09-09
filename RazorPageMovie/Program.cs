using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPageMovie.Data;
using RazorPageMovie.Models;

var builder = WebApplication.CreateBuilder(args);

// 将 Razor 添加到容器中
builder.Services.AddRazorPages();
// 将 数据库上下文 添加到容器中
builder.Services.AddDbContext<RazorPageMovieContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RazorPageMovieContext") ?? throw new InvalidOperationException("Connection string 'RazorPageMovieContext' not found.")));
// 将 健康检查插件 添加到容器中
builder.Services.AddHealthChecks();

// 构建 应用包括构建容器
var app = builder.Build();

// 初始化 数据库内容
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    SeedData.Initalize(serviceProvider);
}

// Configure the HTTP request pipeline.
// 配置 HTTP 请求管道：Middleware, see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
if (!app.Environment.IsDevelopment())
{
    // this 关键字主要作用：
    // 1. 代表 对象自己
    // 2. 静态方法：增加 this 参数，代表可以按照扩展方法的方式调用
    // ExceptionHandlerExtensions.UseExceptionHandler(app, "/Error");
    app.UseExceptionHandler("/Error");
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// This app dosen't use Authorization, this line can be removed.
// app.UseAuthorization();

app.MapRazorPages();

// Map health checks
app.MapHealthChecks("/healthz");

app.Run();
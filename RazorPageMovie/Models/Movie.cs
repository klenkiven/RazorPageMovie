using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPageMovie.Models;

/// <summary>
/// 电影
/// <para>这是一个 POCO 类，类似于 Java 中的 POJO 类</para>
/// </summary>
public class Movie
{
    // C# 中可以在实体类中添加一个 字段（field）
    public int ID { get; set; }
    
    // 对于字段而言，可以设置它的默认值 下面的两种方式都是有效的
    //[StringLength(60, MinimumLength = 3)]
    //[Required]
    [StringLength(60, MinimumLength = 3), Required]
    public string Title { get; set; } = string.Empty;
    
    // 这里使用到了 注解（Attribute） 标注这个字段的类型
    [Display(Name = "Release Date"), DataType(DataType.Date)]
    // 展示格式在这里设置
    // [DisplayFormat(DataFormatString = "0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
    // 关于日期的数据校验，需要使用Range来校验参数，但是不建议这样使用
    // see https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/validation?view=aspnetcore-6.0&tabs=visual-studio-code#use-datatype-attributes
    // [Range(typeof(DateTime), "1/1/1966", "1/1/2020")]
    public DateTime ReleaseTime { get; set; }
    
    // 类型
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
    public string Genre { get; set; } = string.Empty;
    
    // 价格
    // 这里使用到了一个比较特别的类型 decimal
    [Range(1, 100), DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    // 排名
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5)]
    public string Rating { get; set; } = string.Empty;
    
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomersApp.Model;

[Table("year_stats")]
public class YearStats
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("year")]
    public int Year { get; set; }
    
    [Column("count")]
    public int Count { get; set; }
}
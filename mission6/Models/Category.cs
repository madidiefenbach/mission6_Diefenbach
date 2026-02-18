using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mission6_Diefenbach.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Column("CategoryId")]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}

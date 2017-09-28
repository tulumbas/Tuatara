using System.ComponentModel.DataAnnotations;

namespace Tuatara.Data.Models
{
    public class Intraweek
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public int DefaultSortOrder { get; set; }
    }
}

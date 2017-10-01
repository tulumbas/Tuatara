using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuatara.Data.Entities
{
    [Table("PlaybookStatuses")]
    public class PlaybookStatusEntity : IBaseEntity
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

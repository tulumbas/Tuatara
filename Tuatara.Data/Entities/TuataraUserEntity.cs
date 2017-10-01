using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuatara.Data.Entities
{
    [Table("TuataraUsers")]
    public class TuataraUserEntity : IBaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string DomainName { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }
    }

}

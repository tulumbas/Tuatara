using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuatara.Data.Entities
{
    [Table("Works")]
    public class WorkEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public int? ParentID { get; set; }
        public virtual WorkEntity Parent { get; set; }
        public virtual ICollection<WorkEntity> SubItems { get; set; }
    }
}

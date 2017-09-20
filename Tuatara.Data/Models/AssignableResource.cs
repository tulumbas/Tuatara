using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuatara.Data.Models
{
    public class AssignableResource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public bool IsBookable { get; set; }

        public virtual ICollection<AssignableResource> SubResources { get; set; }

        public int? ParentID { get; set; }

        public virtual AssignableResource Parent { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuatara.Data.Models
{
    public class Work
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string ProjectName { get; set; }

        public int? ParentID { get; set; }
        public virtual Work Parent { get; set; }
        public virtual ICollection<Work> SubItems { get; set; }
    }
}

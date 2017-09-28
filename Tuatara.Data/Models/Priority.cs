using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuatara.Data.Models
{
    public class Priority
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

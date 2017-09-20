using System.ComponentModel.DataAnnotations;

namespace Tuatara.Data.Models
{
    public class TuataraUser
    {
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

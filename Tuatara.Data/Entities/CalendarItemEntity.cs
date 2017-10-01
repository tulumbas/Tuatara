using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tuatara.Data;

namespace Tuatara.Data.Entities
{
    [Table("CalendarItems")]
    public class CalendarItemEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int WeekNo { get; set; }
        public PeriodTypes PeriodType { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tuatara.Data;

namespace Tuatara.Data.Models
{
    public class CalendarItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int WeekNo { get; set; }
        public PeriodTypes PeriodType { get; set; }
    }

}

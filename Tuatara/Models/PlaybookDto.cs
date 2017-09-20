using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tuatara.Models
{
    public class PlaybookDto
    {
        public CalendarItemDto Week { get; set; }
        public List<PlaybookRowDto> Rows { get; } = new List<PlaybookRowDto>();
    }

}
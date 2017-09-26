using System.Collections.Generic;

namespace Tuatara.Data.Dto
{
    public class PlaybookDto
    {
        public CalendarItemDto Week { get; set; }
        public List<PlaybookRowDto> Rows { get; } = new List<PlaybookRowDto>();
    }

}
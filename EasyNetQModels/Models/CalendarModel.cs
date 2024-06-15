using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class CalendarModel
    {
        public string drId { get; set; } = null!;
        public string AppId { get; set; } = null!;
        public string dayName { get; set; } = null!;
        public string dayTime { get; set; } = null!;
    }
}

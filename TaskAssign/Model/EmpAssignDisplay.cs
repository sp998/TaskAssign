using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssign.Model
{
    public class EmpAssignDisplay
    {

        public int AssignId { get; set; }
        public string TaskName { get; set; }
        public string EmpName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Completed { get;set; }
        public int TotalHours { get; set; }
        public string TimePeriod { get; set; }
    }
}

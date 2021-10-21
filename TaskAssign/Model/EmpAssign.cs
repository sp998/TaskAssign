using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssign.Model
{
    public class EmpAssign
    {
        [Key]
        public int AssignId { get; set; }

        public int TaskId { get; set; }

        public string EmpEmail { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool Completed { get; set; }
        public int TotalHours { get; set; }
    }
}

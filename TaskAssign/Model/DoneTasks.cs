using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssign.Model
{
    public class DoneTasks
    {
        [Key]
        public int Id { get; set; }

        public int TaskId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }
}

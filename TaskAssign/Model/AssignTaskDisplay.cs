using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssign.Model
{
    public class AssignTaskDisplay
    {

        public int AssignId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        
        public DateTime AssignedDate { get; set; }

        public bool Completed { get; set; }
        }
    
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAssign.Model
{
    
    [Serializable]
    public class EmpTask
    {
        [Key]
        public int TaskId { get; set; }
        
        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public bool Completed { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;


namespace TaskAssign.Model
{
    public class StartViewModel
    {
        public  EmpAssign EmpAssign { get; set; }
        public List<DoneTasks> DoneTasks { get; set; }
        public string TaskName{ get; set; }
    }
}

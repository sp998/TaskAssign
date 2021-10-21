using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAssign.Model;

namespace TaskAssign.Data
{
    public class EmpAssignContext : DbContext
    {
        public EmpAssignContext (DbContextOptions<EmpAssignContext> options)
            : base(options)
        {
        }

        public DbSet<TaskAssign.Model.EmpAssign> EmpAssign { get; set; }
        public DbSet<TaskAssign.Model.DoneTasks> DoneTasks { get; set; }
    }
}

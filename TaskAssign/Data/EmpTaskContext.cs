using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAssign.Model;

namespace TaskAssign.Data
{
    public class EmpTaskContext : DbContext
    {
        public EmpTaskContext (DbContextOptions<EmpTaskContext> options)
            : base(options)
        {
        }

        public DbSet<TaskAssign.Model.EmpTask> EmpTask { get; set; }

      
    }
}

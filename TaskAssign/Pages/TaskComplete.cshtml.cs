using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskAssign.Data;

namespace TaskAssign.Pages
{
    public class TaskCompleteModel : PageModel
    {
        private readonly EmpAssignContext _empAssignContext;
        public TaskCompleteModel(EmpAssignContext empAssignContext)
        {
            _empAssignContext = empAssignContext;
        }

        public TimeSpan TimePeriod{get;set;}
        public int? Id { get; set; }
        public async Task OnGetAsync(int? id)
        {
           
           
            if (id != null)
            {
                var emptask = await _empAssignContext.EmpAssign.FindAsync(id);
                if (emptask != null)
                {
                    TimePeriod = emptask.EndTime - emptask.StartTime;
                }
            }
        }
    }
}

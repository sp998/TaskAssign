using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskAssign.Data;
using TaskAssign.Model;

namespace TaskAssign.Pages.AssignTasks
{
    public class DetailsModel : PageModel
    {
        private readonly TaskAssign.Data.EmpAssignContext _context;

        public DetailsModel(TaskAssign.Data.EmpAssignContext context)
        {
            _context = context;
        }

        public EmpAssign EmpAssign { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmpAssign = await _context.EmpAssign.FirstOrDefaultAsync(m => m.AssignId == id);

            if (EmpAssign == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

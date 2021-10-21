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
    public class DeleteModel : PageModel
    {
        private readonly TaskAssign.Data.EmpAssignContext _context;

        public DeleteModel(TaskAssign.Data.EmpAssignContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmpAssign = await _context.EmpAssign.FindAsync(id);
            
            if (EmpAssign != null)
            {
                var doneTasks = _context.DoneTasks.Where(task => task.TaskId == EmpAssign.AssignId).ToList();
                if (doneTasks.Count > 0)
                {
                    _context.DoneTasks.RemoveRange(doneTasks);
                }

                _context.EmpAssign.Remove(EmpAssign);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

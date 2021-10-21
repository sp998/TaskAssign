using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskAssign.Data;
using TaskAssign.Model;

namespace TaskAssign.Pages.AssignTasks
{
    public class EditModel : PageModel
    {
        private readonly TaskAssign.Data.EmpAssignContext _context;

        public EditModel(TaskAssign.Data.EmpAssignContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EmpAssign).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpAssignExists(EmpAssign.AssignId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmpAssignExists(int id)
        {
            return _context.EmpAssign.Any(e => e.AssignId == id);
        }
    }
}

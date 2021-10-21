using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskAssign.Areas.Identity;
using TaskAssign.Data;
using TaskAssign.Model;

namespace TaskAssign.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly TaskAssign.Data.EmpTaskContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public bool IsAdmin { get; set; }
        public DeleteModel(TaskAssign.Data.EmpTaskContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public EmpTask EmpTask { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var email = HttpContext.User.Identity.Name;
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            }

            if (id == null)
            {
                return NotFound();
            }

            EmpTask = await _context.EmpTask.FirstOrDefaultAsync(m => m.TaskId == id);

            if (EmpTask == null)
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

            EmpTask = await _context.EmpTask.FindAsync(id);

            if (EmpTask != null)
            {
                _context.EmpTask.Remove(EmpTask);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

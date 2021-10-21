using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskAssign.Areas.Identity;
using TaskAssign.Data;
using TaskAssign.Model;

namespace TaskAssign.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly TaskAssign.Data.EmpTaskContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
      
        public bool IsAdmin{ get; set; }
    
        public CreateModel(TaskAssign.Data.EmpTaskContext context,UserManager<ApplicationUser>userManger)
        {
            _context = context;
            _userManager = userManger; 
        }



        [BindProperty]
        public EmpTask EmpTask { get; set; }


        public async Task OnGet()
        {
            var email = HttpContext.User.Identity.Name;
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            }
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.EmpTask.Add(EmpTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

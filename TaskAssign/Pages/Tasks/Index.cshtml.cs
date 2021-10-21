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
    public class IndexModel : PageModel
    {
        private readonly TaskAssign.Data.EmpTaskContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public bool IsAdmin { get; set; }
        public IndexModel(TaskAssign.Data.EmpTaskContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<EmpTask> EmpTask { get;set; }

        public async Task OnGetAsync()
        {
            var email = HttpContext.User.Identity.Name;
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            }
            EmpTask = await _context.EmpTask.ToListAsync();
        }
    }
}

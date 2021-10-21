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

namespace TaskAssign.Pages.AssignTasks
{
    public class CreateModel : PageModel
    {
        private readonly TaskAssign.Data.EmpAssignContext _context;
        private readonly EmpTaskContext _taskContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateModel(TaskAssign.Data.EmpAssignContext context,
            EmpTaskContext taskContext,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _taskContext = taskContext;
            _userManager = userManager;
        }


        public IList<EmpTask> TaskList { get; set; }
        public IList<ApplicationUser> ApplicationUsers { get; set; }


      

        public void OnGet()
        {
            var tasks = _taskContext.EmpTask.ToList();
            TaskList = tasks;
            var email = HttpContext.User.Identity.Name;
            var users = _userManager.Users.Where(user => user.Email != email).ToList();
            ApplicationUsers = users;


        }

        [BindProperty]
        public EmpAssignResult EmpAssignResult { get; set; }

       

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var EmpAssign = new EmpAssign()
            {
                TaskId = EmpAssignResult.TaskId,
                EmpEmail = EmpAssignResult.EmpEmail,
            };
          _context.EmpAssign.Add(EmpAssign);
           await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

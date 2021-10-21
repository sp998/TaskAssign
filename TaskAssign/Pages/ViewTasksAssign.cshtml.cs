using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using TaskAssign.Areas.Identity;
using TaskAssign.Data;
using TaskAssign.Extension;
using TaskAssign.Model;

namespace TaskAssign.Pages
{
    public class ViewTasksAssignModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmpAssignContext _empAssignContext;
        private readonly EmpTaskContext _taskContext;
        public ViewTasksAssignModel(UserManager<ApplicationUser> userManager,EmpAssignContext empAssignContext,EmpTaskContext taskContext)
        {
            _userManager = userManager;
            _empAssignContext = empAssignContext;
            _taskContext = taskContext;
           
        }
        [BindProperty(SupportsGet =true)]
        public string Name { get; set; }

        [BindProperty]
        public int Id { get; set; }

        public IList<AssignTaskDisplay> AssignTasks { get; set; }


        private async Task<string> GetUserNameByEmail(string email)
        {
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    return user.Name;
                }

            }
            return " ";
        }

        private string GetTaskNameById(int id)
        {
            var task = _taskContext.EmpTask.Where(task => task.TaskId == id).FirstOrDefault();
            if (task != null)
            {
                return task.TaskName;
            }
            return " ";

        }

        private string GetDecritptionById(int id)
        {
            var task = _taskContext.EmpTask.Where(task => task.TaskId == id).FirstOrDefault();
            if (task != null)
            {
                return task.TaskDescription;
            }
            return " ";

        }
        public EmpAssign EmpTask { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            EmpTask = await _empAssignContext.EmpAssign.FindAsync(Id);

            if (EmpTask != null)
            {
                EmpTask.Completed = true;
                EmpTask.EndTime = DateTime.Now;
                _empAssignContext.EmpAssign.Update(EmpTask);
                await _empAssignContext.SaveChangesAsync();
            }

            return RedirectToPage("./TaskComplete", new {id=Id});
        }


        public void OnGet()
        {
            AssignTasks = new List<AssignTaskDisplay>();
            var email = HttpContext.User.Identity.Name;
            if (email != null)
            {
                var tasks = _empAssignContext.EmpAssign.Where(task => task.EmpEmail == email).ToList();
              
                foreach (var task in tasks)
                {
                    AssignTasks.Add(new AssignTaskDisplay() { 
                     TaskName = GetTaskNameById(task.TaskId),
                     TaskDescription = GetDecritptionById(task.TaskId),
                     AssignedDate = task.StartTime,
                     Completed = task.Completed,
                     AssignId = task.AssignId
                    });
                }
                
            }
            }
        }
    }


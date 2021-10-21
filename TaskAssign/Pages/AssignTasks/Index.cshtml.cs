using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskAssign.Areas.Identity;
using TaskAssign.Data;
using TaskAssign.Model;
using TaskAssign.Utils;

namespace TaskAssign.Pages.AssignTasks
{
    public class IndexModel : PageModel
    {
        private readonly EmpAssignContext _context;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly EmpTaskContext _taskContext;
       
        public IndexModel(TaskAssign.Data.EmpAssignContext context,UserManager<ApplicationUser> userManager,EmpTaskContext taskContext)
        {
            _context = context;
            _userManger = userManager;
            _taskContext = taskContext;
        }

        public IList<EmpAssignDisplay> EmpAssigns { get;set; }

        private async Task<string> GetUserNameByEmail(string email)
        {
            if (email != null)
            {
                var user = await _userManger.FindByEmailAsync(email);
                if (user != null)
                {
                    return user.Name;
                }
                
            }
            return " ";
        }

        private string GetTaskNameById(int id)
        {
            var task=  _taskContext.EmpTask.Where(task => task.TaskId == id).FirstOrDefault();
            if (task != null)
            {
                return task.TaskName;
            }
            return " ";
           
        }


       

        public async Task OnGetAsync()
        {
           var EmpAssign= await _context.EmpAssign.ToListAsync();
            EmpAssigns = new List<EmpAssignDisplay>();
        
            foreach (var item in EmpAssign)
            {
                var timeDuration = new TimeSpan();
                var doneTasks = _context.DoneTasks.Where(doneItem => doneItem.TaskId == item.AssignId);
                var doneTaskTimeSpans = doneTasks.Select(task => task.EndTime - task.StartTime);

                foreach(var tspan in doneTaskTimeSpans)
                {
                    timeDuration = timeDuration.Add(tspan);
                }
               
                EmpAssigns.Add(new EmpAssignDisplay()
                {
                    EmpName = await GetUserNameByEmail(item.EmpEmail),
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    AssignId = item.AssignId,
                    TaskName = GetTaskNameById(item.TaskId),
                    Completed = item.Completed,
                    TimePeriod =TimeUtils.GetTime(timeDuration)
                }
                    ) ; 
                
            }
        }
    }
}

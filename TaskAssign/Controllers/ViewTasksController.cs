using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.Areas.Identity;
using TaskAssign.Data;
using TaskAssign.Model;
using TaskAssign.Utils;

namespace TaskAssign.Controllers
{

    [Authorize]
    public class ViewTasksController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmpAssignContext _empAssignDb;
        private readonly EmpTaskContext _empTaskDb;

        public ViewTasksController(
            UserManager<ApplicationUser> userManager,
            EmpAssignContext empAssignDb,
            EmpTaskContext empTaskDb)
        {
            _userManager = userManager;
            _empAssignDb = empAssignDb;
            _empTaskDb = empTaskDb;
        }

        public EmpTask GetTask(int id)
        {
            return _empTaskDb.EmpTask.Find(id);
        }
        
        public IActionResult Index()
        {
            var assigntasks = new List<EmpAssign>();
            var tasks = new List<AssignTaskDisplay>();
            var user_email = HttpContext.User.Identity.Name;
            if (user_email != null)
            {
              assigntasks= _empAssignDb.EmpAssign.Where(assignTask => assignTask.EmpEmail == user_email).ToList();
            }
            foreach(var assignTask in assigntasks)
            {
                tasks.Add(new AssignTaskDisplay()
                {
                    AssignId=assignTask.AssignId,
                    TaskName=GetTask(assignTask.TaskId).TaskName,
                    TaskDescription=GetTask(assignTask.TaskId).TaskDescription,
                    AssignedDate=assignTask.StartTime,
                    Completed=assignTask.Completed
                });
            }
            return View(tasks);
        }
       public EmpAssign GetData(int id)
        {
            var assignTask = _empAssignDb.EmpAssign.Find(id);
            return assignTask;
        }
       public IActionResult StartTask(int Id){
            var data = GetData(Id);
            var  doneTasks = _empAssignDb.DoneTasks.Where(item => item.TaskId == Id).ToList();
            var viewModel = new StartViewModel
            { 
                DoneTasks=doneTasks,
                EmpAssign=data,
                TaskName =GetTask(data.TaskId).TaskName
            };
        
            if (data == null) return NotFound();
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Start(int AssignId)
        {
            var task = _empAssignDb.EmpAssign.Find(AssignId);
            var doneTasks = _empAssignDb.DoneTasks.Where(item => item.TaskId == AssignId).ToList();
            if (doneTasks.Count == 0)
            {
                task.StartTime = DateTime.Now;
                _empAssignDb.EmpAssign.Update(task);
                _empAssignDb.SaveChanges();
            }
            var doneTask = new DoneTasks
            {
                TaskId = AssignId,
                StartTime = DateTime.Now
                
            };
            _empAssignDb.DoneTasks.Add(doneTask);
            _empAssignDb.SaveChanges();
            return RedirectToAction("StartTask",new { Id = AssignId });
        }

        public IActionResult End(int AssignId)
        {
            var doneTask = _empAssignDb.DoneTasks.Where(item => item.TaskId == AssignId).ToList().Last();
            doneTask.EndTime = DateTime.Now;
            _empAssignDb.DoneTasks.Update(doneTask);
            _empAssignDb.SaveChanges();
            
           return RedirectToAction("StartTask", new { Id = AssignId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Complete(int AssignId)
        {
            var task = _empAssignDb.EmpAssign.Find(AssignId);
            task.EndTime = DateTime.Now;
            task.Completed = true;
            _empAssignDb.EmpAssign.Update(task);
            _empAssignDb.SaveChanges();
            return RedirectToAction("TotalTime",new { id = AssignId });
        }

        public IActionResult TotalTime(int id)
        {
            var doneTasks = _empAssignDb.DoneTasks.Where(item => item.TaskId == id).ToList();
            var doneTaskTimeSpans = doneTasks.Select(task => task.EndTime - task.StartTime);
            var timeDuration = new TimeSpan();
            foreach(var tspan in doneTaskTimeSpans)
            {
                timeDuration=timeDuration.Add(tspan);
            }
            return View(model:TimeUtils.GetTime(timeDuration));
        }

        public IActionResult CompleteTask(int Id){
            var data = GetData(Id);
            if (data == null) return NotFound();
            return View(data);

        } 
    }
}

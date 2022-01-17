using Microsoft.AspNetCore.Mvc;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    public class TasksController : Controller
    {
        private TaskTrackerContext _db;

        public TasksController(TaskTrackerContext db)
        {
            _db = db;
        }
        
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
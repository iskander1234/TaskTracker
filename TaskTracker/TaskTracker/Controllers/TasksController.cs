using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Models;
using TaskTracker.ViewModels;

namespace TaskTracker.Controllers
{
    public enum SortState
    {
        NameAsc,
        NameDesc,
        PriorityAcs,
        PriorityDesc,
        StatusAsc,
        StatusDesc,
        DataAsc,
        DataDesc
    }
    public class TasksController : Controller
    {
        private TaskTrackerContext _db;

        public TasksController(TaskTrackerContext db)
        {
            _db = db;
        }
        
               // GET
        public IActionResult Index(int id, int page = 1, SortState sortOrder = SortState.NameAsc)
        {

            ViewBag.NameSort = sortOrder == SortState.NameAsc ?
                SortState.NameDesc : SortState.NameAsc;
            ViewBag.PrioritySort = sortOrder == SortState.PriorityAcs ?
                SortState.PriorityDesc : SortState.PriorityAcs;
            ViewBag.StatusSort = sortOrder == SortState.StatusAsc ? 
                SortState.StatusDesc : SortState.StatusAsc;
            ViewBag.DataSort = sortOrder == SortState.DataAsc ? 
                SortState.DataDesc : SortState.DataAsc;
            
            int pageSize = 10;
            IQueryable<Task> tasks = _db.Tasks.Where(t => !t.IsDeleted);
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    tasks = tasks.OrderByDescending(p => p.Name);
                    break;
                case SortState.PriorityAcs:
                    tasks = tasks.OrderBy(p => p.Priority);
                    break;
                case SortState.PriorityDesc:
                    tasks = tasks.OrderByDescending(p => p.Priority);
                    break;
                case SortState.StatusAsc:
                    tasks = tasks.OrderBy(p => p.Status);
                    break;
                case SortState.StatusDesc:
                    tasks = tasks.OrderByDescending(p => p.Status);
                    break;
                case SortState.DataAsc:
                    tasks = tasks.OrderBy(p => p.ClosedDate);
                    break;
                case SortState.DataDesc:
                    tasks = tasks.OrderByDescending(p => p.ClosedDate);
                    break;
                default:
                    tasks = tasks.OrderBy(p => p.Name);
                    break;
            }
            
            var count = tasks.Count();
            var items = tasks.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(page, count, pageSize);
            pageViewModel.SortState = sortOrder;
            TaskIndexViewModel viewModel = new TaskIndexViewModel()
            {
                PageViewModel   = pageViewModel,
                Tasks = items.ToList()
            };
            
            return View(viewModel);
        }
        
        public IActionResult Edit(int? id)
        {
            Task task = _db.Tasks.FirstOrDefault(p => p.Id == id);
            ViewModel viewModel = new ViewModel()
            {
                Task = task
            };
            return View(viewModel);
        }
    
        [HttpPost]
        public IActionResult Edit(ViewModel model)
        {
            if (model.Task != null)
            {
                _db.Tasks.Update(model.Task);
                _db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Task task)
        {
            if (task != null)
            {
                _db.Tasks.Add(task);
                _db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        
        public IActionResult Details(int id)
        {
            Task task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            
            if (task is null)
                return NotFound();
            
            return View(task);
        }
        
        public IActionResult Delete(int id)
        {
            Task task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            { 
                return View(task);
            }
            return NotFound();
        }
        
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            Task task =  _db.Tasks.FirstOrDefault(t => t.Id == id);
            
            if (task != null)
            {
                task.IsDeleted = true;
                _db.Tasks.Update(task);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return NotFound();
        }

        public IActionResult Open(int id)
        {
            Task task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task is null)
                return NotFound();
            else
            {
                task.Status = Status.Open;
                task.OpenedDate = DateTime.Now;
                _db.Tasks.Update(task);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Close(int id)
        {
            Task task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task is null)
                return NotFound();
            else
            {
                task.Status = Status.Close;
                task.ClosedDate = DateTime.Now;
                _db.Tasks.Update(task);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
    }
}
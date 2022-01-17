using System.Collections.Generic;
using System.Threading.Tasks;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.ViewModels
{
    public class TaskIndexViewModel
    {
        public List<Task> Tasks { get; set; }
        
        public PageViewModel PageViewModel { get; set; }
    }
}
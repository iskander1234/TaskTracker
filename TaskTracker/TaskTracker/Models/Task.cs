using System;

namespace TaskTracker.Models
{
    public enum Status
    {
        New,
        Open,
        Close
    }
    public enum Priority
    {
        Tall,
        Middle,
        Low
    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.New;
        public bool IsDeleted { get; set; } = false;
        public Priority Priority { get; set; }

        public DateTime СreatedDate { get; set; } = DateTime.Now;

        public DateTime? ClosedDate { get; set; }

        public DateTime? OpenedDate { get; set; }
    }
}
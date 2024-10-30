
namespace rostislaim.TaskPlanner.Domain.Models
{
    public enum Priority
    {
        None = 0,
        Low = 1,
        Medium = 2,
        High = 3,
        Urgent = 4,
    }

    public enum Complexity
    {
        None = 0,
        Minutes = 1,
        Hours = 2,
        Days = 3,
        Weeks = 4,
    }

    public class WorkItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueTime { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Complexity Complexity { get; set; }
        public Priority Priority { get; set; }

        public override string ToString()
        {
            return $"{Title}: due to {DueTime.ToString("dd/MM/yyyy")}, {Priority.ToString().ToLower()} priority, id:{Id} , completed: {IsCompleted}";
        }

        public WorkItem Clone()
        {
            WorkItem item = new WorkItem();
            item.CreationDate = this.CreationDate;
            item.DueTime = this.DueTime;
            item.Title = this.Title;
            item.Id = this.Id;
            item.Description = this.Description;
            item.IsCompleted = this.IsCompleted;
            item.Complexity = this.Complexity;
            item.Priority = this.Priority;
            return item ;
        }
    }
}

using Domain.Common;

namespace Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; } 
        public bool IsCompleted { get; set; }
    }
}

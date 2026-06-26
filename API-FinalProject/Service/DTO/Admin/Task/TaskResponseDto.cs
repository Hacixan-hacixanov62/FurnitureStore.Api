namespace Service.DTO.Admin.Task
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public bool IsCompleted { get; set; }
    }
}

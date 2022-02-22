using System;

namespace Tasssssk
{
    public enum TaskStatus
    {
        ToDo = 1,
        InProgress,
        Done
    }
    public class Tasks
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string TaskName { get; set; } = "";
        public string TaskDescription { get; set; } = "";
        public int ProjectsId { get; set; }
        public Projects Projects;
    }
}

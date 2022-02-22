using System;
using System.Collections.Generic;

namespace Tasssssk
{
    public enum ProjectStatus
    {
        NotStarted = 1,
        Active,
        Completed
    }
    public class Projects
    {
        public int ID { get; set; }
        public ProjectStatus projectStatus;
        public int Priority { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectCompletionDate { get; set; }
        public List<Tasks> tasks { get; set; } = new List<Tasks>();
    }
}

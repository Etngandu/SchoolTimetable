using ENB.SchoolTimetables.Entities.Collections;
using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class DisplaySubject 
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string SubjectDescription { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public ClassRs? ClassRs { get; set; }
        public PlannedTimetables? PlannedTimetables { get; set; }

        
    }
}

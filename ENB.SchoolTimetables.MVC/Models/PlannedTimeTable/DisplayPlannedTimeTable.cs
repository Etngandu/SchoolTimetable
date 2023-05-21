using ENB.SchoolTimetables.Entities;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class DisplayPlannedTimeTable
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int ClassRId { get; set; }
        public ClassR? ClassR { get; set; }
        public Ref_Periods Period_Number { get; set; }       
        public DateTime PlannedDay { get; set; }
        public EventStatus? TimetableStatus { get; set; }
        public string OtherDetails { get; set; } = string.Empty;
        public string NameTeacher { get; set; } = string.Empty;
        public string NameSubject { get; set; } = string.Empty;
        public RefClassR NameClass { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

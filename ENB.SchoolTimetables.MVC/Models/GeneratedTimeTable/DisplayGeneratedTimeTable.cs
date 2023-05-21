using ENB.SchoolTimetables.Entities;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class DisplayGeneratedTimeTable
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int PlannedTimetableId { get; set; }
        public PlannedTimetable? PlannedTimetable { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int ClassRId { get; set; }
        public ClassR? ClassR { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public RefClassR Classroom { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Color { get; set; }
        public string NameTeacher { get; set; } = string.Empty;
        public string NameSubject { get; set; } = string.Empty;
        public RefClassR NameClass { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

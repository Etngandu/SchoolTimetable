using ENB.SchoolTimetables.Entities;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class DisplayTeacherSubject
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

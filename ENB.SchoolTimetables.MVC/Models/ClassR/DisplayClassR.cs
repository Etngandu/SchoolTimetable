using ENB.SchoolTimetables.Entities;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class DisplayClassR
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public RefClassR  Classroom { get; set; } 
        public string ClassDescription { get; set; } = string.Empty;
        public string NameTeacher { get; set; }=string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

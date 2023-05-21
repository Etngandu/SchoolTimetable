using ENB.SchoolTimetables.Entities;
using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class CreateAndEditGeneratedTimeTable : IValidatableObject
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
        public Ref_Periods PeriodNumber { get; set; }
        public EventStatus TimetableStatus { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Color { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TimetableStatus == EventStatus.None)
            { 
               yield return new ValidationResult("TimetableStatus can't be None", new[] {"TimetableStatus"});
            }
        }
    }
}

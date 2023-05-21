using ENB.SchoolTimetables.Entities.Collections;
using ENB.SchoolTimetables.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.Entities
{
    public class PlannedTimetable : DomainEntity<int>, IDateTracking
    {
        public PlannedTimetable()
        {
            GeneratedTimetables = new();
        }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int TeacherId { get; set; }
        public Teacher?  Teacher { get; set; }
        public int ClassRId { get; set; }
        public ClassR? ClassR { get; set; }
        public Ref_Periods Period_Number { get; set; }       
        public DateTime PlannedDay { get; set; }
        public string OtherDetails { get; set; } = string.Empty;
        public EventStatus? TimetableStatus { get; set; } 
        public GeneratedTimetables GeneratedTimetables { get; set; }
        public DateTime DateCreated { get ; set; }
        public DateTime DateModified { get; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
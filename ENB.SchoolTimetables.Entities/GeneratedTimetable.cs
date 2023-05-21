using ENB.SchoolTimetables.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.SchoolTimetables.Entities
{
    public class GeneratedTimetable: DomainEntity<int>,IDateTracking
    {
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int PlannedTimetableId { get; set; }
        public PlannedTimetable? PlannedTimetable { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int ClassRId { get; set; }
        public ClassR?  ClassR{ get; set; }         
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Color { get; set; }       
        public DateTime DateCreated { get; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

using ENB.SchoolTimetables.Entities.Collections;
using ENB.SchoolTimetables.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.SchoolTimetables.Entities
{
    public class ClassR : DomainEntity<int>, IDateTracking
    {
        public ClassR()
        {
            PlannedTimetables = new();
            GeneratedTimetables = new();
        }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }       
        public  RefClassR Classroom { get; set; }
        public string ClassDescription { get; set; } = string.Empty;
        public PlannedTimetables PlannedTimetables { get; set; }
        public GeneratedTimetables GeneratedTimetables  { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

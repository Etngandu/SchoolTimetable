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
    public class Subject : DomainEntity<int>, IDateTracking
    {
        public Subject()
        {
            ClassRs = new();
            PlannedTimetables= new();
            GeneratedTimetables= new();
            SubjectTeachers= new();
        }
        public string SubjectName { get; set; } = string.Empty;
        public string SubjectDescription { get; set; } = string.Empty;
        public DateTime DateCreated { get; set ; }
        public DateTime DateModified { get ; set; }        
        public ClassRs ClassRs { get; set; }
        public PlannedTimetables PlannedTimetables{ get; set; }
        public GeneratedTimetables GeneratedTimetables { get; set; }

        /// <summary>
        /// Gets or sets the List of subject per Teacher.
        /// </summary>
        /// 
        public SubjectsTeachers SubjectTeachers { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(SubjectName))
                yield return new ValidationResult("SubjectName can't be null", new[] { "SubjectName" });
        }
    }
}

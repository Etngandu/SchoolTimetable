using ENB.SchoolTimetables.Entities;
using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class CreateAndEditPlannedTimeTable :IValidatableObject
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
        public string OtherDetails { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Period_Number == Ref_Periods.None)
            {
                yield return new ValidationResult("Period_Number can't be None", new[] { "Period_Number" });
            }

            
            if(string.IsNullOrEmpty(OtherDetails))
            {
                yield return new ValidationResult("OtherDetails can't be empty", new[] {"OtherDetails" });
            }
        }
    }
}

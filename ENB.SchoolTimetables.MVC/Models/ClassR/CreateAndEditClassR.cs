using ENB.SchoolTimetables.Entities;
using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class CreateAndEditClassR : IValidatableObject
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public RefClassR  Classroom { get; set; } 
        public string ClassDescription { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Classroom == RefClassR.None)
            {
                yield return new ValidationResult("ClassName can't be none", new[] { "RefClassR" });
            }
            if (string.IsNullOrEmpty(ClassDescription))
            {
                yield return new ValidationResult("ClassDescription can't be none", new[] { "ClassDescription" });
            }
        }
    }
}

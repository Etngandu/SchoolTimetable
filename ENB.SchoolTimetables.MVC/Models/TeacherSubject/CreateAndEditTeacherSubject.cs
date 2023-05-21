using ENB.SchoolTimetables.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class CreateAndEditTeacherSubject :IValidatableObject
    {
        public int Id { get; set; }
        public int TeacherId { get;  set; }
        public Teacher? Teacher { get; set; }
        public int SubjectId { get;  set; }
        public Subject? Subject { get; set; }
        public string NameTeacher { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public List<SelectListItem>? ListTeacher { get; set; }

        public List<SelectListItem>? ListSubject { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TeacherId == 0)
            {
                yield return new ValidationResult("Teacher can't be Null", new[] { "TeacherId" });
            }

            if (SubjectId == 0)
            {
                yield return new ValidationResult("Subject can't be Null", new[] { "SubjectId" });
            }
        }
    }
}

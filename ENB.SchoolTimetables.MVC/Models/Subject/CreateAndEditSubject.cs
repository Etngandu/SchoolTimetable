using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.Entities.Collections;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class CreateAndEditSubject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string SubjectDescription { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public ClassRs? ClassRs { get; set; }
        public PlannedTimetables? PlannedTimetables { get; set; }
    }

        public class CreateAndEditSubjectValidator : AbstractValidator<CreateAndEditSubject>
        {
            public CreateAndEditSubjectValidator()
            {
                RuleFor(x => x.SubjectName)
                .NotEmpty()
                .WithMessage("SubjectName  can't be empty");

                RuleFor(x => x.SubjectDescription)
               .NotEmpty().WithMessage("SubjectDescription  can't be empty");


            }

        }
    
}
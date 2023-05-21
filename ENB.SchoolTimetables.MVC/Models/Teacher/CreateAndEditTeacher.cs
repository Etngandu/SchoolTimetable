using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.Entities.Collections;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ENB.SchoolTimetables.MVC.Models
{
    public class CreateAndEditTeacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string EmailAddress { get; set; } = String.Empty;

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime DateOfBirth { get; set; }


        public Address? AddressCustomer { get; set; }


        public string Other_details { get; set; } = String.Empty;

       
    }

    public class CreateAndEditTeacherValidator : AbstractValidator<CreateAndEditTeacher>
    {
        public CreateAndEditTeacherValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("FirstName  can't be empty");

            RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("LastName  can't be empty");

            RuleFor(x => x.EmailAddress)            
           .NotEmpty().WithMessage("Mail")
           .EmailAddress();

            RuleFor(x => x.Gender)
           .NotEqual(Gender.None)
           .WithMessage("Gender  can't be None");

            RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("PhoneNumber  can't be empty");   

            RuleFor(x => x.DateOfBirth)
           .LessThan(x=> DateTime.Now)           
           .WithMessage($"DateOfBirth should be less than {DateTime.Now}" );

            RuleFor(x => x.Other_details)
          .NotEmpty().WithMessage("Other_details  can't be empty");

        }
    
    }
}

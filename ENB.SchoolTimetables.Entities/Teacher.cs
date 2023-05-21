using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENB.SchoolTimetables.Entities.Collections;
using ENB.SchoolTimetables.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ENB.SchoolTimetables.Entities
{
    public class Teacher : DomainEntity<int>, IDateTracking
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Customer class.
        /// </summary>
        public Teacher()
        {
            
            AddressTeacher = new();           
            SubjectTeachers= new();
            ClassRs = new();
            PlannedTimetables= new();
            GeneratedTimetables= new(); 
        }
       
        #endregion

        #region "Public Properties"

        /// <summary>
        /// Gets or sets the first name of this Customer.
        /// </summary>
        [Required]
        public string FirstName { get; set; }=String.Empty;

        /// <summary>
        /// Gets or sets the last name of this Customer.
        /// </summary>
        [Required]
        public string LastName { get; set; }= String.Empty;

        /// <summary>
        /// Gets or sets the EmailAddress of the Customer.
        /// </summary>
        /// 
        public string EmailAddress { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the Gender of the Customer.
        /// </summary>
        /// 
        public Gender Gender { get; set; } 

        /// <summary>
        /// Gets or sets the PhoneNumber of the Customer.
        /// </summary>
        /// 
        public string PhoneNumber { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the DateOfBirth of the Customer.
        /// </summary>
        /// 
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth
        {
            get;
            set;
        }

       

        /// <summary>
        /// Gets or sets the List of subject per Teacher.
        /// </summary>
        /// 
        public SubjectsTeachers  SubjectTeachers { get; set; }


        public ClassRs ClassRs { get; set; }

        public PlannedTimetables PlannedTimetables { get; set; }
        public GeneratedTimetables GeneratedTimetables { get; set; }

        /// <summary>
        /// Gets or sets the Other_details of the Customer.
        /// </summary>
        /// 
        public string Other_details { get; set; } = String.Empty;

        /// <summary>
        /// Gets the full name of this person.
        /// </summary>
        public string FullName
        {
            get
            {
                string temp = FirstName ?? string.Empty;
                if (!string.IsNullOrEmpty(LastName))
                {
                    if (temp.Length > 0)
                    {
                        temp += " ";
                    }
                    temp += LastName;
                }
                return temp;
            }
        }

       

        /// <summary>
        /// Gets or sets the Address of this customer.
        /// </summary>

        public Address AddressTeacher { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateCreated { get ; set ; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        /// 
        public DateTime DateModified { get; set ; }
        #endregion



        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
            if (DateOfBirth > DateTime.Now)
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
        }
        #endregion
    }
}

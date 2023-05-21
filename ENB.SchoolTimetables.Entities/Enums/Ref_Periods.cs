using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ENB.SchoolTimetables.Entities
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum Ref_Periods
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        [Display(Name = "Lesson 1 08:30 - 09:20")]
        /// <summary>
        /// Indicates a business contact record.
        /// </summary>
        Lesson_1 = 1,

        [Display(Name = "Lesson 2 09:20 - 10:10")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_2 = 2,

        [Display(Name = "Lesson 3 10:20 - 11:10")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_3 = 3,

        [Display(Name = "Lesson 4 11:10 - 12:00")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_4 = 4,

        [Display(Name = "Lesson 5 12:00 - 12:50")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_5 = 5,

        [Display(Name = "Lesson 6 12:50 - 13:40")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_6 = 6,

        [Display(Name = "Lesson 7 13:40 - 14:30")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_7 = 7,

       [Display(Name = "Lesson 8 14:40 - 15:30")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_8 = 8,

       [Display(Name = "Lesson 9 15:30 - 16:20")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_9 = 9,

        [Display(Name = "Lesson 10 16:20 - 17:10")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Lesson_10 = 10
    }
}

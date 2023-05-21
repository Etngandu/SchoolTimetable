using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.Entities
{
    /// <summary>
    /// Determines the day of a the week.
    /// </summary>
    public enum Ref_Days
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a monday.
        /// </summary>
        Monday = 1,

       
        /// <summary>
        /// Indicates tuesday.
        /// </summary>
        Tuesday = 2,

        
        /// <summary>
        /// Indicates wednesday.
        /// </summary>
        Wednesday = 3,

        /// <summary>
        /// Indicates thursday.
        /// </summary>
        Thursday = 4,

        /// <summary>
        /// Indicates friday.
        /// </summary>
        Friday = 5

    }
}

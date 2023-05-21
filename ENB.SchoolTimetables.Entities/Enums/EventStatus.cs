using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.SchoolTimetables.Entities
{
    /// <summary>
    /// Determines the Status of an LawyerEvent record.
    /// </summary>

    public enum EventStatus
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates LawyerEvent Planned.
        /// </summary>
        [Display(Name = "Generated")]
        orange = 1,        

    }
}

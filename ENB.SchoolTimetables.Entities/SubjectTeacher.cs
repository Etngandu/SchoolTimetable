using ENB.SchoolTimetables.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.SchoolTimetables.Entities
{
    public class SubjectTeacher: IDateTracking
    {
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set; }
    }
}

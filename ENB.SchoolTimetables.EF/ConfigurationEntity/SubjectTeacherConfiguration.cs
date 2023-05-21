using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ENB.SchoolTimetables.Infrastructure;
using ENB.SchoolTimetables.Entities;

namespace ENB.SchoolTimetables.EF.ConfigurationEntity
{
   public class SubjectTeacherConfiguration : IEntityTypeConfiguration<SubjectTeacher>
    {
        public void Configure(EntityTypeBuilder<SubjectTeacher> builder)
        {
            builder.HasKey(x => new { x.SubjectId, x.TeacherId });

        }
    }
}

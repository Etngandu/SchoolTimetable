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
   public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.Property(x => x.SubjectName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.SubjectDescription).IsRequired().HasMaxLength(350);            

        }
    }
}

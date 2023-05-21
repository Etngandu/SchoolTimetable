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
   public class PlannedTimeTableConfiguration : IEntityTypeConfiguration<PlannedTimetable>
    {
        public void Configure(EntityTypeBuilder<PlannedTimetable> builder)
        {
           
            builder.Property(x => x.OtherDetails).IsRequired().HasMaxLength(350);            
            builder.HasOne<Teacher>(t=>t.Teacher)
                .WithMany(tt=>tt.PlannedTimetables)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Subject>(s => s.Subject)
                .WithMany(tt => tt.PlannedTimetables)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

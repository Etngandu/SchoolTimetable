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
   public class GeneratedTimeTableConfiguration : IEntityTypeConfiguration<GeneratedTimetable>
    {
        public void Configure(EntityTypeBuilder<GeneratedTimetable> builder)
        {
           
            builder.Property(x => x.Color).IsRequired().HasMaxLength(30);            
            builder.HasOne<Teacher>(t=>t.Teacher)
                .WithMany(gt=>gt.GeneratedTimetables)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Subject>(s => s.Subject)
                .WithMany(gt => gt.GeneratedTimetables)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<ClassR>(cl => cl.ClassR)
                .WithMany(gt => gt.GeneratedTimetables)
                .HasForeignKey(x => x.ClassRId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<PlannedTimetable>(pl => pl.PlannedTimetable)
                .WithMany(gt => gt.GeneratedTimetables)
                .HasForeignKey(x => x.PlannedTimetableId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

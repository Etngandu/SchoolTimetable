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
   public class ClassRConfiguration : IEntityTypeConfiguration<ClassR>
    {
        public void Configure(EntityTypeBuilder<ClassR> builder)
        {
            
            builder.Property(x => x.ClassDescription).IsRequired().HasMaxLength(350);            

        }
    }
}

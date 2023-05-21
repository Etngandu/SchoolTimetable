using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ENB.SchoolTimetables.Infrastructure;
using ENB.SchoolTimetables.Entities;

namespace ENB.InsuranceAndClaims.EF.ConfigurationEntity
{
   public class TeacherConfiguration:IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.EmailAddress).IsRequired().HasMaxLength(250);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(150);            
            builder.Property(x => x.Other_details).IsRequired().HasMaxLength(300);
            builder.OwnsOne(o => o.AddressTeacher, a =>
            {
                a.Property(p => p.Number_street).HasMaxLength(600)
                    .HasColumnName("Numberstreet")
                    .HasDefaultValue("");
                a.Property(p => p.City).HasMaxLength(250)
                    .HasColumnName("City")
                    .HasDefaultValue("");
                a.Property(p => p.State_province_county).HasMaxLength(250)
                    .HasColumnName("State_province_county")
                    .HasDefaultValue("");
                a.Property(p => p.Zipcode).HasMaxLength(12)
                    .HasColumnName("ZipCode")
                    .HasDefaultValue("");
                a.Property(p => p.Country).HasMaxLength(250)
                   .HasColumnName("Country")
                   .HasDefaultValue("");
            });

        }
    }
}

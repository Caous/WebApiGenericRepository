using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGenericRepository.Model;

namespace WebApiGenericRepository.Infraestructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Tb_User");
            builder.HasKey(u => u.Id).HasName("PK_Persons");
            builder.Property(u => u.FirstName).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(u => u.LastName).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(u => u.DtInclused).IsRequired();
            builder.Property(u => u.DtExclused).IsRequired(false);
            builder.Property(u => u.IdDepartament).HasColumnName("IdDepartament");
            builder.HasOne(d => d.Departament)
             .WithMany()
             .HasForeignKey(d => d.IdDepartament).IsRequired(false);
        }
    }
}

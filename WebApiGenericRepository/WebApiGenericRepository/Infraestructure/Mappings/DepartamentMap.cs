using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGenericRepository.Model;

namespace WebApiGenericRepository.Infraestructure.Mappings
{
    public class DepartamentMap : IEntityTypeConfiguration<Departament>
    {
        public void Configure(EntityTypeBuilder<Departament> builder)
        {
            builder.ToTable("Tb_Departament");
            //Config Primary Key and Named
            builder.HasKey(dp => dp.IdDepartament).HasName("PK_Departament");
            builder.Property(dp => dp.NameDepartament).IsRequired();
            builder.Property(dp => dp.DesDepartament).IsRequired();
            builder.Property(dp => dp.DtInclused).IsRequired();
            builder.Property(dp => dp.DtExclused).IsRequired(false);
        }
    }
}

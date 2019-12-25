using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyForms.Core.Domain.Entities;
using SurveyForms.Database.FormsAdminDb.Extensions;

namespace SurveyForms.Database.FormsAdminDb.Configuration
{
    public class FormAreaConfiguration : IEntityTypeConfiguration<FormArea>
    {
        public void Configure(EntityTypeBuilder<FormArea> builder)
        {
            builder.HasKey(_ => _.FormAreaId);
            builder.Property(_ => _.FormAreaId)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(_ => _.Name)
                .IsUnique();

            builder.HasMany(_ => _.Forms)
                .WithOne(_ => _.FormArea)
                .OnDelete(DeleteBehavior.Cascade);

            builder.SetAllAuditableProperties();
        }
    }
}

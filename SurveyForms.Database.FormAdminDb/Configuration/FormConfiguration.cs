using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyForms.Core.Domain.Entities;
using SurveyForms.Database.FormsAdminDb.Extensions;

namespace SurveyForms.Database.FormsAdminDb.Configuration
{
    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasKey(_ => _.FormId);
            builder.Property(_ => _.FormId)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(_ => _.Description)
                .HasMaxLength(300);

            builder.Property(_ => _.TimeStamp)
                .IsRowVersion();

            builder.HasIndex(_ => new { _.Name, _.FormAreaId })
                .IsUnique();

            builder.HasMany(_ => _.FormItems)
                .WithOne(_ => _.Form)
                .OnDelete(DeleteBehavior.Cascade);

            builder.SetAllAuditableProperties();
        }
    }
}

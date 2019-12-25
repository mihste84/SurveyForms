using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyForms.Core.Domain.Entities;
using SurveyForms.Database.FormsAdminDb.Extensions;

namespace SurveyForms.Database.FormAdminDb.Configuration
{
    public class FormItemConfiguration : IEntityTypeConfiguration<FormItem>
    {
        public void Configure(EntityTypeBuilder<FormItem> builder)
        {
            builder.HasKey(_ => _.FormItemId);
            builder.Property(_ => _.FormItemId)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(_ => _.TimeStamp)
                .IsRowVersion();

            builder.SetAllAuditableProperties();
        }
    }
}

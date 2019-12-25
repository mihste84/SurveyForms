using Microsoft.EntityFrameworkCore;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using SurveyForms.Core.Domain.Entities;
using SurveyForms.Database.FormAdminDb.Configuration;
using SurveyForms.Database.FormsAdminDb.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Database.FormsAdminDb
{
    public class FormAdminDbContext : DbContext, IFormAdminDbContext
    {
        public FormAdminDbContext(DbContextOptions<FormAdminDbContext> options) : base(options) { }

        public DbSet<FormArea> FormAreas { get; set; }
        public DbSet<Form> Forms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FormAreaConfiguration());
            modelBuilder.ApplyConfiguration(new FormConfiguration());
            modelBuilder.ApplyConfiguration(new FormItemConfiguration());
        }

        public Task<int> SaveAuditableChangesAsync(string userName, CancellationToken cancellationToken = default)
        {
            var modifiedEntries = base.ChangeTracker.Entries()
               .Where(x => x.Entity is BaseAuditableEntity
                   && (x.State == EntityState.Added || x.State == EntityState.Modified)).ToList();

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseAuditableEntity entity)
                {
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.Created = now; //Update 'Created' column on all inserts
                        entity.CreatedBy = userName;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.Created).IsModified = false;
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    }

                    entity.Updated = now;
                    entity.UpdatedBy = userName;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

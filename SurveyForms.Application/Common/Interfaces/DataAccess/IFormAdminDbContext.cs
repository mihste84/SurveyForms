using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SurveyForms.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Application.Common.Interfaces.DataAccess
{
    public interface IFormAdminDbContext
    {
        DbSet<FormArea> FormAreas { get; set; }
        DbSet<Form> Forms { get; set; }
        Task<int> SaveAuditableChangesAsync(string userName, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
        EntityEntry Remove(object entity);
    }
}

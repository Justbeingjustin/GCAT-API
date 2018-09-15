using GCAT.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GCAT.API.Services
{
    public interface IReportJobRepository
    {
        Task<IEnumerable<ReportJob>> GetReportJobsAsync();

        void AddReportJob(ReportJob reportJob);

        Task<ReportJob> GetReportJobAsync(long id);

        Task<bool> SaveChangesAsync();
    }
}
using GCAT.API.Contexts;
using GCAT.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCAT.API.Services
{
    public class ReportJobRepository : IReportJobRepository
    {
        private CryptoContext _context;

        public ReportJobRepository(CryptoContext context)
        {
            _context = context;
        }

        public void AddReportJob(ReportJob reportJob)
        {
            if (reportJob == null)
            {
                throw new ArgumentNullException(nameof(reportJob));
            }
            _context.Add(reportJob);
        }

        public async Task<ReportJob> GetReportJobAsync(long id)
        {
            return await _context.ReportJobs.FirstOrDefaultAsync(b => b.ReportJobId == id);
        }

        public async Task<IEnumerable<ReportJob>> GetReportJobsAsync()
        {
            return await _context.ReportJobs.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
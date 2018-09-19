using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GCAT.API.Contexts;
using GCAT.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GCAT.API.Services
{
    public class ReportJobFileRepository : IReportJobFileRepository
    {
        private CryptoContext _context;

        public ReportJobFileRepository(CryptoContext context)
        {
            _context = context;
        }

        public async Task<ReportJobFile> GetReportJobFileAsync(long id)
        {
            return await _context.ReportJobFile.FirstOrDefaultAsync(b => b.ReportJobId == id);
        }
    }
}
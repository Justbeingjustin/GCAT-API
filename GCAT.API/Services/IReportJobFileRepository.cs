using GCAT.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCAT.API.Services
{
    public interface IReportJobFileRepository
    {
        Task<ReportJobFile> GetReportJobFileAsync(long reportJobId);
    }
}
using AutoMapper;
using GCAT.API.Entities;
using GCAT.API.Filters;
using GCAT.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GCAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportJobFilesController : Controller
    {
        private IReportJobFileRepository _reportJobFileRepository;
        private IReportJobRepository _reportJobRepository;
        private UserManager<CryptoUser> _userManager;
        private readonly IMapper _mapper;

        public ReportJobFilesController(IReportJobFileRepository reportJobFileRepository, IReportJobRepository reportJobRepository, UserManager<CryptoUser> userManager, IMapper mapper)
        {
            _reportJobFileRepository = reportJobFileRepository;
            _reportJobRepository = reportJobRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        private async Task<CryptoUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        [ReportJobResultFilterAttribute]
        [Route("{reportJobId}", Name = "GetReportJobFile")]
        public async Task<IActionResult> GetReportJob(long reportJobId)
        {
            var userIdOfReport = (await _reportJobRepository.GetReportJobAsync(reportJobId)).UserId;

            var reportJob = await _reportJobFileRepository.GetReportJobFileAsync(reportJobId);
            if (reportJob == null || userIdOfReport != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            return File(new MemoryStream(reportJob.File), "application/octet-stream", "SingleAssetSummary.xlsm");
        }
    }
}
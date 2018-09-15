using AutoMapper;
using GCAT.API.Entities;
using GCAT.API.Filters;
using GCAT.API.Models;
using GCAT.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportJobsController : Controller
    {
        private IReportJobRepository _reportJobRepository;
        private UserManager<CryptoUser> _userManager;
        private readonly IMapper _mapper;

        public ReportJobsController(IReportJobRepository organizationRepository, UserManager<CryptoUser> userManager, IMapper mapper)
        {
            _reportJobRepository = organizationRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        private async Task<CryptoUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);

        //             var user = await GetCurrentUserAsync();
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<Entities.ReportJob> reportJobEntities = await _reportJobRepository.GetReportJobsAsync(userId);
            return Ok(reportJobEntities);
        }

        [HttpGet]
        [ReportJobResultFilterAttribute]
        [Route("{id}", Name = "GetReportJob")]
        public async Task<IActionResult> GetReportJob(long id)
        {
            var reportJob = await _reportJobRepository.GetReportJobAsync(id);
            if (reportJob == null || reportJob.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            return Ok(reportJob);
        }

        [HttpPost]
        [ReportJobResultFilterAttribute]
        public async Task<IActionResult> CreateReportJob([FromBody] ReportJobForCreation reportJobForCreation)
        {
            var reportJobEntity = _mapper.Map<Entities.ReportJob>(reportJobForCreation);
            reportJobEntity.CreatedDate = DateTime.Now;
            reportJobEntity.UserId = _userManager.GetUserId(User);
            reportJobEntity.StatusMessage = "Processing";
            _reportJobRepository.AddReportJob(reportJobEntity);

            await _reportJobRepository.SaveChangesAsync();

            // Fetch (refetch) the book from the data store, including the author
            await _reportJobRepository.GetReportJobAsync(reportJobEntity.ReportJobId);

            return CreatedAtRoute("GetReportJob",
                new { id = reportJobEntity.ReportJobId },
                reportJobEntity);
        }
    }
}
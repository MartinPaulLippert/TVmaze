using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TVmaze.Domain.Models;
using TVmaze.Services.Interfaces;

namespace TVmaze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly ITVmazeService _tvmazeService;
        private readonly ILogger<ShowsController> _logger;

        public ShowsController(ITVmazeService tvmazeService, ILogger<ShowsController> logger)
        {
            _tvmazeService = tvmazeService;
            _logger = logger;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Show>> Get(int page = 0, int pagesize = 5)
        {
            try
            {
                return Ok(_tvmazeService.GetAllShows(page, pagesize));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting shows");
                return StatusCode(500);
            }
        }
        
    }
}

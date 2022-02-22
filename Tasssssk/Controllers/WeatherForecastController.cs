using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasssssk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ILogger<ProjectsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Projects> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Projects
            {
                ProjectStartDate = DateTime.UtcNow.AddDays(index),
                ProjectCompletionDate = DateTime.Now.AddDays(index),
                Priority = index,
                ProjectName = "Nameee"
            })
            .ToArray();
        }
    }
}

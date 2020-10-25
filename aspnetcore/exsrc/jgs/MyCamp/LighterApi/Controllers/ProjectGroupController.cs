using LighterApi.Data;
using LighterApi.Data.Project;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LighterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectGroupController : ControllerBase
    {
        private readonly LighterDbContext _lighterDbContext;
        public ProjectGroupController(LighterDbContext lighterDbContext)
        {
            _lighterDbContext = lighterDbContext;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ProjectGroup group, CancellationToken cancellationToken)
        {
            //if (!ModelState.IsValid)
            //{
            //    return ValidationProblem();
            //}
            _lighterDbContext.ProjectGroups.Add(group);
            await _lighterDbContext.SaveChangesAsync(cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, group);

        }
    }
}

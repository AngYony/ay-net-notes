using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LighterApi.Data;
using LighterApi.Data.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LighterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly LighterDbContext _lighterDbContext;
        public ProjectController(LighterDbContext lighterDbContext)
        {
            _lighterDbContext = lighterDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetListAsync(CancellationToken cancellation)
        {
            return await _lighterDbContext.Projects.ToListAsync(cancellation);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateAsync([FromBody] Project project, CancellationToken cancellation)
        {
            //project.Id = Guid.NewGuid().ToString();
            _lighterDbContext.Projects.Add(project);
            await _lighterDbContext.SaveChangesAsync(cancellation);

            return StatusCode((int)HttpStatusCode.Created, project);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] Project project, CancellationToken cancellationToken)
        {
            var origin = await _lighterDbContext.Projects.FirstOrDefaultAsync(p => p.Id == project.Id, cancellationToken);
            if (origin == null)
            {
                return NotFound();
            }

            origin.Title = project.Title;
            origin.StartDate = project.StartDate;
            origin.EndDate = project.EndDate;

            await _lighterDbContext.SaveChangesAsync(cancellationToken);

            return Ok(origin);

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(string id, CancellationToken cancellationToken)
        {
            //关联查询，得到关联表结果集的三种方式
            #region 方式一：预先加载
            var project = await _lighterDbContext.Projects
                .Include(p => p.Groups)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            #endregion

            #region 方式二：显式加载
            var project2 = await _lighterDbContext.Projects.FirstOrDefaultAsync();
            //关联的是一个集合类型的导航属性
            await _lighterDbContext.Entry(project2).Collection(p => p.Groups).LoadAsync();
            //关联的是一个单一实体类型的导航属性
            //await _lighterDbContext.Entry(project2).Reference(p => p.Groups).LoadAsync();

            #endregion

            #region 方式三：延迟加载
            // 参见：https://docs.microsoft.com/zh-cn/ef/core/querying/related-data/lazy
            #endregion

            return Ok(project);
        }

    }
}

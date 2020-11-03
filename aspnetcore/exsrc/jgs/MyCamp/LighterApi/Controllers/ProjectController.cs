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
            return await _lighterDbContext.Projects
            .Include(p => p.Groups)
            .ToListAsync(cancellation);
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


        //并发冲突的解决
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Project>> UpdateAsync(string id,[FromBody] Project project,CancellationToken cancellationToken)
        {
           
            var origin = await _lighterDbContext.Projects.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            //先将旧的RowVersion传给新的，这样可以解决 在打开页面的过程中，已经被其他人更改了数据的情况
            _lighterDbContext.Entry(origin).Property(p => p.RowVersion).OriginalValue = project.RowVersion;

            if (origin == null)
                return NotFound();

            _lighterDbContext.Entry(origin).CurrentValues.SetValues(project);

            await _lighterDbContext.SaveChangesAsync(cancellationToken);
            return origin;
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

        /*
         HttpPut：一般用于整个对象的替换更新
         HttpPatch：一般用于单个对象的某个属性的部分更改
         */


        //对确定字段的更新
        [HttpPatch]
        [Route("{id}/title")]
        public async Task<ActionResult<Project>> SetTitleAsync(string id, [FromQuery] string title, CancellationToken cancellationToken)
        {
            //# region 方式一：先查询，再更新实体
            ////查询实体信息
            //var originProject = await _lighterDbContext.Projects.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            //var originGroup = await _lighterDbContext.ProjectGroups.Where(g => g.ProjectId == id).ToListAsync();


            ////修改实体属性
            //originProject.Title = title;

            //foreach (var group in originGroup)
            //{
            //    group.Name = $"{title}-{group.Name}";
            //}

            ////数据提交保存
            //await _lighterDbContext.SaveChangesAsync();

            //return originProject;

            //#endregion

            #region 方式二：不查询，直接更新实体
            //注意：使用这种方式时，必须将实体的所有的属性都赋值才能更新正确，否则没有赋值的属性将会设置成类型的默认值，导致数据异常

            var entity = new Project { Id = id };

            _lighterDbContext.Projects.Attach(entity);
            entity.Title = title;
            entity.LastUpdatedBy = "smallz";
            await _lighterDbContext.SaveChangesAsync();

            return entity;
            #endregion









        }


        //针对任意字段的更新
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<Project>> SetAsync(string id, CancellationToken cancellationToken)
        {
            //查询实体信息
            var origin = await _lighterDbContext.Projects
            //.Include(p => p.Groups)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            var properties = _lighterDbContext.Entry(origin).Properties.ToList();

            foreach (var query in HttpContext.Request.Query)
            {
                var property = properties.FirstOrDefault(p => p.Metadata.Name == query.Key);
                if (property == null) continue;

                var currentValue = Convert.ChangeType(query.Value.First(), property.Metadata.ClrType);

                _lighterDbContext.Entry(origin).Property(query.Key).CurrentValue = currentValue;
                _lighterDbContext.Entry(origin).Property(query.Key).IsModified = true;
            }


            //数据提交保存
            await _lighterDbContext.SaveChangesAsync();

            return origin;

        }


        //不查询删除和更新
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            #region 方式一：先查询再删除
            var project = await _lighterDbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project != null)
            {
                _lighterDbContext.Projects.Remove(project);
                await _lighterDbContext.SaveChangesAsync();
            }

            #endregion

            #region 方式二：不查询，直接删除
            var project2 = new Project { Id = id };

            //设置实例的状态
            _lighterDbContext.Projects.Attach(project2);
            _lighterDbContext.Projects.Remove(project2);
            await _lighterDbContext.SaveChangesAsync();

            #endregion

            return StatusCode((int)HttpStatusCode.NoContent);
        }






    }
}

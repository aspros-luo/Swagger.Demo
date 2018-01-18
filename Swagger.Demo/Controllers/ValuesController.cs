using AQHG.Infrastructure.Middleware.Core;
using Microsoft.AspNetCore.Mvc;
using Swagger.Dto;

namespace Swagger.Demo.Controllers
{
    /// <summary>
    /// 接口类
    /// </summary>

    public class ValuesController : WebApiController
    {
        /// <summary>
        /// get 方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("api.get")]
        [CustomSwaggerResponse("app", typeof(AppDTO))]
        public IActionResult Get(int id)
        {
            var appDto = new AppDTO{Id = 1,Title = "test",IsCheck = true};
            return Ok(new { app = appDto, is_success = true });
        }

        /// <summary>
        /// post 方法
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("api.post")]
        [CustomSwaggerResponse]
        public IActionResult Post([FromBody]AppDTO value)
        {
            return Success();
        }

        /// <summary>
        /// post2 方法
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("api.post2")]
        [CustomSwaggerResponse]
        public IActionResult Post2([FromBody]TestDTO value)
        {
            return Success();
        }
    }
}

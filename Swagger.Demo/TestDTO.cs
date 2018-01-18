using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swagger.Dto;

namespace Swagger.Demo
{
    /// <summary>
    /// 测试dto
    /// </summary>
    public class TestDTO
    {
        /// <summary>
        /// 测试 id
        /// </summary>
        [SwaggerDescription(description:"测试id",defaultValue:1)]
        public string Id { get; set; }
    }
}

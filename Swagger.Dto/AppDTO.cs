using System.ComponentModel;

namespace Swagger.Dto
{
    public class AppDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [SwaggerDescription(description: "测试id", defaultValue: 1)]
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [SwaggerDescription(description: "标题", defaultValue: "测试标题")]
        public string Title { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        [SwaggerDescription(description: "是否选中", defaultValue: true)]
        public bool IsCheck { get; set; }
    }
}

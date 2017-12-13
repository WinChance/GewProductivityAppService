namespace GewProductivityAppService.Models
{
    public class GeneralQueryBindModel
    {
        /// <summary>
        /// 查询类型，必须
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 可选
        /// </summary>
        public string param2 { get; set; }
        /// <summary>
        /// 可选
        /// </summary>
        public string param3 { get; set; }
        /// <summary>
        /// 可选
        /// </summary>
        public string param4 { get; set; }
        /// <summary>
        /// 可选
        /// </summary>
        public string param5 { get; set; }
    }
}
namespace GewProductivityAppService.Models.Yd.SarongStatus
{
    /// <summary>
    /// 产量录入对象
    /// </summary>
    public class RtProductionBindModel
    {
        public string Type { get; set; }

        public string BatchNo { get; set; }

        public string SarongNO { get; set; }

        public string InputClass { get; set; }
        /// <summary>
        /// 工人
        /// </summary>
        public string WorkID { get; set; }
        /// <summary>
        /// 输入人
        /// </summary>
        public string InputerID { get; set; }
        /// <summary>
        /// 产量
        /// </summary>
        public string YieldNum { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 计价类型
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 输入方式
        /// </summary>
        public string Way { get; set; }

    }

    
}
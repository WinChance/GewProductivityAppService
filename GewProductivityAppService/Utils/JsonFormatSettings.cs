using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GewProductivityAppService.Utils
{
    public class JsonFormatSettings
    {
        public static readonly JsonFormatSettings Instance = new JsonFormatSettings();

        /// <summary>
        /// 获取Json格式配置
        /// </summary>
        /// <returns></returns>
        public JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                ContractResolver = new NullToEmptyStringResolver(),
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
        }
    }

    
}
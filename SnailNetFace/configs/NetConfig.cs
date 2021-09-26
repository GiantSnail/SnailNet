using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailNetFace.configs
{
    /// <summary>
    /// 网络的相关配置,在应用启动时根据配置文件实例化
    /// </summary>
    internal class NetConfig
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string Host;
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port;
        /// <summary>
        /// 最大连接数量
        /// </summary>
        public string MaxConNum;
    }
}

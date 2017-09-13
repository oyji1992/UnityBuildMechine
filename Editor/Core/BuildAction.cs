using System.Collections.Generic;

namespace UniGameTools.BuildMechine
{
    /// <summary>
    /// 编译会导致非公开变量的值丢失
    /// </summary>
    public abstract class BuildAction
    {
        public BuildMechine Mechine { get; set; }

        /// <summary>
        /// 上下文
        /// </summary>
        public BuildContext Context
        {
            get
            {
                if (Mechine == null) return null;
                return Mechine.Context;
            }
        }

        /// <summary>
        /// 更新方法
        /// 每Tick调用
        /// </summary>
        public abstract BuildState OnUpdate();

        /// <summary>
        /// 获得当前进度
        /// </summary>
        /// <returns>
        /// null : 不显示进度
        /// not null : 显示进度
        /// </returns>
        public virtual BuildProgress GetProgress()
        {
            return null;
        }
    }
}
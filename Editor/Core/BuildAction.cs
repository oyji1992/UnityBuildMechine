using System;
using UnityEngine;

namespace UniGameTools.BuildMechine
{
    /// <summary>
    /// 编译会导致非公开变量的值丢失
    /// </summary>
    [Serializable]
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
        public virtual BuildState OnUpdate() { return BuildState.Success; }

        /// <summary>
        /// 退出
        /// </summary>
        public virtual void OnExit() { }

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

        public BuildAction DeepCopy()
        {
            var json = JsonUtility.ToJson(this);

            var newAction = JsonUtility.FromJson(json, this.GetType());

            return newAction as BuildAction;
        }
    }
}
using System.Collections.Generic;

namespace UniGameTools.BuildMechine
{
    public class BuildProgress
    {
        public string Title;
        public string Content;
        public float Porgress;
    }

    /// <summary>
    /// 编译会导致非公开变量的值丢失
    /// </summary>
    public abstract class BuildAction
    {
        /// <summary>
        /// 
        /// </summary>
        private BuildState _state;

        /// <summary>
        /// 属性
        /// </summary>
        public List<Info> Infos = new List<Info>();

        /// <summary>
        /// 当前进度
        /// </summary>
        public BuildState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;

                //            if (_state != BuildState.Building)
                //            {
                //                if (Mechine != null) Mechine.Update();
                //            }
            }
        }

        public virtual void OnUpdate() { }

        public abstract void OnEnter();

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
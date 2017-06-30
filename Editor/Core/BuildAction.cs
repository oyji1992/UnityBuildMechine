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
        private BuildState _state;

        public List<object> Infos = new List<object>();

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

        public virtual void Update(){}

        public abstract void Build();

        public abstract BuildProgress GetProgress();
    }
}
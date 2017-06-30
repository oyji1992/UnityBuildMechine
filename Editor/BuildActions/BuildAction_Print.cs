using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_Print : BuildAction
    {
        public string Msg;

        public BuildAction_Print(string msg)
        {
            this.Msg = msg;
        }

        public override void OnEnter()
        {
            Debug.Log(Msg);
            this.State = BuildState.Success;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}
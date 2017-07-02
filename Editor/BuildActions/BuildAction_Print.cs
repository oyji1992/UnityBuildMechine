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

        public override BuildState OnUpdate()
        {
            Debug.LogWarning(Msg);
            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}
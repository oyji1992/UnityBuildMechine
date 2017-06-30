using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SaveAndFresh : BuildAction
    {
        public override void OnEnter()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            this.State = BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}
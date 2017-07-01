using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SaveAndFresh : BuildAction
    {
        public override BuildState OnUpdate()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}
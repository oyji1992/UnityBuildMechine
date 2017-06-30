using UnityEditor;

public class BuildAction_SaveAndFresh : BuildAction
{
    public override void Build()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        this.State = BuildState.Succeed;
    }

    public override BuildProgress GetProgress()
    {
        return null;
    }
}
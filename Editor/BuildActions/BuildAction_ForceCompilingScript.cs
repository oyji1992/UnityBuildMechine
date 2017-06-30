using UnityEditor;

public class BuildAction_ForceCompilingScript : BuildAction
{
    public override void Build()
    {
        var cMonoScript = MonoImporter.GetAllRuntimeMonoScripts()[0];

        foreach (var script in MonoImporter.GetAllRuntimeMonoScripts())
        {
            var assetPath = AssetDatabase.GetAssetPath(script);
            if (assetPath.StartsWith("Assets/_Scripts"))
            {
                cMonoScript = script;
                break;
            }
        }

        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(cMonoScript));

        this.State = BuildState.Succeed;
    }

    public override BuildProgress GetProgress()
    {
        return null;
    }
}
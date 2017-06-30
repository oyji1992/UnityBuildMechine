public class BuildAction_SetBuildScenes : BuildAction
{
    public string[] sceneNames;

    public BuildAction_SetBuildScenes(params string[] sceneNames)
    {
        this.sceneNames = sceneNames;
    }

    public override void Build()
    {
        BuildHelper.SetBuildScenes(sceneNames);
        State = BuildState.Succeed;
    }

    public override BuildProgress GetProgress()
    {
        return null;
    }
}
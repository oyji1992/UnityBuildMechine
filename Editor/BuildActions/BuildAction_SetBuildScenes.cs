namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetBuildScenes : BuildAction
    {
        public string[] sceneNames;

        public BuildAction_SetBuildScenes(params string[] sceneNames)
        {
            this.sceneNames = sceneNames;
        }

        public override void OnEnter()
        {
            BuildHelper.SetBuildScenes(sceneNames);
            State = BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}
namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetBuildScenes : BuildAction
    {
        public string[] sceneNames;

        public BuildAction_SetBuildScenes(params string[] sceneNames)
        {
            this.sceneNames = sceneNames;
        }

        public override BuildState OnUpdate()
        {
            BuildHelper.SetBuildScenes(sceneNames);
            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }

    public class BuildAction_Error : BuildAction
    {
        public override BuildState OnUpdate()
        {
            return BuildState.Failure;
        }
    }
}
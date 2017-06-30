namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetAppIcon : BuildAction
    {
        public string path;

        public BuildAction_SetAppIcon(string path)
        {
            this.path = path;
        }

        public override void OnEnter()
        {
            BuildHelper.SetAppIcon(path);
            State = BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}
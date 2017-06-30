namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetAppIcon : BuildAction
    {
        public string path;

        public BuildAction_SetAppIcon(string path)
        {
            this.path = path;
        }

        public override void Build()
        {
            BuildHelper.SetAppIcon(path);
            State = BuildState.Succeed;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}
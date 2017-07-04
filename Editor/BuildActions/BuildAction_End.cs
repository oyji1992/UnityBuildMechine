namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_End : BuildAction
    {
        public override BuildState OnUpdate()
        {
            return BuildState.Success;
        }
    }
}
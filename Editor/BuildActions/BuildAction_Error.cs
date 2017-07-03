namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_Error : BuildAction
    {
        public override BuildState OnUpdate()
        {
            this.Context.Set("ErrorCode", "5");

            return BuildState.Failure;
        }
    }
}
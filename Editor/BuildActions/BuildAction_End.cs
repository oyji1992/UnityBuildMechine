using System;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_CommandLine : BuildAction
    {
        public override BuildState OnUpdate()
        {
            throw new NotImplementedException();
        }
    }

    public class BuildAction_End : BuildAction
    {
        public override BuildState OnUpdate()
        {
            return BuildState.Success;
        }
    }
}